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
        float pulsosPorSegundo = Direcci�nJuego.direcci�nM�sica.pulsosPorSegundo;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * margenError / 100);
        float tiempoActual = 0;
        while (tiempoActual < tiempoInvalidez)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = true;
        while (tiempoActual < pulsosPorSegundo)
        {
            tiempoActual += Time.deltaTime;
            yield return null;
        }
        esV�lida = false;
        AlFallarPulsaci�n();
    }
    public void ComprobarValidez()
    {
        if (esV�lida == true)
        {
            AlAcertarPulsaci�n();
        }
        else { AlFallarPulsaci�n(); }
    }
    void Destruir()
    {
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        AlAcertarPulsaci�n -= Destruir;
        AlFallarPulsaci�n -= Destruir;
    }
}
