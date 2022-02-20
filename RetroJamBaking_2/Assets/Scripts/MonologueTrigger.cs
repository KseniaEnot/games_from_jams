using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonologueTrigger : MonoBehaviour
{
    [SerializeField] Monologue mg; //just an array of strings
    [SerializeField] string fileName;
    void Start()
    {
        //can read from file here i guess
        //or need to write it in unity
        if (fileName != null && fileName.Length > 0) mg = new Monologue(fileName);
    }
    public void TriggerMonologue()
    {
        //Debug.Log(mg.sentences[0]);
        MonologueManager monMan = FindObjectOfType<MonologueManager>();
        if (monMan.isInMonologue) monMan.DiaplayNextSentence();
        else FindObjectOfType<MonologueManager>().StartMonologue(mg);
    }
}
