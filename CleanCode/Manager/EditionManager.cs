using UnityEngine;
using System.Collections.Generic;

public class EditionManager : MonoBehaviour
{
    public List<VideoPlayerController> videoControllers;
    public List<VideoHider> videoHiders;
    public List<Hider> hiders;
    public List<MeshRenderer> campfireMeshRenderers;
    public RoomManager roomManager;

    private VideoPlayerController currentVideo = null;

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
            videoHider.instanciate();
            if (videoHider.gameObject.name != "Clip1Instanciator")
            {
                videoHider.proximityStop = 10.0f;
            }
            else
            {
                videoHider.proximityStop = 2.75f;
            }
        }

        foreach (var campfireMeshRenderer in campfireMeshRenderers)
        {
            campfireMeshRenderer.enabled = true;
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

        foreach (var campfireMeshRenderer in campfireMeshRenderers)
        {
            campfireMeshRenderer.enabled = false;
        }

        currentVideo = null;
    }
}
