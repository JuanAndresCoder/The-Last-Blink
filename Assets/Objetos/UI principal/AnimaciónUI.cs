using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class AnimaciónUI : MonoBehaviour
{
    Animator animator;
    static float tiempoAnimación;
    void Start() 
    { 
        animator = GetComponent<Animator>();
        AnimationClip[] animaciones = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip animación in animaciones)
        {
            if (animación.name == "Cerrar ojos") { tiempoAnimación = animación.length; break; }
        }
    }
    void OnEnable()
    {
        DirecciónJuego.AlComenzarJuego += FadeIn;
        DirecciónJuego.AlFallarPulsación += FadeOut;
    }
    void FadeOut()
    {
        animator.Play("Cerrar ojos");
    }
    void FadeIn()
    {
        animator.Play("Abrir ojos");
    }
    public static float ObtenerTiempoAnimación()
    {
        return tiempoAnimación;
    }
}
