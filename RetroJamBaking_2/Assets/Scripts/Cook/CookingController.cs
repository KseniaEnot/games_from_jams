using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingController : MonoBehaviour
{
    public List<Ingredients> chosen { get; private set; }
    [SerializeField] GameObject panel;
    [SerializeField] GameObject prefab;
    [SerializeField] Sprite empty;
    [SerializeField] Sprite[] sprites = new Sprite[10];
    [SerializeField] GameObject[] block = new GameObject[4];

    private void Start()
    {
        //gameObject.SetActive(false);
    }

    private void Init()
    {
        chosen = new List<Ingredients>();
        foreach (GameObject obj in block) obj.GetComponent<Image>().sprite = empty;
    }

    public void StartCooking()
    {
        //if (gameObject.activeSelf) return;
        //gameObject.SetActive(true);
        panel.transform.parent.gameObject.SetActive(true);

        Init();
        int i = 0;
        foreach(int ingNum in Info.inventory)
        {
            if (ingNum > 0)
            {
                //create button
                GameObject obj = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                obj.GetComponentInChildren<Text>().text = ((Ingredients)i).ToString() + " x" + ingNum;
                obj.transform.parent = panel.transform;
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.GetComponent<ChooseIngridient>().InitChoice((Ingredients)i);
            }
            i++;
        }
    }

    public void EndCooking()
    {
        foreach (Transform child in panel.transform)
            Destroy(child.gameObject);
        foreach (Ingredients ing in chosen)
        {
            Info.inventory[(int)ing]--;
        }
        
        int dish = WhatToGenerate.MakeDish(chosen);
        Debug.Log("Dish num " + dish);
        //start monologue about dish
        if (dish <= 3)
        {
            Monologue monAbDish = new Monologue("Dish" + dish);
            FindObjectOfType<MonologueManager>().StartMonologue(monAbDish);
            StartCoroutine(WaitingMonologue(dish));
        }
        else
        {
            //END GAME
            FindObjectOfType<GameControllerScript>().EndGame();
        }
    }

    public IEnumerator WaitingMonologue(int dish)
    {
        yield return new WaitUntil(() => !FindObjectOfType<MonologueManager>().isInMonologue);
        Debug.Log("Nigt end");
        //send dish to gameController or something
        FindObjectOfType<GameControllerScript>().NewDay(dish);
    }

    public void StartEndCooking()
    {
        //START ANIMATION HERE
        StartCoroutine(WaitingCook());
    }

    public IEnumerator WaitingCook()
    {
        Debug.Log("Coroutine start");
        yield return new WaitForSeconds(1.2f);
        EndCooking();
    }

    public void Select(Ingredients ing)
    {
        chosen.Add(ing);
        UpdateBlocks();
    }

    public void Deselect(Ingredients ing)
    {
        chosen.Remove(ing);
        UpdateBlocks();
    }

    void UpdateBlocks()
    {
        int i = 0;
        foreach(Ingredients ing in chosen)
        {
            block[i].GetComponent<Image>().sprite = sprites[(int)ing];
            i++;
        }
        for (int j = i; i < block.Length; i++) block[i].GetComponent<Image>().sprite = empty;
    }
}
