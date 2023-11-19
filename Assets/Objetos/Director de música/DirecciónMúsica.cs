using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónMúsica : MonoBehaviour
{
    AudioSource audioSource;
    [HideInInspector] public int totalPulsos;
    float tiempoCanción = 0f;
    public List<float> marcasTiempo;
    public List<float> duraciónPulsos;
    List<float> marcasTiempo_copia;
    List<float> duraciónPulsos_copia;
    // Corrutinas
    public IEnumerator DirigirMúsica;
    // Eventos
    public Action AlAlcanzarMarca;
    public Action AlComenzarMúsica;
    public Action AlPararMúsica;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        marcasTiempo_copia = new List<float>();
        duraciónPulsos_copia = new List<float>();
        marcasTiempo_copia = marcasTiempo;
        duraciónPulsos_copia = duraciónPulsos;
        DirigirMúsica = _DirigirMúsica();
    }
    public void ComenzarMúsica()
    {
        StartCoroutine(DirigirMúsica);
    }
    IEnumerator _DirigirMúsica()
    {
        audioSource.Play();
        while (audioSource.isPlaying == true)
        {
            if (audioSource.time >= marcasTiempo_copia[0] - duraciónPulsos_copia[0])
            {
                DirecciónJuego.direcciónMúsica.AlAlcanzarMarca();
                marcasTiempo_copia.RemoveAt(0);
                duraciónPulsos_copia.RemoveAt(0);
            }
            yield return null;
        }
    }
    IEnumerator PararMúsica()
    {
        yield return null;
    }
}
