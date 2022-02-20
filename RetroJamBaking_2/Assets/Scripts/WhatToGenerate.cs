using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WhatToGenerate
{
    public static int MakeDish(List<Ingredients> ingredients)
    {
        int specIngCount = 0;
        foreach (Ingredients ing in ingredients)
        {
            if (Info.specialIngredients.Contains(ing)) specIngCount++;
        }

        return specIngCount;
    }
}
