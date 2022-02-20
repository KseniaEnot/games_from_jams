using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontWantToCook : MonoBehaviour
{
    public void NO_COOKING_TODAY()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        FindObjectOfType<GameControllerScript>().NewDay(-1);
    }
}
