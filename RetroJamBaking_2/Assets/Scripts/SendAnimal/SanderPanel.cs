using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanderPanel : MonoBehaviour
{
    [HideInInspector]
    public GameObject animal;
    [SerializeField]
    private GameObject prefab;

    public void CoiceLocation()
    {
        int i = 0;
        foreach (bool ingNum in Info.location)
        {
            if (ingNum)
            {
                //create button
                GameObject obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                obj.GetComponentInChildren<Text>().text = ((Locations)i).ToString();
                obj.transform.parent = this.transform;
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.GetComponent<ChooseLocation>().locNum = (Locations)i;
            }
            i++;
        }
    }

    public void EndChoosing()
    {
        foreach (Transform child in this.transform)
            Destroy(child.gameObject);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().CheckEndSendAnimals();
        this.gameObject.SetActive(false);
    }


}
