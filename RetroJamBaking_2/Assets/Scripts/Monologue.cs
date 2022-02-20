using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monologue
{
    [TextArea(3, 10)]
    public string[] sentences;

    public Monologue() { }

    public Monologue(string[] sentences)
    {
        this.sentences = sentences;
    }

    public Monologue(string fileName)
    {
        var textFile = Resources.Load<TextAsset>(fileName);
        Debug.Log(textFile == null);
        string Text = textFile.text;

        List<string> vs = new List<string>();
        while (Text.Length > 0)
        {
            Debug.Log(Text.IndexOf("\r\n"));
            if (Text.IndexOf("\r\n") != -1)
            {
                vs.Add(Text.Substring(0, Text.IndexOf("\r\n")));
                Text = Text.Remove(0, Text.IndexOf("\r\n") + 2);
            }
            else { vs.Add(Text); break; }
        }

        sentences = vs.ToArray();
        Debug.Log("Monologue created");
    }
}
