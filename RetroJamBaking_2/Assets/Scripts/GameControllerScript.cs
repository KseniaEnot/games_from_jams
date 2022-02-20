using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    GameObject dayCanvas, nightCanvas, endCanvas;
    [SerializeField]
    GameObject lekarstvo;
    [SerializeField]
    GameObject claudron; //kotel
    [SerializeField]
    GameObject panelLocation;
    [SerializeField]
    GameObject sun;
    [SerializeField]
    GameObject[] animals;

    private void Start()
    {
        Monologue start = new Monologue("Start");
        FindObjectOfType<MonologueManager>().StartMonologue(start);
    }

    public void SendAnimal(GameObject animal) {
        panelLocation.SetActive(true);
        panelLocation.GetComponent<SanderPanel>().animal = animal;
        for (int i = 0; i < animals.Length; i++)
            if (animals[i].GetComponent<AnimalController>().condition == 0)
                animals[i].GetComponent<Button>().interactable = false;
        panelLocation.GetComponent<SanderPanel>().CoiceLocation();
    }
    public void CheckEndSendAnimals()
    {
        for (int i = 0; i < animals.Length; i++)
            if (animals[i].GetComponent<AnimalController>().condition == 0)
                animals[i].GetComponent<Button>().interactable = true;
        for (int i = 0; i < animals.Length; i++)
            if (animals[i].activeSelf == true)  //rabotaet li....?
                return;
        //animaciya 
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].SetActive(true);
            animals[i].GetComponent<Button>().interactable = true;
            animals[i].GetComponent<AnimalController>().condition = 2;
        }
    }

    public void CheckEndDay()
    {
        for (int i = 0; i < animals.Length; i++)
            if (animals[i].GetComponent<AnimalController>().condition != 3)
                return;
        for (int i = 0; i < animals.Length; i++)
            animals[i].SetActive(false);
        StartCoroutine(WaitingSun());
    }
    private IEnumerator WaitingSun()
    {
        yield return new WaitUntil(() => !FindObjectOfType<MonologueManager>().isInMonologue);
        sun.SetActive(true);
        yield return new WaitForSeconds(2.1f);
        sun.SetActive(false);
        int sum = 0;
        for (int i = 0; i < Info.inventory.Length; i++)
            if (Info.inventory[i] > 0)
                sum++;
        if (sum > 3)
        {
            NewNight();
        }
        else
        {
            FindObjectOfType<MonologueManager>().StartMonologue(new Monologue("NotEnoughIngredients"));
            yield return new WaitUntil(() => !FindObjectOfType<MonologueManager>().isInMonologue);
            NewDay(0);
        }
    }

    void NewNight()
    {
        nightCanvas.SetActive(true);
        dayCanvas.SetActive(false);
        claudron.GetComponent<Button>().interactable = true;
    }
    public void NewDay(int baf)
    {
        nightCanvas.SetActive(false);
        dayCanvas.SetActive(true);
        for (int i = 0; i < animals.Length; i++)
        {
            animals[i].SetActive(true);
            animals[i].GetComponent<Button>().interactable = true;
            animals[i].GetComponent<AnimalController>().condition = 0;
        }
        if (baf>0)
        {
            int buffAnimalNumb = Random.Range(0, int.MaxValue) % 4;
            //rasskazyat' 4to jivotnoe fabnyto
            animals[buffAnimalNumb].GetComponent<AnimalController>().Baff = baf;
        }
    }

    public void EndGame()
    {
        //animation maybe??
        Monologue end = new Monologue("End");
        FindObjectOfType<MonologueManager>().StartMonologue(end);
        nightCanvas.SetActive(false);
        dayCanvas.SetActive(true);
        endCanvas.SetActive(true);
        for (int i = 0; i < animals.Length; i++)
            animals[i].SetActive(false);
    }
}
