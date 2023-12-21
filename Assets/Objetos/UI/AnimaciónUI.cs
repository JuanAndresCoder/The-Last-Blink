using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimaciónUI : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    void Start() 
    { 
        animator = GetComponent<Animator>();
    }
    void CerrarOjos()
    {
        animator.SetTrigger("Cerrar ojos");
    }
    public void AbrirOjos()
    {
        animator.SetTrigger("Abrir ojos");
    }
    public float ObtenerTiempoAnimación()
    {
        AnimatorStateInfo infoEstado = animator.GetCurrentAnimatorStateInfo(0);
        float porcentajeAnimación = infoEstado.normalizedTime - (Mathf.RoundToInt(infoEstado.normalizedTime));
        float tiempoAnimación = infoEstado.length - (infoEstado.length * porcentajeAnimación);
        return tiempoAnimación;
    }
    void OnEnable()
    {
        DirecciónJuego.AlFallarPulsación += CerrarOjos;
    }
    private void OnDisable()
    {
        DirecciónJuego.AlFallarPulsación -= CerrarOjos;
    }
}
