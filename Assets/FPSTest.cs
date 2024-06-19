using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSTest : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI totalCubesText;
    public List<GameObject> cubes;

    private float fpsUpdateTime = 1;
    private float timeFromLastFrame = 0;
    private int frames = 0;

    private void Start()
    {
        AddCubes(39);
    }

    private void Update()
    {
        frames++;
        timeFromLastFrame += Time.deltaTime;
        if (timeFromLastFrame >= fpsUpdateTime)
        {
            fpsText.text = "FPS: " + (frames / timeFromLastFrame).ToString("F2");
            frames = 0;
            timeFromLastFrame = 0;
        }

        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].transform.position.y < -8)
            {
                cubes[i].transform.position = new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 9f), Random.Range(0f, 10f));
                cubes[i].GetComponent<Rigidbody>().velocity = Random.insideUnitCircle;
            }
        }
    }

    public void AddCubes(int n)
    {
        for (int i = 0; i < n; i++)
        {
            GameObject cube = Instantiate(cubes[0], new Vector3(Random.Range(-8f, 8f), Random.Range(7f, 9f), Random.Range(0f, 10f)), Quaternion.identity);
            cube.GetComponent<Rigidbody>().velocity = Random.insideUnitCircle;
            cube.GetComponent<MeshRenderer>().material = new Material(cube.GetComponent<MeshRenderer>().material);
            cube.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            cubes.Add(cube);
        }
        totalCubesText.text = "Cubes: " + cubes.Count;
    }

    public void RemoveCube()
    {
        while (cubes.Count > 1)
        {
            GameObject cube = cubes[0];
            cubes.RemoveAt(0);
            Destroy(cube);
        }
        totalCubesText.text = "Cubes: " + cubes.Count;
    }
}
