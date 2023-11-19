using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntradaTeclado : MonoBehaviour
{
    public static EntradaTeclado main;
    public bool isInputOn = false;

    AudioSource heartbeatSFX;

    private void Awake()
    {
        main = this;

        heartbeatSFX = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            heartbeatSFX.Play();

            if (isInputOn == false)
            {
                isInputOn = true;
                StartCoroutine(DirecciónJuego.main.ComenzarJuego());
            }
            else
            {
                if (DirecciónJuego.instanciaciónSeñales.listaSeñales.Count > 0)
                {
                    bool isvalid = DirecciónJuego.instanciaciónSeñales.listaSeñales[0].validezPulsación.ComprobarValidez();
                }
                else
                {
                    Debug.Log("no hay mas");
                }
            }            
        }
    }
}
