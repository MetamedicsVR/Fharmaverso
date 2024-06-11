using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_FallingObjects : MonoBehaviour
{
    public Interactable2D basket;
    public List<Sprite> goodItems;
    public List<Sprite> badItems;
    public GameObject itemPrefab;

    public GameObject startScreen;
    public GameObject endScreen;
    public TextMeshProUGUI pointsText;

    [Header("Settings")]
    public float spawnTime = 1;
    public float fallingSpeed = 2;
    [Range(0, 1)]
    public float goodItemsRatio = 0.5f;
    public int pointsPerGoodItem = 10;
    public int pointsPerBadItem = -5;
    public int neededItems = 10;

    private Coroutine fallingCoroutine;
    private List<FallingItem> instancedItems = new List<FallingItem>();
    private int collectedItems;
    private int currentPoints;

    private void OnEnable()
    {
        basket.canInteract = false;
        startScreen.SetActive(true);
        endScreen.SetActive(false);
    }

    private void OnDisable()
    {
        if (fallingCoroutine != null)
        {
            StopCoroutine(fallingCoroutine);
        }
        DestroyAllItems();
    }

    public void StartGame()
    {
        collectedItems = 0;
        SetPoints(0);
        startScreen.SetActive(false);
        basket.canInteract = true;
        fallingCoroutine = StartCoroutine(Falling());
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
        basket.canInteract = false;
        if (fallingCoroutine != null)
        {
            StopCoroutine(fallingCoroutine);
        }
    }

    private IEnumerator Falling()
    {
        float timer = 0;
        while (true)
        {
            yield return null;
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                InstanceItem();
                timer -= spawnTime;
            }
            MoveItems();
        }
    }

    private void InstanceItem()
    {
        GameObject item = Instantiate(itemPrefab, new Vector3(Random.Range(-8f, 8f), 8, 0), Quaternion.identity);  //ALTURA
        bool isGoodItem = Random.Range(0f, 1f) < goodItemsRatio;
        item.GetComponent<FallingItem>().isGood = isGoodItem;
        item.GetComponent<MeshRenderer>().material = new Material(item.GetComponent<MeshRenderer>().material);
        if (isGoodItem)
        {
            //item.GetComponent<Image>().sprite = goodItems[Random.Range(0, goodItems.Count)];
            item.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        else
        {
            //item.GetComponent<Image>().sprite = badItems[Random.Range(0, badItems.Count)];
            item.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        instancedItems.Add(item.GetComponent<FallingItem>());
    }

    private void MoveItems()
    {
        for (int i = 0; i < instancedItems.Count; i++)
        {
            instancedItems[i].gameObject.transform.position -= Vector3.up * Time.deltaTime * fallingSpeed;
        }
        if (instancedItems.Count > 0 && instancedItems[0].gameObject.transform.position.y < -8) //ALTURA
        {
            DestroyItem(instancedItems[0]);
        }
    }

    public void GotItem(FallingItem item)
    {
        if (item.isGood)
        {
            SetPoints(currentPoints + pointsPerGoodItem);
            collectedItems++;
        }
        else
        {
            SetPoints(currentPoints + pointsPerBadItem);
        }
        DestroyItem(item);
        if (collectedItems >= neededItems)
        {
            EndGame();
        }
    }

    private void SetPoints(int points)
    {
        currentPoints = points;
        if (currentPoints < 0)
        {
            currentPoints = 0;
        }
        pointsText.text = "Points: " + points;
    }

    private void DestroyItem(FallingItem item)
    {
        GameObject itemObject = item.gameObject;
        instancedItems.Remove(item);
        Destroy(itemObject);
    }

    private void DestroyAllItems()
    {
        for (int i = instancedItems.Count - 1; i >= 0; i--)
        {
            DestroyItem(instancedItems[i]);
        }
    }
}
