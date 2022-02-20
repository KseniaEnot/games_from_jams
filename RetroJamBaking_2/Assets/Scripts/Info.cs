using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Info
{
    public static int[] inventory = new int[10] {1, 1, 0, 0, 1, 1, 2, 0, 1, 1};
    public static bool[] location = new bool[5] {true, true, true, false, false};
    public static List<Ingredients> specialIngredients = new List<Ingredients>
    {
        Ingredients.MOSS, Ingredients.MUSHROOM, Ingredients.SNOW, Ingredients.BEE
    };
}

public enum Ingredients
{
    CONE = 0,
    MOSS = 1,
    GRASS = 2,
    ALGAE = 3, //vodorosl'
    FISH = 4,
    ROCK = 5,
    BEE = 6,
    FLOWER = 7,
    SNOW = 8,
    MUSHROOM = 9
}

public enum Locations
{
    FOREST = 0,
    RIVER = 1,
    MEADOW = 2,
    MOUNTAINS = 3,
    CAVE = 4
}
