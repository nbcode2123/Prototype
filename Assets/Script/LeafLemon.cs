using UnityEngine;
using UnityEngine.UIElements;

public class LeafLemon : FoodIngredient
{
    public string _sortingLayer;
    public override void Start()
    {
        base.Start();
        _ingredient = Ingredient.LeafLemon;


    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnDrag(Vector3 position)
    {
        base.OnDrag(position);
    }
    public override void OnDragCancel()
    {
        base.OnDragCancel();


    }
}
