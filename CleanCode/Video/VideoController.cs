using UnityEngine;
using System.Diagnostics;

[RequireComponent (typeof(AudioSource))]

public class VideoController : MonoBehaviour {
    public RoomManager roomManager;
    public MovieTexture movie;
    public AudioSource audioSource;
    public int activateAfterSeconds;

    private System.TimeSpan activationTime;
    private VideoAnchor videoAnchor;
    private int vsyncprevious;
    private Stopwatch timer;
    private Stopwatch totalTime;
    private bool timerStarted = false;
    private bool activated = false;

    void Start () {
        videoAnchor = GetComponentInParent<VideoAnchor>();
        vsyncprevious = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = 0;
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Custom;
        activationTime = new System.TimeSpan(0, 0, activateAfterSeconds);
        UnityEngine.Debug.Log(activationTime);
        timer = new Stopwatch();
    }

    void OnSelect()
    {
        if (!movie.isPlaying)
        {
            if (!roomManager.editionMode)
            {
                playVideo();
            }
        }
        else
        {
            if (!roomManager.editionMode)
            {
                pauseVideo();
            }
        }
    }

    void Update()
    {
        if (!movie.isPlaying)
        {
            QualitySettings.vSyncCount = vsyncprevious;
        }

        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }

        if (!activated && timer.Elapsed >= activationTime)
        {
            //TODO : event to activate 
            UnityEngine.Debug.Log( gameObject.name + " activated.");
            activated = true;
        }
    }

    public void resetVideo()
    {
        movie.Stop();
        timer = new Stopwatch();
        activated = false;
        timerStarted = false;
        audioSource.Stop();
        audioSource.Play();
        audioSource.Pause();
    }

    public void playVideo()
    {
        videoAnchor.freeAnchor();
        movie.Play();
        if (!timerStarted)
        {
            startTimer();
        }
        else
        {
            unPauseTimer();
        }
        
        audioSource.UnPause();
    }

    public void pauseVideo()
    {
        stopTimer();
        movie.Pause();
        try
        {
            audioSource.Pause();
        }
        catch
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Pause();
        }
    }

    public void startTimer()
    {
        timer = Stopwatch.StartNew();
        timerStarted = true;
    }

    public void unPauseTimer()
    {
        timer.Start();
        var elapsed = timer.Elapsed;
        UnityEngine.Debug.Log(gameObject.name + " unpause time elapsed = " + elapsed.ToString());
    }

    public void stopTimer()
    {
        if (movie.isPlaying)
        {
            timer.Stop();
            var elapsed = timer.Elapsed;
            UnityEngine.Debug.Log(gameObject.name + " time elapsed = " + elapsed.ToString());
        }
    }
}
