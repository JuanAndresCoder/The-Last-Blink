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
    // Corrutinas
    IEnumerator DirigirM�sica;
    IEnumerator BajarVolumen;
    // Eventos
    public Action AlTerminarIntro;
    public Action AlAlcanzarMarca;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        DirigirM�sica = _DirigirM�sica();
        BajarVolumen = _BajarVolumen();
    }
    void ComenzarM�sica()
    {
        StartCoroutine(DirigirM�sica);
    }
    void PararM�sica()
    {
        StartCoroutine(BajarVolumen);
    }
    public float ObtenerDuraci�nPulso()
    {
        return informaci�nTema.informaci�nMarcas[n�meroMarca].duraci�nPulso;
    } 
    void OnEnable()
    {
        Direcci�nJuego.AlComenzarJuego += ComenzarM�sica;
        Direcci�nJuego.AlFallarPulsaci�n += PararM�sica;
    }
    void OnDisable()
    {
        Direcci�nJuego.AlComenzarJuego -= ComenzarM�sica;
        Direcci�nJuego.AlFallarPulsaci�n -= PararM�sica;
    }
    IEnumerator _DirigirM�sica()
    {
        float tiempoAparici�n = 0;
        yield return new WaitForSeconds(1);
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
        StopCoroutine(DirigirM�sica);
        n�meroMarca = 0;
        float tiempoAnimaci�n = Animaci�nUI.ObtenerTiempoAnimaci�n();
        float velocidadBajada = audioSource.volume / tiempoAnimaci�n;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= velocidadBajada * Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
    }
}
