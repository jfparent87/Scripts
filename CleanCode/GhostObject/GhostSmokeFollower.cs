using UnityEngine;

public class GhostSmokeFollower : MonoBehaviour {

    private GhostZone ghostZone;

    void Start () {
        ghostZone = transform.parent.GetComponentInChildren<GhostZone>();
        transform.position = ghostZone.transform.position;
	}
	
	void Update () {
        if (transform.position != ghostZone.transform.position)
        {
            transform.position = ghostZone.transform.position;
        }
    }
}
