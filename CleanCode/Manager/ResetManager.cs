using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetManager : MonoBehaviour {

    public List<TapToPlaceGhost> ghostZones;
    public List<VideoPlayerController> videoControllers;
    public List<TapToPlaceIngredient> ingredients;
    public List<VideoAnchorPosition> videoAnchorPositions;
    public GameObject database;

    void Start()
    {
        resetSetup();
    }

    public void OnSelect()
    {
        SceneManager.LoadScene("Scene002");
    }

    IEnumerator resetGhostPositions(TapToPlaceGhost ghostZone)
    {
        yield return new WaitForSeconds(0.2f);
        ghostZone.resetTargetPosition();
    }

    IEnumerator resetGhostPositions(VideoPlayerController videoController)
    {
        yield return new WaitForSeconds(0.2f);
        videoController.resetVideo();
    }

    private void resetSetup()
    {
        database.GetComponent<Database>().resetAnchorConnection();
        database.GetComponent<Database>().findDatas();
        foreach (var ghostZone in ghostZones)
        {
            StartCoroutine(resetGhostPositions(ghostZone));
        }
        foreach (var videoAnchorPosition in videoAnchorPositions)
        {
            StartCoroutine(videoAnchorPosition.resetPosition());
        }
        foreach (var videoController in videoControllers)
        {
            StartCoroutine(resetGhostPositions(videoController));
        }
    }

}
