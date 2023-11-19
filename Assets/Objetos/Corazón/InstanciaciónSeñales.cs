using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InstanciaciónSeñales : MonoBehaviour
{
    [SerializeField] GameObject señal;
    public List<SeñalRitmoMaster> listaSeñales;
    Transform puntoInstanciación;
    SeñalRitmoMaster señalRitmoMaster;
    void Start()
    {
        listaSeñales = new List<SeñalRitmoMaster>();
        señalRitmoMaster = señal.GetComponent<SeñalRitmoMaster>();
        puntoInstanciación = transform.GetChild(0);
        // NOTA: La propia señal subscribe la función de autodestrucción
    }
    public void PararInstanciación()
    {
        foreach (SeñalRitmoMaster señalRitmoMaster in listaSeñales)
        {

        }
    }
    void InstanciarSeñal()
    {
        float duraciónPulso = DirecciónJuego.direcciónMúsica.duraciónPulsos[0];
        SeñalRitmoMaster instanciaSeñal = señalRitmoMaster.Instanciar(puntoInstanciación, duraciónPulso);
        listaSeñales.Add(instanciaSeñal);
    }
    //IEnumerator _InstanciarSeñales()
    //{
    //    int pulsoActual = DirecciónJuego.direcciónMúsica.totalPulsos;
    //    while (pulsoActual == DirecciónJuego.direcciónMúsica.totalPulsos) 
    //    {
    //        yield return null;
    //    }
    //    SeñalRitmoMaster instanciaSeñal = señalRitmoMaster.Instanciar(puntoInstanciación);
    //    listaSeñales.Add(instanciaSeñal);
    //    yield return null;
    //    //StartCoroutine(_InstanciarSeñales());
    //}
    void OnEnable()
    {
        DirecciónJuego.direcciónMúsica.AlAlcanzarMarca += InstanciarSeñal;
    }
    void OnDisable()
    {
        DirecciónJuego.direcciónMúsica.AlAlcanzarMarca -= InstanciarSeñal;
    }
}
