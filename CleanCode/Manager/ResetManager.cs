using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public class ResetManager : MonoBehaviour {

    public List<TapToPlaceGhost> ghostZones;
    public List<VideoController> videoControllers;
    public List<TapToPlaceIngredient> ingredients;
    public List<VideoAnchorPosition> videoAnchorPositions;
    public GameObject database;

    public void OnSelect()
    {
        Debug.Log("Reset Scene");
        SceneManager.LoadScene("Scene002");
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
    }

    IEnumerator resetGhostPositions(TapToPlaceGhost ghostZone)
    {
        yield return new WaitForSeconds(0.5f);
        ghostZone.resetTargetPosition();
    }

}
