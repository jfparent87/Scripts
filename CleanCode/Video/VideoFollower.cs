using UnityEngine;

public class VideoFollower : MonoBehaviour {

    public Hider hider;
    public bool follow;

    private Quaternion videoRotation;

    void Start()
    {
        follow = true;
    }

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
            rotateToFaceCamera();
        }
    }

    private void rotateToFaceCamera()
    {
        videoRotation = Camera.main.transform.localRotation;
        videoRotation.x = 0;
        videoRotation.z = 0;
        transform.parent.rotation = videoRotation;
    }
}
