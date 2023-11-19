using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ValidezPulsaci�n : MonoBehaviour
{
    Se�alRitmoMaster se�alRitmoMaster;
    bool esV�lida;
    public Action AlAcertarPulsaci�n;
    public Action AlFallarPulsaci�n;
    // Corrutinas
    IEnumerator EsperarPulsaci�n;
    void Start()
    {
        se�alRitmoMaster = GetComponent<Se�alRitmoMaster>();
        // Suscripciones al acertar
        AlAcertarPulsaci�n += Destruirse;
        AlAcertarPulsaci�n += DarAcierto;
        // Suscripciones al fallar
        AlFallarPulsaci�n += Destruirse;
        AlFallarPulsaci�n += DarFallo;
        EsperarPulsaci�n = _EsperarPulsaci�n();
        StartCoroutine(EsperarPulsaci�n);
    }
    IEnumerator _EsperarPulsaci�n()
    {
        float tiempoInvalidez = se�alRitmoMaster.duraci�nPulso - (se�alRitmoMaster.duraci�nPulso + se�alRitmoMaster.margenError / 100);
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = true;
        while (tiempoActual < se�alRitmoMaster.duraci�nPulso)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = false;
        AlFallarPulsaci�n();
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
        Direcci�nJuego.instanciaci�nSe�ales.listaSe�ales.Remove(se�alRitmoMaster);
        Destroy(gameObject);
    }
    public void ComprobarValidez()
    {
        StopCoroutine(EsperarPulsaci�n);
        if (esV�lida == true)
        {
            AlAcertarPulsaci�n();
        }
        else { AlFallarPulsaci�n(); }
    }
    void OnDestroy()
    {
        AlAcertarPulsaci�n -= Destruirse;
        AlAcertarPulsaci�n -= DarAcierto;
        AlFallarPulsaci�n -= DarFallo;
        AlFallarPulsaci�n -= Destruirse;
    }
}
