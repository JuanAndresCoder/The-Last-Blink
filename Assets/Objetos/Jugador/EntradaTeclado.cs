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
                StartCoroutine(Direcci�nJuego.main.ComenzarJuego());
            }
            else
            {
                if (Direcci�nJuego.instanciaci�nSe�ales.listaSe�ales.Count > 0)
                {
                    bool isvalid = Direcci�nJuego.instanciaci�nSe�ales.listaSe�ales[0].validezPulsaci�n.ComprobarValidez();
                }
                else
                {
                    Debug.Log("no hay mas");
                }
            }            
        }
    }
}
