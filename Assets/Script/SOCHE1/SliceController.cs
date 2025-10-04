using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SliceController : MonoBehaviour
{
    public FoodIngredient _foodIngredient;
    public bool _isOnCutBroad;
    public bool _sliceProcess;
    public SpriteRenderer _coreRender;
    private Texture2D skinTexture;
    private Color[] pixels;
    public List<Transform> _sliceTransform;
    public int _sliceCounter;
    public GameObject _knifeCutPrototype;
    public Collider2D _sliceCollider;
    public GameObject _processedIngredient;
    void Start()
    {
        // Clone texture để thay đổi
        skinTexture = Instantiate(_coreRender.sprite.texture);
        _coreRender.sprite = Sprite.Create(skinTexture,
            new Rect(0, 0, skinTexture.width, skinTexture.height),
            new Vector2(0.5f, 0.5f));
        pixels = skinTexture.GetPixels();
        _sliceCounter = 0;
        _sliceCollider.enabled = false;
        _processedIngredient.GetComponent<Collider2D>().enabled = false;
        _processedIngredient.GetComponent<IDraggable>().SetDefaultPos(gameObject.transform.position);
        _sliceCollider.enabled = true;
        _knifeCutPrototype.SetActive(false);



    }
    void Update()
    {
        if (_foodIngredient.CookProcess.Count > 0)
        {
            if (Input.GetMouseButtonDown(0) && _foodIngredient.CookProcess.Peek() == IngredientProcess.Slice && _sliceProcess == true)
            {
                _processedIngredient.SetActive(true);
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, LayerMask.GetMask("SliceTrigger"));
                if (hit.collider != null)
                {
                    CutSprite(_sliceTransform[_sliceCounter].position);
                    Debug.Log(3);
                    _knifeCutPrototype.transform.position = _sliceTransform[_sliceCounter].transform.position;
                    var startY = _knifeCutPrototype.gameObject.transform.position.y;
                    Sequence sequence = DOTween.Sequence();
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY - 0.5f, 0.2f));
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY + 0.5f, 0.2f));
                    sequence.Append(_knifeCutPrototype.transform.DOMoveY(startY, 0.2f));
                    sequence.OnComplete(() =>
                    {
                        sequence.Kill();

                    });
                    _sliceCounter++;
                    if (_sliceCounter > _sliceTransform.Count - 1)
                    {
                        Debug.Log("Slice Complete");
                        ObserverManager.Notify("Slice Complete");
                        SliceComplete();
                        _knifeCutPrototype.SetActive(false);
                        _sliceProcess = false;
                        return;
                    }
                    _knifeCutPrototype.transform.position = _sliceTransform[_sliceCounter].position;
                }
            }
        }

    }


    public void CutSprite(Vector3 worldPos)
    {
        if (_sliceProcess == true && _isOnCutBroad)
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
    public void SliceProcess()
    {
        _sliceCollider.enabled = true;
        _knifeCutPrototype.SetActive(true);
        _knifeCutPrototype.transform.position = _sliceTransform[0].transform.position;
        _sliceProcess = true;






    }
    public void SliceComplete()
    {
        _foodIngredient.CookProcess.Pop();

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_coreRender.DOFade(0f, 0.5f));

        sequence.AppendCallback(() =>
        {
            _processedIngredient.SetActive(true);
        });
        sequence.OnComplete(() =>
        {
            sequence.Kill();
        });
        _processedIngredient.GetComponent<Collider2D>().enabled = true;
        ObserverManager.Notify("SliceComplete");




    }



}
