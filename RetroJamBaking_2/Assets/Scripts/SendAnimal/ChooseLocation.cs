using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLocation : MonoBehaviour
{
    public Locations locNum;

    public void SelectLocation()
    {
        this.GetComponentInParent<SanderPanel>().animal.GetComponent<AnimalController>().location = (int)locNum;
        this.GetComponentInParent<SanderPanel>().animal.GetComponent<AnimalController>().condition = 1;
        this.GetComponentInParent<SanderPanel>().animal.SetActive(false);
        this.GetComponentInParent<SanderPanel>().EndChoosing();
    }
}
