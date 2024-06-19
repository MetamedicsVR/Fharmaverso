using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Game_FindPairs : MonoBehaviour
{
    public List<Interactable2D> cardsList;
    public List<Sprite> itemSprites;

    public GameObject cards;
    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI pointsText;

    private int firstSelected = -1;
    private int lastSelected = -1;
    private bool firstWasDiscovered;
    private List<int> cardValues;
    private List<bool> discoveredCards;
    private int found;
    private int points;
    private bool blocked;

    private void OnEnable()
    {
        startScreen.SetActive(true);
        endScreen.SetActive(false);
        cards.SetActive(false);
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        cards.SetActive(true);
        discoveredCards = Enumerable.Repeat(false, cardsList.Count).ToList();
        for (int i = 0; i < cardsList.Count; i++)
        {
            cardsList[i].transform.eulerAngles = Vector3.zero;
        }
        found = 0;
        points = 0;
        blocked = false;
        for (int i = 0; i < cardsList.Count; i++)
        {
            cardsList[i].transform.eulerAngles = Vector3.zero;
        }
        //Set sprites
        SetColors();
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
    }

    public void SelectedCard(Interactable2D card)
    {
        if (!blocked && card.transform.localEulerAngles.y == 0)
        {
            card.transform.localEulerAngles = new Vector3(0, 180, 0);
            lastSelected = cardsList.IndexOf(card);
            if (firstSelected >= 0)
            {
                if (cardValues[lastSelected] == cardValues[firstSelected])
                {
                    found++;
                    points += 3;
                    pointsText.text = "Points: " + points;
                    if (found == 6)
                    {
                        endScreen.SetActive(true);
                    }
                    firstSelected = -1;
                }
                else
                {
                    if (firstWasDiscovered || discoveredCards[lastSelected])
                    {
                        points--;
                        pointsText.text = "Points: " + points;
                    }
                    blocked = true;
                    Invoke(nameof(Unblock), 2);
                }
            }
            else
            {
                firstSelected = lastSelected;
                firstWasDiscovered = discoveredCards[firstSelected];
            }
            discoveredCards[lastSelected] = true;
        }
    }

    private void Unblock()
    {
        cardsList[firstSelected].transform.localEulerAngles = Vector3.zero;
        cardsList[lastSelected].transform.localEulerAngles = Vector3.zero;
        firstSelected = -1;
        lastSelected = -1;
        blocked = false;
    }

    private void SetColors()
    {
        List<Color> colors = new List<Color>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    colors.Add(new Color(i * 0.5f, j * 0.5f, k * 0.5f));
                }
            }
        }
        cardValues = Enumerable.Repeat(0, cardsList.Count).ToList();
        List<Interactable2D> unpairedCards = new List<Interactable2D>(cardsList);
        Interactable2D card_1;
        Interactable2D card_2;
        Color randomColor;
        int value = 0;
        while (unpairedCards.Count > 0)
        {
            card_1 = unpairedCards[Random.Range(0, unpairedCards.Count)];
            unpairedCards.Remove(card_1);
            card_2 = unpairedCards[Random.Range(0, unpairedCards.Count)];
            unpairedCards.Remove(card_2);
            value++;
            cardValues[cardsList.IndexOf(card_1)] = value;
            cardValues[cardsList.IndexOf(card_2)] = value;
            randomColor = colors[Random.Range(0, colors.Count)];
            colors.Remove(randomColor);
            card_1.transform.GetChild(0).GetComponent<MeshRenderer>().material = new Material(card_1.GetComponent<MeshRenderer>().material);
            card_1.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = randomColor;
            card_2.transform.GetChild(0).GetComponent<MeshRenderer>().material = new Material(card_2.GetComponent<MeshRenderer>().material);
            card_2.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = randomColor;
        }
    }
}
