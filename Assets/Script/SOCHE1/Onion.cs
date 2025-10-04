using System.Collections.Generic;
using UnityEngine;

public class Onion : FoodIngredient
{
    public override void Start()
    {
        base.Start();
        CookProcess = new Stack<IngredientProcess>(new List<IngredientProcess>() { IngredientProcess.FinalProcessedIngredient, IngredientProcess.Slice, IngredientProcess.BocVo });

    }
    public override void LayDown()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
