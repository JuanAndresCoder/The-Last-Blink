using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónMúsica : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public int totalPulsos;
    [SerializeField] InformaciónTema informaciónTema;
    int númeroMarca;
    float volumen;
    Coroutine DirigirMúsica;
    // Eventos
    public Action AlTerminarIntro;
    public Action AlAlcanzarMarca;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        volumen = audioSource.volume;
    }
    void ComenzarMúsica()
    {
        DirigirMúsica = StartCoroutine(_DirigirMúsica());
    }
    void PararMúsica()
    {
        StopCoroutine(DirigirMúsica);
        StartCoroutine(_BajarVolumen());
    }
    public float ObtenerDuraciónPulso()
    {
        return informaciónTema.informaciónMarcas[númeroMarca].duraciónPulso;
    } 
    void OnEnable()
    {
        DirecciónJuego.AlMontarNivel += ComenzarMúsica;
        DirecciónJuego.AlFallarPulsación += PararMúsica;
    }
    void OnDisable()
    {
        DirecciónJuego.AlMontarNivel -= ComenzarMúsica;
        DirecciónJuego.AlFallarPulsación -= PararMúsica;
    }
    IEnumerator _DirigirMúsica()
    {
        float tiempoAparición = 0;
        audioSource.Play();
        // Que no se olvide la intro
        AlTerminarIntro();
        while (true)
        {
            tiempoAparición = 
                informaciónTema.informaciónMarcas[númeroMarca].tiempoMarca - 
                informaciónTema.informaciónMarcas[númeroMarca].duraciónPulso;
            if (audioSource.time >= tiempoAparición)
            {
                DirecciónJuego.direcciónMúsica.AlAlcanzarMarca();
                númeroMarca++;
            }
            yield return null;
        }
    }
    IEnumerator _BajarVolumen()
    {
        númeroMarca = 0;
        float tiempoAnimación = DirecciónJuego.animaciónUI.ObtenerTiempoAnimación();
        float velocidadBajada = audioSource.volume / tiempoAnimación;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= velocidadBajada * Time.deltaTime;
            yield return null;
        }
        audioSource.Stop();
        audioSource.volume = volumen;
    }
}
