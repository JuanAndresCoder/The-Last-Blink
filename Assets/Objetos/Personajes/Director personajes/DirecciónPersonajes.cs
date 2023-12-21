using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónPersonajes : MonoBehaviour
{
    List<DiálogoPersonaje> listaPersonajes;
    [HideInInspector] public Animator animator;
    [HideInInspector] public int númeroPersonaje;
    void Start()
    {
        animator = GetComponent<Animator>();
        listaPersonajes = new List<DiálogoPersonaje>();
        listaPersonajes.AddRange(GetComponentsInChildren<DiálogoPersonaje>());
    }
    void IniciarConversación()
    {
        animator.enabled = true;
    }
    void PararConversación()
    {
        animator.enabled = false;
        foreach (DiálogoPersonaje personaje in listaPersonajes) 
        {
            personaje.Reiniciar();
        }
    }
    public void IniciarDiálogo()
    {
        listaPersonajes[númeroPersonaje].Hablar();
    }
    void OnEnable()
    {
        DirecciónJuego.AlFallarPulsación += PararConversación;
        DirecciónJuego.direcciónMúsica.AlTerminarIntro += IniciarConversación;
    }
    void OnDisable()
    {
        DirecciónJuego.AlFallarPulsación -= PararConversación;
        DirecciónJuego.direcciónMúsica.AlTerminarIntro -= IniciarConversación;
    }
}
