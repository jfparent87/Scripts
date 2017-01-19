using UnityEngine;
using System.Collections;

public class VideoCommands : MonoBehaviour
{

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        var videoController = this.gameObject.GetComponent<VideoController>();
        if (!videoController.movie.isPlaying)
        {
            videoController.playVideo();
        } else
        {
            videoController.stopVideo();
        }
    }
}
