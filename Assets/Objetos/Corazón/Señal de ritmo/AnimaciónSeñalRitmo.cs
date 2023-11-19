using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class AnimaciónSeñalRitmo : MonoBehaviour
{
    SeñalRitmoMaster señalRitmoMaster;
    RectTransform rectTransform;
    Image image;
    public float tiempoApariciónCírculo;
    void Start()
    {
        señalRitmoMaster = GetComponent<SeñalRitmoMaster>();
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        StartCoroutine(Contraerse());
    }
    IEnumerator Contraerse()
    {
        StartCoroutine(Aparecer());
        float freno = 1 - (señalRitmoMaster.margenError / 100);
        float velocidadAnimación = rectTransform.localScale.x  * freno / señalRitmoMaster.duraciónPulso;
        while (rectTransform.localScale.x > 0)
        {
            rectTransform.localScale -= Vector3.one * velocidadAnimación * Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator Aparecer()
    {
        float velocidadAparición = 1 / tiempoApariciónCírculo;
        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + (velocidadAparición * Time.deltaTime));
            yield return null;
        }
    }
}
