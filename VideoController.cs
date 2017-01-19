using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class VideoController : MonoBehaviour {
    public MovieTexture movie;
    int vsyncprevious;

    // Use this for initialization
    void Start () {
        vsyncprevious = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = 0;
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        GetComponent<AudioSource>().Play();
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
