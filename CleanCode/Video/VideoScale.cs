using UnityEngine;

public class VideoScale : MonoBehaviour {

    public GameObject videoScreen;
    public float scaleFactor;

    private Vector3 screen;

    void OnSelect()
    {
        GetComponentInParent<VideoAnchor>().freeAnchor();
        screen = videoScreen.transform.localScale;
        videoScreen.transform.localScale = new Vector3(screen.x * scaleFactor, screen.y * scaleFactor, screen.z * scaleFactor);
        videoScreen.GetComponent<Hider>().previousSize = videoScreen.transform.localScale;
        GetComponentInParent<VideoAnchor>().lockAnchor();
    }
}
