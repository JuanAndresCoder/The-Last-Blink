using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Direcci�nM�sica : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<TemaPersonaje> temasPersonajes;
    [HideInInspector] public float pulsosPorSegundo;
    public Action AlComenzarM�sica;
    [HideInInspector] public int totalPulsos;
    // Corrutinas
    public IEnumerator ComenzarM�sica;

    float timer = 0f;
    public List<float> timestamps;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ComenzarM�sica = _ComenzarM�sica(temasPersonajes[0]);
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timestamps.Count == 0) return;

        if (timer >= timestamps[0])
        {
            StartCoroutine(Direcci�nJuego.main.instanciaci�nSe�ales.InstanciarSe�al());
            timestamps.RemoveAt(0);
        }
    }


    IEnumerator _ComenzarM�sica(TemaPersonaje temaPersonaje)
    {
        if (temaPersonaje.intro != null)
        {
            audioSource.clip = temaPersonaje.intro;
            audioSource.Play();
            yield return new WaitUntil(() => audioSource.isPlaying == false);
        }
        audioSource.clip = temaPersonaje.canci�n;
        pulsosPorSegundo = ObtenerPulsosPorSegundo();

        audioSource.Play();

        while (timestamps.Count > 0)
        {
            totalPulsos = Mathf.RoundToInt(
                audioSource.timeSamples / audioSource.clip.frequency * pulsosPorSegundo
                );
            yield return null;
        }
    }
    public float ObtenerPulsosPorSegundo()
    {
        return 60f / UniBpmAnalyzer.AnalyzeBpm(audioSource.clip);
    }
    [Serializable]
    public class TemaPersonaje
    {
        public AudioClip canci�n;
        public AudioClip intro;
        public int finalMinijuego;
    }
}
