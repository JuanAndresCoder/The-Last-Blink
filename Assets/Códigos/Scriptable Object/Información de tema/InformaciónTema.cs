using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Información de tema", order = 0, menuName = "Scriptable Object/Información de tema")]
public class InformaciónTema : ScriptableObject
{
    public InformaciónMarca[] informaciónMarcas;
    [Serializable]
    public class InformaciónMarca
    {
        public float duraciónPulso;
        public float tiempoMarca;
    }
}