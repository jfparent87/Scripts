using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class VideoController : MonoBehaviour {
    public MovieTexture movie;
    public AudioSource audio;
    int vsyncprevious;

    // Use this for initialization
    void Start () {
        vsyncprevious = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = 0;
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        audio = GetComponent<AudioSource>();
        audio.spatialize = true;
        audio.spatialBlend = 1.0f;
        audio.dopplerLevel = 0.0f;
        audio.rolloffMode = AudioRolloffMode.Custom;
        audio.Play();
        movie.Play();
	}

    void Update()
    {
        if (!movie.isPlaying)
        {
            QualitySettings.vSyncCount = vsyncprevious;
        }
    }

    public void playVideo()
    {
        movie.Play();
        GetComponent<AudioSource>().UnPause();
    }

    public void stopVideo()
    {
        movie.Pause();
        GetComponent<AudioSource>().Pause();
    }
}
