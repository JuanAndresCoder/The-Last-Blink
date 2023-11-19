using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Direcci�nM�sica : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public int totalPulsos;
    float tiempoCanci�n = 0f;
    public List<float> marcasTiempo;
    public List<float> duraci�nPulsos;
    List<float> marcasTiempo_copia;
    List<float> duraci�nPulsos_copia;
    // Corrutinas
    public IEnumerator DirigirM�sica;
    // Eventos
    public Action AlAlcanzarMarca;
    public Action AlComenzarM�sica;
    public Action AlPararM�sica;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        marcasTiempo_copia = new List<float>();
        duraci�nPulsos_copia = new List<float>();
        marcasTiempo_copia = marcasTiempo;
        duraci�nPulsos_copia = duraci�nPulsos;
        DirigirM�sica = _DirigirM�sica();
    }
    public void ComenzarM�sica()
    {
        StartCoroutine(DirigirM�sica);
    }
    IEnumerator _DirigirM�sica()
    {
        audioSource.Play();
        while (audioSource.isPlaying == true)
        {
            if (audioSource.time >= marcasTiempo_copia[0] - duraci�nPulsos_copia[0])
            {
                Direcci�nJuego.direcci�nM�sica.AlAlcanzarMarca();
                marcasTiempo_copia.RemoveAt(0);
                duraci�nPulsos_copia.RemoveAt(0);
            }
            yield return null;
        }
    }
    IEnumerator PararM�sica()
    {
        yield return null;
    }
}
