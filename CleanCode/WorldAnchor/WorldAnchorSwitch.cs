using UnityEngine;
using System.Collections.Generic;

public class WorldAnchorSwitch : MonoBehaviour
{
    public List<VideoController> videoControllers;
    public List<VideoHider> videoHiders;
    public List<Hider> hiders;
    public RoomManager roomManager;

    void OnSelect()
    {
        roomManager.editionMode = !roomManager.editionMode;
        if (roomManager.editionMode)
        {
            foreach (var videoHider in videoHiders)
            {
                videoHider.proximityStop = 10.0f;
            }
            foreach (var hider in hiders)
            {
                hider.show();
            }
        }
        else
        {
            foreach (var hider in hiders)
            {
                hider.hide();
            }
        }

        foreach (var videoController in videoControllers)
        {
            videoController.restartVideo();
        }
    }
}
