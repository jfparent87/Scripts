using UnityEngine;
using System.Collections;

public class VideoInstanciator : MonoBehaviour {

    public bool isCreated = false;
    public GameObject video;
    public GameObject instantiatedObject;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if(!isCreated)
        {
            instantiatedObject = (GameObject)Instantiate(video, new Vector3(0, 0, 3), Quaternion.Euler(-270,180,0));
            instantiatedObject.transform.parent = GameObject.Find("Room").transform;
            isCreated = true;
        }
        else
        {
            instantiatedObject.GetComponent<VideoController>().movie.Stop();
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }
}
