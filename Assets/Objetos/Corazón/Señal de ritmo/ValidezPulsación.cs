using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsaci�n : MonoBehaviour
{
    Se�alRitmoMaster se�alRitmoMaster;
    [HideInInspector] public int margenError;
    bool esV�lida;
    public Action AlAcertarPulsaci�n;
    public Action AlFallarPulsaci�n;

    // Corrutinas
    IEnumerator EsperarPulsaci�n;
    void Start()
    {
        se�alRitmoMaster = GetComponent<Se�alRitmoMaster>();
        AlAcertarPulsaci�n += Destruir;
        AlFallarPulsaci�n += Destruir;
        EsperarPulsaci�n = _EsperarPulsaci�n();
        StartCoroutine(EsperarPulsaci�n);
    }
    IEnumerator _EsperarPulsaci�n()
    {
        float pulsosPorSegundo = 1f;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * margenError / 100);
        float tiempoActual = 0;

        esV�lida = true;

        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        while (tiempoActual < pulsosPorSegundo)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = false;
        AlFallarPulsaci�n();
    }
    public bool ComprobarValidez()
    {

        if (esV�lida == true)
        {
            Debug.Log(esV�lida);
            AlAcertarPulsaci�n();
        }
        else { AlFallarPulsaci�n(); }

        return esV�lida;
    }
    void Destruir()
    {
        Direcci�nJuego.instanciaci�nSe�ales.listaSe�ales.RemoveAt(0);

        Destroy(gameObject);
    }
    void OnDestroy()
    {
        AlAcertarPulsaci�n -= Destruir;
        AlFallarPulsaci�n -= Destruir;
    }
}
