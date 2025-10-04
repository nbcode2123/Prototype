using DG.Tweening;
using UnityEngine;

public class PeelController : MonoBehaviour
{
    public FoodIngredient _foodIngredient;
    public SpriteRenderer skinRenderer; // sprite vỏ
    private Texture2D skinTexture;
    private Color[] pixels;
    public bool _isOnCutBroad;
    public GameObject PeelingSkin;
    public GameObject SkinRemove;

    void Start()
    {
        // Clone texture để thay đổi
        skinTexture = Instantiate(skinRenderer.sprite.texture);
        skinRenderer.sprite = Sprite.Create(skinTexture,
            new Rect(0, 0, skinTexture.width, skinTexture.height),
            new Vector2(0.5f, 0.5f));

        pixels = skinTexture.GetPixels();

    }
    public void PeelComplete()
    {

        skinRenderer.DOFade(0f, 0.5f);
        _foodIngredient.CookProcess.Pop();
        if (_foodIngredient.CookProcess.Peek() == IngredientProcess.Grate)
        {
            _foodIngredient.transform.parent.GetComponentInChildren<GrateController>()._collider.enabled = true;
            _foodIngredient.transform.parent.GetComponentInChildren<CoreDraggable>().SetDefaultPos(gameObject.transform.position);

        }


    }

    void Update()
    {

    }

    public void PeelAt(Vector3 worldPos)
    {
        if (_isOnCutBroad == true && _foodIngredient.CookProcess.Peek() == IngredientProcess.Peel)
        {
            // Chuyển world -> local
            Vector3 localPos = transform.InverseTransformPoint(worldPos);

            // Sprite pivot có thể không ở center
            Sprite sprite = skinRenderer.sprite;
            Rect rect = sprite.rect;
            Vector2 pivot = sprite.pivot; // pixel

            // LocalPos từ (-0.5..0.5) * size -> pixel
            float unitsToPixelsX = rect.width / sprite.bounds.size.x;
            float unitsToPixelsY = rect.height / sprite.bounds.size.y;

            int px = Mathf.RoundToInt(localPos.x * unitsToPixelsX + pivot.x);
            int py = Mathf.RoundToInt(localPos.y * unitsToPixelsY + pivot.y);

            int radius = 20;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int nx = px + x;
                    int ny = py + y;

                    if (nx >= 0 && nx < rect.width && ny >= 0 && ny < rect.height)
                    {
                        float dist = Mathf.Sqrt(x * x + y * y);
                        if (dist <= radius)
                        {
                            Color c = pixels[ny * (int)rect.width + nx];
                            c.a = 0;
                            pixels[ny * (int)rect.width + nx] = c;
                        }
                    }
                }
            }

            skinTexture.SetPixels(pixels);
            skinTexture.Apply();
            GetPeelProgress();
        }


    }
    public float GetPeelProgress()
    {
        int totalPixels = pixels.Length;
        int peeledPixels = 0;

        for (int i = 0; i < totalPixels; i++)
        {
            if (pixels[i].a <= 0.01f) // alpha gần 0 => đã gọt
                peeledPixels++;
        }

        float progress = (float)peeledPixels / totalPixels;

        if (progress >= 0.99)
        {
            ObserverManager.Notify("PeelComplete");
            Instantiate(SkinRemove, gameObject.transform.position, Quaternion.identity);
            PeelComplete();




        }

        return progress; // 0.0 -> 1.0
    }



}
