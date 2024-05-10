using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public TMP_InputField quantityText;
    public GameObject dancingPrefab;
    public GameObject itemPrefab;
    public float modelMinDistance;
    public float modelMaxDistance;

    public TextMeshProUGUI performanceText;

    public Camera mainCamera;
    private float rotationSpeed = 10;
    private float minRotationSpeed = -100;
    private float maxRotationSpeed = 100;
    public TextMeshProUGUI speedText;

    private List<GameObject> dancingPeople = new List<GameObject>();
    private List<GameObject> items = new List<GameObject>();
    private string[] animationNames = new string[] { "Hip Hop Dancing", "Macarena Dance", "Snake Hip Hop Dance", "Ymca Dance", "Twist Dance" };

    private float lastInterval = 0;
    private float updateInterval = 0.5f;
    private int frames = 0;
    private float fps;

    public void SpawnDancingPeople()
    {
        for (int i = 0; i < dancingPeople.Count; i++)
        {
            Destroy(dancingPeople[i]);
        }
        dancingPeople = new List<GameObject>();
        int quantity = 0;
        int.TryParse(Regex.Replace(quantityText.text, @"[^\d]", ""), out quantity);
        for (int i = 0; i < quantity; i++)
        {
            float angle = i * Mathf.PI * 2 / quantity;
            float radius = Random.Range(modelMinDistance, modelMaxDistance);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 spawnPosition = new Vector3(x, 0f, z);
            GameObject anciana = Instantiate(dancingPrefab, spawnPosition, Quaternion.identity);
            anciana.transform.LookAt(Vector3.zero);
            anciana.GetComponent<Animator>().Play(animationNames[Random.Range(0, animationNames.Length)]);
            dancingPeople.Add(anciana);
        }
    }

    public void SpawnItems()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Destroy(items[i]);
        }
        items = new List<GameObject>();
        int quantity = 0;
        int.TryParse(Regex.Replace(quantityText.text, @"[^\d]", ""), out quantity);
        for (int i = 0; i < quantity; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f);
            float radius = Random.Range(modelMinDistance, modelMaxDistance);
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            Vector3 spawnPosition = new Vector3(x, 0f, z);
            GameObject item = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            item.transform.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
            items.Add(item);
        }
    }

    private void Update()
    {
        UpdateCamera();
        UpdatePerformance();
    }

    private void UpdateCamera()
    {
        mainCamera.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void UpdatePerformance()
    {
        frames++;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = frames / (timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;

            performanceText.text = "FPS: " + fps.ToString() + '\n' + "Polígonos: " + CountPolygons();
        }
    }

    public void AddCameraSpeed(float n)
    {
        rotationSpeed = Mathf.Clamp(rotationSpeed + n, minRotationSpeed, maxRotationSpeed);
        speedText.text = "Speed: " + rotationSpeed;
    }

    private int CountPolygons()
    {
        int totalDancing = 0;
        for (int i = 0; i < dancingPeople.Count; i++)
        {
            if (IsInCamera(dancingPeople[i]))
            {
                totalDancing++;
            }
        }
        int totalItems = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (IsInCamera(items[i]))
            {
                totalItems++;
            }
        }
        return totalDancing * 49112 + totalItems * 90566;
    }

    private bool IsInCamera(GameObject g)
    {
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(g.transform.position);
        return (viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z >= 0);
    }
}
