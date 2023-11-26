using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsaci�n : MonoBehaviour
{
    Se�alRitmoMaster se�alRitmoMaster;
    bool esV�lida;
    // Corrutinas
    IEnumerator EsperarPulsaci�n;
    void Start()
    {
        se�alRitmoMaster = GetComponent<Se�alRitmoMaster>();
        EsperarPulsaci�n = _EsperarPulsaci�n();
        StartCoroutine(EsperarPulsaci�n);
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
        esV�lida = false;
        Direcci�nJuego.instanciaci�nSe�ales.ComprobarValidez();
    }
    public bool ObtenerValidez()
    {
        StopCoroutine(EsperarPulsaci�n);
        return esV�lida;
    }
    void OnDisable()
    {
        StopCoroutine(EsperarPulsaci�n);
    }
}
