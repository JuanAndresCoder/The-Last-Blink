using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsación : MonoBehaviour
{
    SeñalRitmoMaster señalRitmoMaster;
    bool esVálida;
    // Corrutinas
    IEnumerator EsperarPulsación;
    void Start()
    {
        señalRitmoMaster = GetComponent<SeñalRitmoMaster>();
        EsperarPulsación = _EsperarPulsación();
        StartCoroutine(EsperarPulsación);
    }
    IEnumerator _EsperarPulsación()
    {
        float pulsosPorSegundo = señalRitmoMaster.duraciónPulso;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * señalRitmoMaster.margenError / 100f);
        float tiempoExtra = pulsosPorSegundo + (pulsosPorSegundo * señalRitmoMaster.margenError / 100f); ;
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = true;
        while (tiempoActual < tiempoExtra)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = false;
        DirecciónJuego.instanciaciónSeñales.ComprobarValidez();
    }
    public bool ObtenerValidez()
    {
        StopCoroutine(EsperarPulsación);
        return esVálida;
    }
    void OnDisable()
    {
        StopCoroutine(EsperarPulsación);
    }
}
