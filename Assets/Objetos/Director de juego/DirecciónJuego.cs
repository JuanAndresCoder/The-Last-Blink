using System;
using System.Collections;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static InstanciaciónSeñales instanciaciónSeñales;
    public static DirecciónMúsica direcciónMúsica;
    static EntradaTeclado entradaTeclado;
    static DirecciónPersonajes direcciónPersonajes;
    // Eventos
    public static Action AlComenzarJuego;
    public static Action AlFallarPulsación;
    void Start()
    {
        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        entradaTeclado = FindObjectOfType<EntradaTeclado>(true);
        instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        direcciónPersonajes = FindObjectOfType<DirecciónPersonajes>(true);
        ComenzarJuego();
    }
    void ComenzarJuego()
    {
        StartCoroutine(MontarNivel());
    }
    void TerminarJuego()
    {
        StartCoroutine(DesmontarNivel());
    }
    void OnEnable()
    {
        AlFallarPulsación += TerminarJuego;
    }
    void OnDisable()
    {
        AlFallarPulsación -= TerminarJuego;
    }
    IEnumerator MontarNivel()
    {
        direcciónMúsica.enabled = true;
        instanciaciónSeñales.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.AlAlcanzarMarca != null);
        yield return new WaitForSeconds(1);
        direcciónPersonajes.enabled = true;
        entradaTeclado.enabled = true;
        AlComenzarJuego();
    }
    IEnumerator DesmontarNivel()
    {
        entradaTeclado.enabled = false;
        yield return new WaitForSeconds(AnimaciónUI.ObtenerTiempoAnimación());
        Debug.Log("Game Over");
    }
}
