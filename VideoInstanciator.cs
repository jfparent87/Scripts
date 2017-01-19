using UnityEngine;
using System.Collections;

public class VideoInstanciator : MonoBehaviour {

    public bool isCreated = false;
    public GameObject video;
    public GameObject instantiatedObject;
    Vector3 position;
    Quaternion rotation;

    void Start()
    {
        position = this.transform.position;
        position.z = position.z + 0.2f;
        position.y = position.y + 0.35f;
        rotation = this.gameObject.transform.rotation;
        rotation.w -= 1.0f;
        rotation.y -= 0.7f;
        rotation.z += 0.7f;
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if (!isCreated)
        {
            instantiatedObject = (GameObject)Instantiate(video, position, rotation);
            instantiatedObject.transform.parent = gameObject.transform.parent;
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