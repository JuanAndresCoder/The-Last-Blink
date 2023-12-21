using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsaci�n : MonoBehaviour
{
    Se�alRitmoMaster se�alRitmoMaster;
    bool esV�lida;
    [HideInInspector] public bool permitirDestrucci�n;
    // Corrutinas
    Coroutine EsperarPulsaci�n;
    void Start()
    {
        se�alRitmoMaster = GetComponent<Se�alRitmoMaster>();
        EsperarPulsaci�n = StartCoroutine(_EsperarPulsaci�n());
    }
    IEnumerator _EsperarPulsaci�n()
    {
        float pulsosPorSegundo = se�alRitmoMaster.duraci�nPulso;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * se�alRitmoMaster.margenError / 100f);
        float tiempoExtra = pulsosPorSegundo + (pulsosPorSegundo * se�alRitmoMaster.margenError / 100f); ;
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = true;
        while (tiempoActual < tiempoExtra)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        if (permitirDestrucci�n == true) { Destroy(gameObject); }
        else 
        {
            esV�lida = false;
            Direcci�nJuego.instanciaci�nSe�ales.ComprobarValidez();
        }
    }
    public bool ObtenerValidez()
    {
        StopCoroutine(EsperarPulsaci�n);
        return esV�lida;
    }
}
