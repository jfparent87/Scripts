using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class VideoController : MonoBehaviour {
    public RoomManager roomManager;
    public MovieTexture movie;

    private AudioSource audioSource;
    private VideoAnchor videoAnchor;
    int vsyncprevious;

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
    }

    void OnSelect()
    {
        if (!movie.isPlaying)
        {
            playVideo();
        }
        else
        {
            pauseVideo();
        }
    }

    void Update()
    {
        if (!movie.isPlaying)
        {
            QualitySettings.vSyncCount = vsyncprevious;
        }
    }

    public void restartVideo()
    {
        movie.Stop();
        audioSource.Stop();
    }

    public void playVideo()
    {
        if (!roomManager.editionMode)
        {
            videoAnchor.freeAnchor();
            movie.Play();
            audioSource.UnPause();
        }
    }

    public void pauseVideo()
    {
        if (!roomManager.editionMode)
        {
            movie.Pause();
            audioSource.Pause();
            videoAnchor.lockAnchor();
        }
    }
}
