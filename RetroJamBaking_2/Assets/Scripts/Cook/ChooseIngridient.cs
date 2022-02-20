using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseIngridient : MonoBehaviour
{
    public Ingredients ingNum = Ingredients.CONE;
    private bool isSelected = false;
    private CookingController controller;

    public void InitChoice(Ingredients ingredient)
    {
        controller = FindObjectOfType<CookingController>();
        ingNum = ingredient;
    }

    public void Choose()
    {
        if (isSelected) {
            isSelected = false;
            GetComponent<Image>().color = new Color(1f, 1f, 1f);
            controller.Deselect(ingNum);
        }
        else if (controller.chosen.Count < 4)
        {
            isSelected = true;
            GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);
            controller.Select(ingNum);
        }
    }
}
