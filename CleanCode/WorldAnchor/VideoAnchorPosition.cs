using UnityEngine;
using System.Collections;

public class VideoAnchorPosition : MonoBehaviour {

    public GameObject videoAnchor;

	void Start () {
        StartCoroutine(resetPosition());
    }

    public IEnumerator resetPosition()
    {
        yield return new WaitForSeconds(1.0f);
        transform.position = videoAnchor.transform.position;
    }
}
