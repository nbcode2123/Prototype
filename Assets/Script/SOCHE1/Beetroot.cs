using System.Collections.Generic;
using UnityEngine;

public class Beetroot : FoodIngredient
{
    public override void Start()
    {
        base.Start();
        CookProcess = new Stack<IngredientProcess>(new List<IngredientProcess>() { IngredientProcess.FinalProcessedIngredient, IngredientProcess.Grate, IngredientProcess.Peel, IngredientProcess.Wash });


    }
    public override void LayDown()
    {
    }

}
