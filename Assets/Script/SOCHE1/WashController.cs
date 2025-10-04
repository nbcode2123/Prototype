using DG.Tweening;
using UnityEngine;

public class WashController : MonoBehaviour
{
    public GameObject _soil;
    public FoodIngredient _foodIngredient;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void RemoveSoil()
    {
        if (_foodIngredient.CookProcess.Peek() == IngredientProcess.Wash)
        {
            _soil.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f).OnComplete(() =>
           {
               _soil.SetActive(false);
               _foodIngredient.CookProcess.Pop();
           });
        }


    }
}
