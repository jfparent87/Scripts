using UnityEngine;
using System.Collections;

public class VideoFollower : MonoBehaviour {

    public VideoInstanciator videoInstanciator;
    public bool follow = false;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if (!follow)
        {
            follow = true;
        }
        else
        {
            follow = false;
        }
    }

    void Update()
    {
        if(videoInstanciator.instantiatedObject != null && follow)
        {
            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            videoInstanciator.instantiatedObject.transform.parent.rotation = toQuat;
        }
    }
}
