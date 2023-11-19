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
        float pulsosPorSegundo = 1f;
        float tiempoInvalidez = pulsosPorSegundo - (pulsosPorSegundo * margenError / 100);
        float tiempoActual = 0;

        esVálida = true;

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
        esVálida = false;
        AlFallarPulsación();
    }
    public bool ComprobarValidez()
    {

        if (esVálida == true)
        {
            Debug.Log(esVálida);
            AlAcertarPulsación();
        }
        else { AlFallarPulsación(); }

        return esVálida;
    }
    void Destruir()
    {
        DirecciónJuego.instanciaciónSeñales.listaSeñales.RemoveAt(0);

        Destroy(gameObject);
    }
    void OnDestroy()
    {
        AlAcertarPulsación -= Destruir;
        AlFallarPulsación -= Destruir;
    }
}
