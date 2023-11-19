using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SeñalRitmoMaster : MonoBehaviour
{
    [HideInInspector] public AnimaciónSeñalRitmo animaciónSeñalRitmo;
    [HideInInspector] public ValidezPulsación validezPulsación;
    [Range(0, 100)] public int frenoAnimación;
    [Range(0, 50)] public int margenError;
    [HideInInspector] public float duraciónPulso;
    void Start()
    {
        animaciónSeñalRitmo = GetComponent<AnimaciónSeñalRitmo>();
        validezPulsación = GetComponent<ValidezPulsación>();
    }
    public SeñalRitmoMaster Instanciar(Transform puntoInstanciación, float duraciónPulso)
    {
        this.duraciónPulso = duraciónPulso;
        GameObject instancia = Instantiate(gameObject, puntoInstanciación.position, Quaternion.identity, puntoInstanciación);
        return instancia.GetComponent<SeñalRitmoMaster>();
    }
}
