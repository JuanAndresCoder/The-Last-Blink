using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Direcci�nM�sica : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public int totalPulsos;
    [SerializeField] Informaci�nTema informaci�nTema;
    int n�meroMarca;
    float volumen;
    Coroutine DirigirM�sica;
    // Eventos
    public Action AlTerminarIntro;
    public Action AlAlcanzarMarca;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumen = audioSource.volume;
    }
    void ComenzarM�sica()
    {
        DirigirM�sica = StartCoroutine(_DirigirM�sica());
    }
    void PararM�sica()
    {
        StopCoroutine(DirigirM�sica);
        StartCoroutine(_BajarVolumen());
    }
    public float ObtenerDuraci�nPulso()
    {
        return informaci�nTema.informaci�nMarcas[n�meroMarca].duraci�nPulso;
    } 
    void OnEnable()
    {
        Direcci�nJuego.AlMontarNivel += ComenzarM�sica;
        Direcci�nJuego.AlFallarPulsaci�n += PararM�sica;
    }
    void OnDisable()
    {
        Direcci�nJuego.AlMontarNivel -= ComenzarM�sica;
        Direcci�nJuego.AlFallarPulsaci�n -= PararM�sica;
    }
    IEnumerator _DirigirM�sica()
    {
        float tiempoAparici�n = 0;
        audioSource.Play();
        // Que no se olvide la intro
        AlTerminarIntro();
        while (true)
        {
            tiempoAparici�n = 
                informaci�nTema.informaci�nMarcas[n�meroMarca].tiempoMarca - 
                informaci�nTema.informaci�nMarcas[n�meroMarca].duraci�nPulso;
            if (audioSource.time >= tiempoAparici�n)
            {
                Direcci�nJuego.direcci�nM�sica.AlAlcanzarMarca();
                n�meroMarca++;
            }
            yield return null;
        }
    }
    IEnumerator _BajarVolumen()
    {
        n�meroMarca = 0;
        float tiempoAnimaci�n = Direcci�nJuego.animaci�nUI.ObtenerTiempoAnimaci�n();
        float velocidadBajada = audioSource.volume / tiempoAnimaci�n;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= velocidadBajada * Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = volumen;
    }
}
