using System.Linq;
using UnityEngine;

public class ClickController : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Lấy tất cả hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero, 100f, LayerMask.GetMask("Clickable"));

            if (hits.Length > 0)
            {
                // Chọn hit có SpriteRenderer nằm trên cùng
                var chosenHit = hits
                    .Select(h => h.collider)
                    .Where(c => c != null && c.GetComponent<IClickable>() != null)
                    .OrderByDescending(c =>
                    {
                        var sr = c.GetComponent<SpriteRenderer>();
                        if (sr == null) return int.MinValue;
                        return sr.sortingLayerID * 10000 + sr.sortingOrder;
                    })
                    .FirstOrDefault();

                chosenHit.GetComponent<IClickable>().OnClick();
            }
        }


    }
}
