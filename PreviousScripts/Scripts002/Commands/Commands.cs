using UnityEngine;
using System.Collections;

abstract public class Commands : MonoBehaviour {

    public VideoInstanciator2 videoInstanciator;
    public bool follow = true;

    virtual public void OnSelect()
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

    virtual public void Update()
    {
        if (videoInstanciator.instantiatedObject != null && follow)
        {
            // Rotate this object's parent object to face the user.
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            videoInstanciator.instantiatedObject.transform.parent.rotation = toQuat;
        }
    }
}
