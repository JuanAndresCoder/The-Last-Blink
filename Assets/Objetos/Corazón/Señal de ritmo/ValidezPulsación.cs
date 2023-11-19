using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsación : MonoBehaviour
{
    SeñalRitmoMaster señalRitmoMaster;
    bool esVálida;
    public Action AlAcertarPulsación;
    public Action AlFallarPulsación;
    // Corrutinas
    IEnumerator EsperarPulsación;
    void Start()
    {
        señalRitmoMaster = GetComponent<SeñalRitmoMaster>();
        // Suscripciones al acertar
        AlAcertarPulsación += Destruirse;
        AlAcertarPulsación += DarAcierto;
        // Suscripciones al fallar
        AlFallarPulsación += Destruirse;
        AlFallarPulsación += DarFallo;
        EsperarPulsación = _EsperarPulsación();
        StartCoroutine(EsperarPulsación);
    }
    IEnumerator _EsperarPulsación()
    {
        float tiempoInvalidez = señalRitmoMaster.duraciónPulso - (señalRitmoMaster.duraciónPulso + señalRitmoMaster.margenError / 100);
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = true;
        while (tiempoActual < señalRitmoMaster.duraciónPulso)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esVálida = false;
        AlFallarPulsación();
    }
    void DarAcierto()
    {
        Debug.Log("Acertaste!");
    }
    void DarFallo()
    {
        Debug.Log("Fallaste!");
    }
    void Destruirse()
    {
        DirecciónJuego.instanciaciónSeñales.listaSeñales.Remove(señalRitmoMaster);
        Destroy(gameObject);
    }
    public void ComprobarValidez()
    {
        StopCoroutine(EsperarPulsación);
        if (esVálida == true)
        {
            AlAcertarPulsación();
        }
        else { AlFallarPulsación(); }
    }
    void OnDestroy()
    {
        AlAcertarPulsación -= Destruirse;
        AlAcertarPulsación -= DarAcierto;
        AlFallarPulsación -= DarFallo;
        AlFallarPulsación -= Destruirse;
    }
}
