using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScore : MonoBehaviour
{
    public AudioClip FxPuntuacion;
    public float VolumeFxPuntuacion = 1f;
    private AudioSource source;
    public float DelaySound;
    private bool DoOnce = true;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("SonidoPuntuacion", DelaySound);
    }
    void SonidoPuntuacion()
    {
        if (DoOnce)
            source.PlayOneShot(FxPuntuacion, VolumeFxPuntuacion);
        DoOnce = false;

    }
}
