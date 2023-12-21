using System;
using System.Collections;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static InstanciaciónSeñales instanciaciónSeñales;
    public static DirecciónMúsica direcciónMúsica;
    public static AnimaciónUI animaciónUI;
    static EntradaTeclado entradaTeclado;
    static DirecciónPersonajes direcciónPersonajes;
    // Eventos
    public static Action AlPulsarBotónInicio;
    public static Action AlMontarNivel;
    public static Action AlFallarPulsación;
    void Start()
    {
        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        entradaTeclado = FindObjectOfType<EntradaTeclado>(true);
        instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        direcciónPersonajes = FindObjectOfType<DirecciónPersonajes>(true);
        animaciónUI =  FindObjectOfType<AnimaciónUI>(true);
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
        AlPulsarBotónInicio += ComenzarJuego;
        AlFallarPulsación += TerminarJuego;
    }
    void OnDisable()
    {
        AlPulsarBotónInicio -= ComenzarJuego;
        AlFallarPulsación -= TerminarJuego;
    }
    IEnumerator MontarNivel()
    {
        if (instanciaciónSeñales.enabled == false) 
        {
            instanciaciónSeñales.enabled = true;
            yield return new WaitUntil(() => direcciónMúsica.AlAlcanzarMarca != null);
            direcciónPersonajes.enabled = true;
            yield return new WaitUntil(() => direcciónMúsica.AlTerminarIntro != null);
        }
        animaciónUI.AbrirOjos();
        yield return new WaitUntil(() => 
            animaciónUI.animator.IsInTransition(0) == false);
        yield return new WaitForSeconds(animaciónUI.ObtenerTiempoAnimación());
        AlMontarNivel();
        entradaTeclado.enabled = true;
    }
    IEnumerator DesmontarNivel()
    {
        entradaTeclado.enabled = false;
        yield return new WaitForSeconds(animaciónUI.ObtenerTiempoAnimación());
    }
}
