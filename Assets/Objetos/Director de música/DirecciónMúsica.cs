using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DirecciónMúsica : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<TemaPersonaje> temasPersonajes;
    [HideInInspector] public float pulsosPorSegundo;
    public Action AlComenzarMúsica;
    [HideInInspector] public int totalPulsos;
    // Corrutinas
    public IEnumerator ComenzarMúsica;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ComenzarMúsica = _ComenzarMúsica(temasPersonajes[0]);
    }
    IEnumerator _ComenzarMúsica(TemaPersonaje temaPersonaje)
    {
        if (temaPersonaje.intro != null)
        {
            audioSource.clip = temaPersonaje.intro;
            audioSource.Play();
            yield return new WaitUntil(() => audioSource.isPlaying == false);
        }
        audioSource.clip = temaPersonaje.canción;
        pulsosPorSegundo = ObtenerPulsosPorSegundo();
        yield return new WaitUntil(() => pulsosPorSegundo > 0);
        audioSource.Play();
        AlComenzarMúsica();
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
        public AudioClip canción;
        public AudioClip intro;
        public int finalMinijuego;
    }
}
