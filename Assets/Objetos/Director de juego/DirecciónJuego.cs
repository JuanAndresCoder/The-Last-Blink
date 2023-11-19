using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static DirecciónJuego main;


    public InstanciaciónSeñales instanciaciónSeñales;

    public static DirecciónMúsica direcciónMúsica;

    private void Awake()
    {
        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        direcciónMúsica.enabled = true;

        instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        instanciaciónSeñales.enabled = true;

        main = this;
    }

    void Start()
    {
        StartCoroutine(direcciónMúsica.ComenzarMúsica);
    }
}
