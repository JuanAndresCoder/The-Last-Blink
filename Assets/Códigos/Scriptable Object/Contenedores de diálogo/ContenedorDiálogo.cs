using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ContenedorDiálogo", order = 0, menuName = "Scriptable Object/Contenedor de diálogo")]
public class ContenedorDiálogo : ScriptableObject
{
    public InformaciónLínea[] listaLíneas;
    [Serializable]
    public class InformaciónLínea
    {
        public string línea;
        public float tiempoEnPantalla;
    }
}
