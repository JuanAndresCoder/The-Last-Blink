using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static DirecciónMúsica direcciónMúsica;
    void Start()
    {
        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        StartCoroutine(ComenzarJuego());
    }
    IEnumerator ComenzarJuego()
    {
        direcciónMúsica.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.ComenzarMúsica != null);
        InstanciaciónSeñales instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        instanciaciónSeñales.enabled = true;
        yield return new WaitUntil(() => instanciaciónSeñales.InstanciarSeñales != null);
        StartCoroutine(direcciónMúsica.ComenzarMúsica);
    }
}
