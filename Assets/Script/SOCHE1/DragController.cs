using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DragController : MonoBehaviour
{
    public static DragController Instance { set; get; }
    private IDraggable _currentDragging;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        DragDetective();
        Dragging();
        ReleaseDragging();

    }
    private void DragDetective()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Lấy tất cả hits
            RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero, 100f, LayerMask.GetMask("Draggable"));

            if (hits.Length > 0)
            {
                // Chọn hit có SpriteRenderer nằm trên cùng
                var chosenHit = hits
                    .Select(h => h.collider)
                    .Where(c => c != null && c.GetComponent<IDraggable>() != null && c.GetComponent<IDraggable>().enabled)
                    .OrderByDescending(c =>
                    {
                        var sr = c.GetComponent<SpriteRenderer>();
                        if (sr == null) return int.MinValue;
                        return sr.sortingLayerID * 10000 + sr.sortingOrder;
                    })
                    .FirstOrDefault();

                if (chosenHit != null)
                {
                    _currentDragging = chosenHit.GetComponent<IDraggable>();
                    _currentDragging.OnDragStart();
                }
            }
        }

    }
    private void Dragging()
    {
        if (Input.GetMouseButton(0) && _currentDragging != null)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _currentDragging.OnDrag(mouseWorld);
        }
    }
    private void ReleaseDragging()
    {
        if (Input.GetMouseButtonUp(0) && _currentDragging != null)
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 100f, LayerMask.GetMask("DropStorage"));
            if (hit.collider != null)
            {
                IDropStorage droppable = hit.collider.GetComponent<IDropStorage>();
                if (droppable != null)
                {
                    _currentDragging.OnDragEnd(droppable);
                    _currentDragging = null;

                }
                else if (droppable == null)
                {
                    _currentDragging.OnDragCancel();

                    _currentDragging = null;
                }

            }
            else
            {
                _currentDragging.OnDragCancel();

                _currentDragging = null;
            }

        }
    }




}
