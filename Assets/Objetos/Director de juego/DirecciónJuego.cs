using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónJuego : MonoBehaviour
{
    public static DirecciónJuego main;
    public static InstanciaciónSeñales instanciaciónSeñales;
    public static DirecciónMúsica direcciónMúsica;

    [SerializeField] GameObject menuUI;
    [SerializeField] Animator eyes;

    float timer;
    bool inGame = false;


    void Awake()
    {
        main = this;
    }


    private void FixedUpdate()
    {
        if (!inGame) return;

        timer += Time.deltaTime;


        // Aquí se controlan las animaciones, cuando aparecen los personajes y demás
        switch (Mathf.FloorToInt(timer))
        {
            case 5:
                eyes.gameObject.SetActive(true);
                eyes.Play("CloseEyes");            
             break;
            case 15:
                // cierra ojos y aparece nieto
                break;

        }

        if (timer > 5f)
        {
            
        }

    }


    public IEnumerator ComenzarJuego()
    {
        menuUI.GetComponent<Animator>().Play("CloseMenu");


        direcciónMúsica = FindObjectOfType<DirecciónMúsica>(true);
        direcciónMúsica.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.DirigirMúsica != null);
        instanciaciónSeñales = FindObjectOfType<InstanciaciónSeñales>(true);
        instanciaciónSeñales.enabled = true;
        yield return new WaitUntil(() => direcciónMúsica.AlAlcanzarMarca != null);
        direcciónMúsica.ComenzarMúsica();

        Camera.main.gameObject.GetComponent<Animator>().Play("Game");

        inGame = true;
    }
}
