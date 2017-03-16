using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ResetSphere : MonoBehaviour {

    public List<TapToPlaceGhost> ghostZones;
    public List<VideoController> videoControllers;

    void OnSelect()
    {
        SceneManager.LoadScene("Scene002");
        foreach (var ghostZone in ghostZones)
        {
            ghostZone.resetTargetPosition();
        }
        foreach (var videoController in videoControllers)
        {
            videoController.restartVideo();
        }
    }
}
