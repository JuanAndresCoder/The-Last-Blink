using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntradaTeclado : MonoBehaviour
{
    Action AlPulsar;
    void OnEnable()
    {
        AlPulsar += Direcci�nJuego.instanciaci�nSe�ales.ComprobarValidez;
    }
    void OnDisable()
    {
        AlPulsar -= Direcci�nJuego.instanciaci�nSe�ales.ComprobarValidez;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            AlPulsar();
        }
    }
}