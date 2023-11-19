using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class InstanciaciónSeñales : MonoBehaviour
{
    [SerializeField] GameObject señal;
    [SerializeField] List<SeñalRitmoMaster> listaSeñales;
    Transform puntoInstanciación;
    SeñalRitmoMaster señalRitmoMaster;
    [Range(0, 50)] public int margenError;
    // Corrutinas
    public IEnumerator InstanciarSeñales;
    void Start()
    {
        listaSeñales = new List<SeñalRitmoMaster>();
        señalRitmoMaster = señal.GetComponent<SeñalRitmoMaster>();
        puntoInstanciación = transform.GetChild(0);
        InstanciarSeñales = _InstanciarSeñales();
        // NOTA: La propia señal subscribe la función de autodestrucción
    }
    void EmpezarInstanciación()
    {
        StartCoroutine(InstanciarSeñales);
    }
    void PararInstanciación()
    {
        foreach (SeñalRitmoMaster señalRitmoMaster in listaSeñales)
        {

        }
    }
    IEnumerator _InstanciarSeñales()
    {
        int pulsoActual = DirecciónJuego.direcciónMúsica.totalPulsos;
        while (pulsoActual == DirecciónJuego.direcciónMúsica.totalPulsos) 
        {
            yield return null;
        }
        SeñalRitmoMaster instanciaSeñal = señalRitmoMaster.Instanciar(puntoInstanciación);
        listaSeñales.Add(instanciaSeñal);
        StartCoroutine(_InstanciarSeñales());
    }
    void OnEnable()
    {
        DirecciónJuego.direcciónMúsica.AlComenzarMúsica += EmpezarInstanciación;
    }
    void OnDisable()
    {
        DirecciónJuego.direcciónMúsica.AlComenzarMúsica -= EmpezarInstanciación;
    }
}
