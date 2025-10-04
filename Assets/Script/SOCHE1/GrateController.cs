using UnityEngine;

public class GrateController : MonoBehaviour
{
    public Collider2D _collider;
    public GameObject _finalIngredient;
    public FoodIngredient _foodIngredient;
    public void GrateProcess()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x - 0.2f, gameObject.transform.localScale.y - 0.2f, 1f);
        if (gameObject.transform.localScale.x <= 0.2f)
        {
            ObserverManager.Notify("GrateComplete");
            ObserverManager.Notify("ChangeDropStorage", _foodIngredient);
            gameObject.SetActive(false);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
