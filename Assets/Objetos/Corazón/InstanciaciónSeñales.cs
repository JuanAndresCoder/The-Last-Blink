using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class InstanciaciónSeñales : MonoBehaviour
{
    [SerializeField] GameObject señal;
    public List<SeñalRitmoMaster> listaSeñales;
    Transform puntoInstanciación;
    SeñalRitmoMaster señalRitmoMaster;
    void Start()
    {
        señalRitmoMaster = señal.GetComponent<SeñalRitmoMaster>();
        puntoInstanciación = transform.GetChild(0);
    }
    void InstanciarSeñal()
    {
        float duraciónPulso = DirecciónJuego.direcciónMúsica.ObtenerDuraciónPulso();
        SeñalRitmoMaster instanciaSeñal = señalRitmoMaster.Instanciar(puntoInstanciación, duraciónPulso);
        listaSeñales.Add(instanciaSeñal);
    }
    void DeshabilitarSeñales()
    {
        foreach (SeñalRitmoMaster señalRitmoMaster in listaSeñales)
        {
            señalRitmoMaster.validezPulsación.enabled = false;
        }
    }
    public void ComprobarValidez()
    {
        SeñalRitmoMaster señalActual = listaSeñales.First();
        bool esVálida = señalActual.validezPulsación.ObtenerValidez();
        if (esVálida == true) { DarAcierto(señalActual); }
        else { DarFallo(señalActual); }
    }
    void DarAcierto(SeñalRitmoMaster señalActual)
    {
        Debug.Log("Acierto");
        listaSeñales.Remove(señalActual);
        Destroy(señalActual.gameObject);
    }
    void DarFallo(SeñalRitmoMaster señalActual)
    {
        Debug.Log("Fallo");
        listaSeñales.Remove(señalActual);
        Destroy(señalActual.gameObject);
        DirecciónJuego.AlFallarPulsación();
    }
    void OnEnable()
    {
        DirecciónJuego.direcciónMúsica.AlAlcanzarMarca += InstanciarSeñal;
        DirecciónJuego.AlFallarPulsación += DeshabilitarSeñales;
    }
    void OnDisable()
    {
        DirecciónJuego.direcciónMúsica.AlAlcanzarMarca -= InstanciarSeñal;
        DirecciónJuego.AlFallarPulsación += DeshabilitarSeñales;
    }
}