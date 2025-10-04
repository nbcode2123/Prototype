using System.Collections.Generic;
using UnityEngine;

public class BocVo : MonoBehaviour
{
    public FoodIngredient _foodIngredient;
    public GameObject _mask;
    public GameObject _skin;
    public GameObject _removeSkin;
    public GameObject _core;
    public int ClickCounter;
    public bool _isOnCutBroad;
    private void Update()
    {
        if (_foodIngredient.CookProcess.Count > 0)
        {
            if (Input.GetMouseButtonDown(0) && _foodIngredient.CookProcess.Peek() == IngredientProcess.BocVo && _isOnCutBroad == true)
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 100f, LayerMask.GetMask("RemoveSkinTrigger"));
                if (hit.collider != null)
                {
                    ClickCounter++;
                    if (ClickCounter == 3)
                    {
                        _mask.SetActive(true);
                        _skin.SetActive(true);

                    }
                    else if (ClickCounter == 6)
                    {
                        _mask.SetActive(false);
                        _skin.SetActive(false);
                        _removeSkin = Instantiate(_removeSkin, gameObject.transform.position, Quaternion.identity);
                        _removeSkin.GetComponent<Collider2D>().enabled = true;
                        _core.SetActive(true);
                        _foodIngredient.CookProcess.Pop();
                        gameObject.SetActive(false);


                    }


                }
            }
        }

    }


}
