using UnityEngine;
using System.Collections;

public class VideoAnchorPosition : MonoBehaviour {

    public GameObject videoAnchor;
    public RoomManager roomManager;

	void Update () {
        if (transform.position != videoAnchor.transform.position && !roomManager.editionMode)
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
