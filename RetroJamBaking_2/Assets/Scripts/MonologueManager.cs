using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonologueManager : MonoBehaviour
{
    [HideInInspector] public bool isInMonologue { get; private set; }
    Text dialogueText;
    [SerializeField] GameObject panel;
    [SerializeField] float typingSpeed;
    private Queue<string> sentences;
    void Start()
    {
        isInMonologue = false;
        //sentences = new Queue<string>();
    }



    public void StartMonologue(Monologue dialogue)
    {
        isInMonologue = true;
        Debug.Log("Starting Conversation");
        //animator.SetBool("IsOpen", true);
        //FindObjectOfType<Player>().isInMonologue = true;
        //sentences.Clear();
        sentences = new Queue<string>();

        dialogueText = panel.GetComponentInChildren<Text>();
        //if (ifHelper) panel.GetComponent<Image>().color = new Color(1, 0, 0, 0.4f);
        panel.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        panel.SetActive(true);

        foreach (string sentence in dialogue.sentences)
        {
            //Debug.Log("Added " + sentence);
            sentences.Enqueue(sentence);
        }

        DiaplayNextSentence();
    }

    public void DiaplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        //StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        if (sentence[0] == '!') { dialogueText.fontStyle = FontStyle.Italic; sentence = sentence.Remove(0, 1); }
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        //yield return new WaitForSeconds(4);
        //DiaplayNextSentence();
    }

    void EndDialogue()
    {
        //FindObjectOfType<Player>().isInMonologue = false;
        //StopAllCoroutines();
        Debug.Log("End");
        panel.SetActive(false);
        isInMonologue = false;
        //animator.SetBool("IsOpen", false);
        sentences.Clear();
    }
}
