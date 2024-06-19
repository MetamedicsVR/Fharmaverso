using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [Header("Menu")]
    public GameObject clase;
    public GameObject libros;
    public GameObject poster_Izquierda;
    public GameObject poster_Derecha;
    public GameObject medicado_o_entrenar;
    public GameObject solo_o_acompa�ado;
    public GameObject comic_1_de_7;
    public GameObject comic_acompa�ado;
    public GameObject entrenar_medicaci�n;
    public GameObject satisfacci�n;

    [Header("Games")]
    public GameObject fallingObjects;
    public GameObject findPairs;

    private void Start()
    {
        CargarEscenario(clase);
    }

    public void CargarEscenario(GameObject escenario)
    {
        clase.SetActive(escenario == clase);
        libros.SetActive(escenario == libros);
        poster_Izquierda.SetActive(escenario == poster_Izquierda);
        poster_Derecha.SetActive(escenario == poster_Derecha);
        medicado_o_entrenar.SetActive(escenario == medicado_o_entrenar);
        solo_o_acompa�ado.SetActive(escenario == solo_o_acompa�ado);
        comic_1_de_7.SetActive(escenario == comic_1_de_7);
        comic_acompa�ado.SetActive(escenario == comic_acompa�ado);
        entrenar_medicaci�n.SetActive(escenario == entrenar_medicaci�n);
        satisfacci�n.SetActive(escenario == satisfacci�n);
        fallingObjects.SetActive(escenario == fallingObjects);
        findPairs.SetActive(escenario == findPairs);
    }
}
