using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SeñalRitmoMaster : MonoBehaviour
{
    [HideInInspector] public AnimaciónSeñalRitmo animaciónSeñalRitmo;
    [HideInInspector] public ValidezPulsación validezPulsación;
    void Start()
    {
        animaciónSeñalRitmo = GetComponent<AnimaciónSeñalRitmo>();
        validezPulsación = GetComponent<ValidezPulsación>();
    }
    public SeñalRitmoMaster Instanciar(Transform puntoInstanciación)
    {
        GameObject instancia = Instantiate(gameObject, puntoInstanciación.position, Quaternion.identity, puntoInstanciación);
        return this;
    }
}
