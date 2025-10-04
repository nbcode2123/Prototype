using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pepper : FoodIngredient
{

    public override void LayDown()
    {
        gameObject.transform.DORotate(new Vector3(0, 0, 90f), 0.5f);
    }

    public override void Start()
    {
        base.Start();
        CookProcess = new Stack<IngredientProcess>(new List<IngredientProcess> { IngredientProcess.FinalProcessedIngredient, IngredientProcess.Slice });


    }
}
