using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static InstanciaciónSeñales instanciaciónSeñales;
    public static DirecciónMúsica direcciónMúsica;
    void Awake()
    {
        StartCoroutine(ComenzarJuego());
    }
    IEnumerator ComenzarJuego()
    {
        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        direcciónMúsica.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.DirigirMúsica != null);
        instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        instanciaciónSeñales.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.AlAlcanzarMarca != null);
        direcciónMúsica.ComenzarMúsica();
    }
}
