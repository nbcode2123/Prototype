using System.Collections.Generic;
using UnityEngine;

public class RawChicken : FoodIngredient
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        _ingredient = Ingredient.RawChicken;
        CookProcess = new Stack<IngredientProcess>(new List<IngredientProcess>() { IngredientProcess.FinalProcessedIngredient, IngredientProcess.Cut });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
