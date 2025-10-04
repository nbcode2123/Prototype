using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Nao : MonoBehaviour
{
    public GameObject _placeIngredient;
    public GrateController _grateIngredient;
    public GameObject _finalIngredient;
    private void Start()
    {
        ObserverManager.AddListener("GrateComplete", GrateComplete);
    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener("GrateComplete", GrateComplete);

    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener("GrateComplete", GrateComplete);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // FoodIngredient foodIngredient = other.gameObject.GetComponent<FoodIngredient>();
        // if (foodIngredient != null)
        // {
        //     Debug.Log("food");
        // }
        // else return;

        GrateController grateController = other.gameObject.GetComponentInChildren<GrateController>();
        if (grateController != null)
        {
            _grateIngredient = grateController;
            grateController.GrateProcess();

            if (_finalIngredient == null)
            {
                _finalIngredient = Instantiate(_grateIngredient._finalIngredient, _placeIngredient.transform.position, Quaternion.identity);
            }



        }
        else return;
    }
    public void GrateComplete()
    {
        gameObject.transform.DOMoveY(100f, 1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }


}
