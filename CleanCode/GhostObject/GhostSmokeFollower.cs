using UnityEngine;

public class GhostSmokeFollower : MonoBehaviour {

    private GhostZone ghostZone;

    void Start () {
        ghostZone = transform.parent.GetComponentInChildren<GhostZone>();
        this.transform.position = ghostZone.transform.position;
	}
	
	void Update () {
        if (this.transform.position != ghostZone.transform.position)
        {
            this.transform.position = ghostZone.transform.position;
        }
    }
}
