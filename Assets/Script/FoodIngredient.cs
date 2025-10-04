using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FoodIngredient : IDraggable
{
    public Stack<IngredientProcess> CookProcess = new Stack<IngredientProcess>();
    public Ingredient _ingredient;
    public override void Start()
    {
        base.Start();

    }
    public virtual void LayDown()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
public enum IngredientProcess
{
    Peel,
    Wash,
    Cut,
    Slice,
    Grate,
    BocVo,
    FinalProcessedIngredient


}
[Serializable]
public enum Ingredient
{
    RawBeef,
    LeafLemon,
    Pepper,
    RawChicken,
    SliceRawChicken,
    BlackPepper,
    SliceChicken
}

