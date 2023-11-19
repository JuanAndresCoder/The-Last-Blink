using System;
using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ComenzarM�sica = _ComenzarM�sica(temasPersonajes[0]);
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
        yield return new WaitUntil(() => pulsosPorSegundo > 0);
        audioSource.Play();
        AlComenzarM�sica();
        while (true)
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
