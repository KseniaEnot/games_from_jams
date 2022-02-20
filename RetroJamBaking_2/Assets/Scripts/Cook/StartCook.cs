using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCook : MonoBehaviour
{
    [SerializeField] GameObject cookingController;
    private CookingController controller;

    private void Awake()
    {
        controller = FindObjectOfType<CookingController>();
    }

    public void GO()
    {
        transform.parent.gameObject.SetActive(false);
        controller.StartCooking();
    }

    public void END()
    {
        if (controller.chosen.Count != 4) return;
        cookingController.SetActive(false);
        controller.StartEndCooking();
    }
}
