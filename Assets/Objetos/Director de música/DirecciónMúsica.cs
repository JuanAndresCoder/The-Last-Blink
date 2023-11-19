using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    float timer = 0f;
    public List<float> timestamps;

    List<float> internal_ts;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        ComenzarMúsica = _ComenzarMúsica(temasPersonajes[0]);

        internal_ts = new List<float>();
        internal_ts = timestamps;
    }

    public void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (internal_ts.Count == 0) return;

        if (timer >= internal_ts[0])
        {
            StartCoroutine(DirecciónJuego.main.instanciaciónSeñales.InstanciarSeñal());
            internal_ts.RemoveAt(0);
        }
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
        public AudioClip canción;
        public AudioClip intro;
        public int finalMinijuego;
    }
}
