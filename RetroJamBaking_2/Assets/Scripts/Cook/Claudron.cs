using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Claudron : MonoBehaviour
{
    [SerializeField] GameObject cookOrNotPanel;

    public void OpenPanel()
    {
        cookOrNotPanel.SetActive(true);
        gameObject.GetComponent<Button>().interactable = false;
    }
}
