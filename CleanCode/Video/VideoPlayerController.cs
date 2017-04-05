﻿using UnityEngine;
using System.Diagnostics;
using UnityEngine.Video;

[RequireComponent(typeof(AudioSource))]

public class VideoPlayerController : MonoBehaviour
{

    public RoomManager roomManager;
    public VideoPlayer movie;
    //public AudioSource audioSource;
    public int activateAfterSeconds;
    public bool activated = false;
    public TapToPlaceCookingPot cookingPot;

    private System.TimeSpan activationTime;
    private int vsyncprevious;
    private Stopwatch timer;
    private Stopwatch totalTime;
    private bool timerStarted = false;

    void Start()
    {
        vsyncprevious = QualitySettings.vSyncCount;
        QualitySettings.vSyncCount = 0;
        movie = GetComponent<VideoPlayer>();
        /*audioSource = GetComponent<AudioSource>();
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.dopplerLevel = 0.0f;
        audioSource.rolloffMode = AudioRolloffMode.Custom;*/
        activationTime = new System.TimeSpan(0, 0, activateAfterSeconds);
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

        /*if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
        }*/

        if (!activated && timer.Elapsed >= activationTime && !roomManager.editionMode)
        {
            //TODO : event to activate 
            pauseVideo();
            activated = true;
            if (gameObject.name == "wendake1")
            {
                cookingPot.locked = false;
            }
            gameObject.GetComponentInParent<Hider>().hide();
        }

        if (roomManager.editionMode && movie.isPlaying)
        {
            pauseVideo();
        }
    }

    public void resetVideo()
    {
        movie.Stop();
        timer = new Stopwatch();
        activated = false;
        timerStarted = false;
        /*audioSource.Stop();
        audioSource.Play();
        audioSource.Pause();*/
    }

    public void playVideo()
    {
        movie.Play();
        if (!timerStarted)
        {
            startTimer();
        }
        else
        {
            unPauseTimer();
        }

        /*audioSource.UnPause();*/
    }

    public void pauseVideo()
    {
        stopTimer();
        movie.Pause();
        /*try
        {
            audioSource.Pause();
        }
        catch
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.Pause();
        }*/
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
    }

    public void stopTimer()
    {
        if (movie.isPlaying)
        {
            timer.Stop();
            var elapsed = timer.Elapsed;
        }
    }
}
