
using Unity.VisualScripting;
using UnityEngine;

public class FinalIngredient : IDraggable
{
    public FoodIngredient foodIngredient;
    public Ingredient _ingredient;

    public override void OnDragEnd(IDropStorage storage)
    {
        storage.OnDrop(this);

    }



}
