using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsación : MonoBehaviour
{
    SeñalRitmoMaster señalRitmoMaster;
    [HideInInspector] public int margenError;
    bool esVálida;
    public Action AlAcertarPulsación;
    public Action AlFallarPulsación;
    // Corrutinas
    IEnumerator EsperarPulsación;
    void Start()
    {
        señalRitmoMaster = GetComponent<SeñalRitmoMaster>();
        AlAcertarPulsación += Destruir;
        AlFallarPulsación += Destruir;
        EsperarPulsación = _EsperarPulsación();
        StartCoroutine(EsperarPulsación);
    }
    IEnumerator _EsperarPulsación()
    {
        float pulsosPorSegundo = DirecciónJuego.direcciónMúsica.pulsosPorSegundo;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * margenError / 100);
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = true;
        while (tiempoActual < pulsosPorSegundo)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = false;
        AlFallarPulsación();
    }
    public void ComprobarValidez()
    {
        if (esVálida == true)
        {
            AlAcertarPulsación();
        }
        else { AlFallarPulsación(); }
    }
    void Destruir()
    {
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        AlAcertarPulsación -= Destruir;
        AlFallarPulsación -= Destruir;
    }
}
