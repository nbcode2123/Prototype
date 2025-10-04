using UnityEngine;

public class MouseControllerMiniGame : MonoBehaviour
{
    [SerializeField] private GameObject _currentObj;
    [SerializeField] private GameObject _basket;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseHold();
        DragObject();
    }
    private void CheckMouseHold()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 100f, LayerMask.GetMask("Interactable"));
            if (hit.collider != null)
            {
                Debug.Log("Chuột đang trỏ vào: " + hit.collider.name);
                _currentObj = hit.collider.gameObject;
                if (_currentObj.GetComponent<PeeledBeetroot>() != null)
                {
                    _basket.GetComponent<Basket>().PutItemToBasket(_currentObj);
                    _currentObj = null;


                }

            }
            else
            {
                Debug.Log("Không trúng object nào");
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (_currentObj != null)
            {
                if (_currentObj.GetComponent<Shovel>() != null)
                {
                    _currentObj.GetComponent<Shovel>().ResetPosition();

                }

            }
            _currentObj = null;

        }
    }
    private void DragObject()
    {
        if (_currentObj != null)
        {
            if (_currentObj.GetComponent<Shovel>() != null)
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                _currentObj.transform.position = mouseWorldPos;
            }


        }
    }
}
