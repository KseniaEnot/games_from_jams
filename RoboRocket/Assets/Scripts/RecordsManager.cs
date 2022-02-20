using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordsManager : MonoBehaviour
{
    Text recs;
    public Button Delete;
    string key = "Record";
    void Start()
    {
        recs = GetComponent<Text>();
        recs.text = "Таблица рекордов";
        for (int rec = 1; rec < 6; rec++)
        {
            if (PlayerPrefs.HasKey(key + rec.ToString())) recs.text += "\n" + PlayerPrefs.GetInt(key + rec.ToString());
            else break;
        }
        Delete.onClick.AddListener(DeleteOnClick);
    }

    void DeleteOnClick()
    {
        PlayerPrefs.DeleteAll();
        recs.text = "Таблица рекордов";
        Delete.onClick.AddListener(DeleteOnClick);
    }

}
