using UnityEngine;
using System.Collections;

public class VideoAnchorPosition : MonoBehaviour {

    public GameObject videoAnchor;
    public RoomManager roomManager;

    private void Start()
    {
        resetPosition();
    }

    void Update () {
        if (transform.position != videoAnchor.transform.position && !GetComponentInChildren<TapToPlaceClip>().placing)
        {
            StartCoroutine(resetPosition());
        }
    }

    public IEnumerator resetPosition()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = videoAnchor.transform.position;
    }
}
