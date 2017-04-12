using UnityEngine;
using System.Diagnostics;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{

    public RoomManager roomManager;
    public VideoPlayer movie;
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

        if (!activated && timer.Elapsed >= activationTime && !roomManager.editionMode)
        {
            pauseVideo();
            activated = true;

            // Insert event to activate for a certain object.
            if (gameObject.name == "Clip1")
            {
                cookingPot.locked = false;
            }
            if (gameObject.name == "Clip2")
            {
                cookingPot.ingredientsLocked = false;
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
    }

    public void pauseVideo()
    {
        stopTimer();
        movie.Pause();
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
