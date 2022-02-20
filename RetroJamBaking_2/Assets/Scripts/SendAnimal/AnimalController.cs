using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalController : MonoBehaviour
{
    [HideInInspector]
    public int Baff;
    [SerializeField]
    public int condition; // 0 - waiting press, 1 - at the location, 2 - returned, 3 - waiting go sleep
    //[HideInInspector]
    public int location; //0 - forest, 1 - river, 2 - MEADOW, 3 - mount, 4 - cave

    void Start()
    {
        Baff = 0;
        condition = 0;
    }

    public void AnimalPress()
    {
        if (FindObjectOfType<MonologueManager>().isInMonologue) return;
        switch (condition)
        {
            case 0:
                this.GetComponent<Button>().interactable = false;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().SendAnimal(this.gameObject);
                break;
            case 2:
                this.GetComponent<Button>().interactable = false;
                RetuurmedAnimal();
                break;
            default: Debug.Log("Error CONDITION"); break;
        }
    }

    private void RetuurmedAnimal()
    {
        if (Baff == 0)
        {
            if (!HasCloseLocation())
            {
                int randInt = Random.Range(0, int.MaxValue) % 20;
                if (randInt >= 0 && randInt < 10)
                {
                    //get item
                    //dialog
                    string[] text = new string[1];
                    text[0] = checkBaff();
                    Monologue talk = new Monologue(text);
                    FindObjectOfType<MonologueManager>().StartMonologue(talk);
                }
                else if (randInt < 17)
                {
                    int i = Info.location.Length - 1;
                    while (Info.location[i] == true)
                        i--;
                    if (i == (Info.location.Length - 1)) //open CAVE
                    {
                        //dialog
                        Info.location[i] = true;
                        Monologue talk = new Monologue("Cave");
                        FindObjectOfType<MonologueManager>().StartMonologue(talk);
                    }
                    else //open MOUNTAINS
                    {
                        //dialog
                        Info.location[i] = true;
                        Monologue talk = new Monologue("Mountains");
                        FindObjectOfType<MonologueManager>().StartMonologue(talk);
                    }
                }
                else if (randInt < 20)
                {
                    // faild
                    //dialog
                    //DONT FORGET TO RANDOM
                    int rand = Random.Range(0, int.MaxValue) % 5;
                    Monologue talk = new Monologue("Failed" + rand);
                    FindObjectOfType<MonologueManager>().StartMonologue(talk);

                }
                else
                    Debug.Log("Error location" + randInt);
            }
            else
            {
                int randInt = Random.Range(0, int.MaxValue) % 10;
                if (randInt >= 0 && randInt < 7)
                {
                    //get item
                    //dialog
                    //ili dialog doljen bit' imenno pri poly4enii...
                    string[] text = new string[1];
                    text[0] = checkBaff();
                    Monologue talk = new Monologue(text);
                    FindObjectOfType<MonologueManager>().StartMonologue(talk);

                }
                else if (randInt < 10)
                {
                    // faild
                    //dialog
                    //DONT FORGET TO RANDOM
                    int rand = Random.Range(0, int.MaxValue) % 5;
                    Monologue talk = new Monologue("Failed" + rand);
                    FindObjectOfType<MonologueManager>().StartMonologue(talk);
                }
                else
                    Debug.Log("Error location" + randInt);
            }
        }
        else
        {
            string[] text = new string[1];
            text[0] = checkBaff();
            Monologue talk = new Monologue(text);
            FindObjectOfType<MonologueManager>().StartMonologue(talk);
        }
        condition = 3;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>().CheckEndDay();
    }

    bool HasCloseLocation()
    {
        for (int i = 0; i < Info.location.Length; i++)
            if (Info.location[i] == false)
                return false;
        return true;
    }
    string checkBaff()
    {//s4itat' symarnyy stroky i vivodit' ee v dialog?
        string result = "I bring you: ";
        switch (Baff)
        {
            case 0: result += getItem(location);break;
            case 1: result += getItem(location) + ", " + getItem(location); break;
            case 2: result += getItem(location) + ", " + getItem(location) + ", " + getItem(-1); break;
            case 3: result += getItem(location) + ", " + getItem(location) + ", " + getItem(-1) + ", " + getItem(-1); break;
            default: Debug.Log("Error baf"); break;
        }
        Baff = 0;
        Debug.Log(result);
        return result;
    }

    Ingredients getItem(int localLocation)
    {
        if (localLocation == -1)
            localLocation = Random.Range(0, int.MaxValue) % Info.location.Length;
        switch (localLocation)
        {
            case 0:
                switch (Random.Range(0, int.MaxValue) % 3)
                {
                    case 0: Info.inventory[(int)Ingredients.CONE]++; return Ingredients.CONE; 
                    case 1: Info.inventory[(int)Ingredients.MOSS]++; return Ingredients.MOSS;
                    case 2: Info.inventory[(int)Ingredients.GRASS]++; return Ingredients.GRASS;
                    default: Debug.Log("Error get item"); break;
                }
                break;
            case 1:
                switch (Random.Range(0, int.MaxValue) % 3)
                {
                    case 0: Info.inventory[(int)Ingredients.ALGAE]++; return Ingredients.ALGAE;
                    case 1: Info.inventory[(int)Ingredients.FISH]++; return Ingredients.FISH;
                    case 2: Info.inventory[(int)Ingredients.ROCK]++; return Ingredients.ROCK;
                    default: Debug.Log("Error get item"); break;
                }
                break;
            case 2:
                switch (Random.Range(0, int.MaxValue) % 3)
                {
                    case 0: Info.inventory[(int)Ingredients.BEE]++; return Ingredients.BEE;
                    case 1: Info.inventory[(int)Ingredients.GRASS]++; return Ingredients.GRASS;
                    case 2: Info.inventory[(int)Ingredients.FLOWER]++; return Ingredients.FLOWER;
                    default: Debug.Log("Error get item"); break;
                }
                break;
            case 3:
                switch (Random.Range(0, int.MaxValue) % 2)
                {
                    case 0: Info.inventory[(int)Ingredients.SNOW]++; return Ingredients.SNOW;
                    case 1: Info.inventory[(int)Ingredients.MOSS]++; return Ingredients.MOSS;
                    default: Debug.Log("Error get item"); break;
                }
                break;
            case 4:
                switch (Random.Range(0, int.MaxValue) % 2)
                {
                    case 0: Info.inventory[(int)Ingredients.MUSHROOM]++; return Ingredients.MUSHROOM;
                    case 1: Info.inventory[(int)Ingredients.ROCK]++; return Ingredients.ROCK;
                    default: Debug.Log("Error get item"); break;
                }
                break;
            default: Debug.Log("Error item location"); break;
        }

        throw new System.Exception("We are fucked");
    }
}
