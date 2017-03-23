using UnityEngine;
using System.Collections.Generic;

public class EditionManager : MonoBehaviour
{
    public List<VideoController> videoControllers;
    public List<VideoHider> videoHiders;
    public List<Hider> hiders;
    public RoomManager roomManager;

    private VideoController currentVideo = null;

    public void OnSelect()
    {
        roomManager.editionMode = !roomManager.editionMode;
        if (roomManager.editionMode)
        {
            enterEditionMode();
        }
        else
        {
            enterPlayMode();
        }
    }

    public void enterEditionMode()
    {
        foreach (var videoHider in videoHiders)
        {
            if (videoHider.gameObject.name != "Wendake1")
            {
                videoHider.proximityStop = 10.0f;
            }
            else
            {
                videoHider.proximityStop = 2.75f;
            }
        }

        foreach (var hider in hiders)
        {
            hider.show();
        }

        foreach (var videoController in videoControllers)
        {
            if (videoController.movie.isPlaying)
            {
                videoController.pauseVideo();
                currentVideo = videoController;
            }
        }
    }

    public void enterPlayMode()
    {
        foreach (var videoController in videoControllers)
        {
            if (videoController == currentVideo)
            {
                videoController.playVideo();
            }
            else
            {
                videoController.resetVideo();
            }
        }

        foreach (var hider in hiders)
        {
            hider.hide();

            if (currentVideo != null && currentVideo.name == hider.gameObject.name.ToLower())
            {
                hider.show();
            }
        }

        currentVideo = null;
    }
}
