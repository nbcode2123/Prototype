using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class CutController : MonoBehaviour
{
    public FoodIngredient _foodIngredient;
    public bool _isOnCutBroad;
    public bool _cutProcess;
    public SpriteRenderer _coreRender;
    private Texture2D skinTexture;
    private Color[] pixels;
    public List<Transform> _cutTransform;
    public List<GameObject> _cutPiece;
    public int _cutCounter;
    public GameObject _knifeCutPrototype;
    public Collider2D _cutCollider;
    public GameObject _processedIngredient;
    void Start()
    {
        // Clone texture để thay đổi
        skinTexture = Instantiate(_coreRender.sprite.texture);
        _coreRender.sprite = Sprite.Create(skinTexture,
            new Rect(0, 0, skinTexture.width, skinTexture.height),
            new Vector2(0.5f, 0.5f));
        pixels = skinTexture.GetPixels();
        _cutCounter = 0;
        _cutCollider.enabled = false;
    }
    void Update()
    {
        if (_foodIngredient.CookProcess.Count > 0)
        {
            if (Input.GetMouseButtonDown(0) && _foodIngredient.CookProcess.Peek() == IngredientProcess.Cut && _cutProcess == true)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, LayerMask.GetMask("CutTrigger"));
                if (hit.collider != null)
                {
                    CutSprite(_cutTransform[_cutCounter].position);
                    _knifeCutPrototype.SetActive(true);
                    // Debug.Log();
                    _knifeCutPrototype.transform.position = _cutTransform[_cutCounter].transform.position;
                    var startY = _knifeCutPrototype.gameObject.transform.position.y;

                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY - 0.5f, 0.2f)); // xuống
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY + 0.5f, 0.2f)); // lên
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY, 0.2f));     // về gốc
                    sequence.OnComplete(() =>
                    {
                        sequence.Kill();
                    });
                    _cutPiece[_cutCounter].SetActive(true);
                    _cutCounter++;
                    if (_cutCounter > _cutTransform.Count - 1)
                    {
                        CutComplete();
                        _knifeCutPrototype.SetActive(false);
                        _cutProcess = false;
                        return;
                    }
                    _knifeCutPrototype.transform.position = _cutTransform[_cutCounter].position;
                }
            }
        }

    }


    public void CutSprite(Vector3 worldPos)
    {
        if (_cutProcess == true && _isOnCutBroad)
        {
            // B1: đổi worldPos về local để tính pixel pivot
            Vector3 localPos = transform.InverseTransformPoint(worldPos);

            Sprite sprite = _coreRender.sprite;
            Rect rect = sprite.rect;
            Vector2 pivot = sprite.pivot;

            float unitsToPixelsX = rect.width / sprite.bounds.size.x;
            float unitsToPixelsY = rect.height / sprite.bounds.size.y;

            int px = Mathf.RoundToInt(localPos.x * unitsToPixelsX + pivot.x);
            int py = Mathf.RoundToInt(localPos.y * unitsToPixelsY + pivot.y);

            // B2: lấy hướng world Y trong local space
            Vector2 worldYInLocal = transform.InverseTransformDirection(Vector3.right);

            // B3: duyệt pixel
            for (int x = 0; x < rect.width; x++)
            {
                for (int y = 0; y < rect.height; y++)
                {
                    // Vector từ điểm cắt tới pixel (local pixel space)
                    Vector2 pixelVec = new Vector2(x - px, y - py);

                    // Nếu pixel nằm "trên" theo hướng world Y
                    if (Vector2.Dot(pixelVec, worldYInLocal) < 0)
                    {
                        int index = y * (int)rect.width + x;
                        Color c = pixels[index];
                        c.a = 0;
                        pixels[index] = c;
                    }
                }
            }

            skinTexture.SetPixels(pixels);
            skinTexture.Apply();
        }



    }
    public void CutProcess()
    {
        _cutCollider.enabled = true;
        _knifeCutPrototype.SetActive(true);
        _knifeCutPrototype.transform.position = _cutTransform[0].transform.position;
        _foodIngredient.GetComponent<Collider2D>().enabled = false;

        _cutProcess = true;






    }
    public void CutComplete()
    {
        _foodIngredient.CookProcess.Pop();
        ObserverManager.Notify("CutComplete");
        _foodIngredient.GetComponent<Collider2D>().enabled = true;



        foreach (var item in _cutPiece)
        {
            item.SetActive(true);
        }
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_coreRender.DOFade(0f, 0.5f));
        sequence.AppendCallback(() =>
        {
            foreach (var item in _cutPiece)
            {
                item.SetActive(false);
            }

        });
        sequence.AppendCallback(() =>
        {
            _processedIngredient.SetActive(true);
        });
        sequence.OnComplete(() =>
        {
            sequence.Kill();
        });

    }




}
