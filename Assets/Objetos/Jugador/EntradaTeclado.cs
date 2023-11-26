using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntradaTeclado : MonoBehaviour
{
    Action AlPulsar;
    void OnEnable()
    {
        AlPulsar += DirecciónJuego.instanciaciónSeñales.ComprobarValidez;
    }
    void OnDisable()
    {
        AlPulsar -= DirecciónJuego.instanciaciónSeñales.ComprobarValidez;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            AlPulsar();
        }
    }
}