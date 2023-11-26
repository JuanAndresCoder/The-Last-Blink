using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using VikingCrew.Tools.UI;
public class DiálogoPersonaje : MonoBehaviour
{
    [SerializeField] ContenedorDiálogo contenedorDiálogo;
    [SerializeField] SpeechBubbleManager.SpeechbubbleType tipoBurbuja;
    int númeroLínea;
    Transform puntoInstanciación;
    void Start()
    {
        puntoInstanciación = transform.GetChild(0);
    }
    public void Hablar()
    {
        ContenedorDiálogo.InformaciónLínea informaciónLínea = contenedorDiálogo.listaLíneas[númeroLínea];
        SpeechBubbleManager.Instance.AddSpeechBubble(puntoInstanciación, informaciónLínea.línea, tipoBurbuja, informaciónLínea.tiempoEnPantalla);
        númeroLínea++;
    }
    void OnDisable()
    {
        númeroLínea = 0;
    }
}
