using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsación : MonoBehaviour
{
    SeñalRitmoMaster señalRitmoMaster;
    bool esVálida;
    [HideInInspector] public bool permitirDestrucción;
    // Corrutinas
    Coroutine EsperarPulsación;
    void Start()
    {
        señalRitmoMaster = GetComponent<SeñalRitmoMaster>();
        EsperarPulsación = StartCoroutine(_EsperarPulsación());
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
        if (permitirDestrucción == true) { Destroy(gameObject); }
        else 
        {
            esVálida = false;
            DirecciónJuego.instanciaciónSeñales.ComprobarValidez();
        }
    }
    public bool ObtenerValidez()
    {
        StopCoroutine(EsperarPulsación);
        return esVálida;
    }
}
