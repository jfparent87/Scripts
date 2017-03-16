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
