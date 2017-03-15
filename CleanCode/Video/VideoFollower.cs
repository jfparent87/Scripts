using UnityEngine;

public class VideoFollower : MonoBehaviour {

    public Hider hider;
    public bool follow = true;

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
        if(hider.showing && follow)
        {
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;
            this.transform.parent.rotation = toQuat;
        }
    }
}
