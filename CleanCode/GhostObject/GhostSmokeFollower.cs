using UnityEngine;

public class GhostSmokeFollower : MonoBehaviour {

    private GhostZone ghostZone;

    void Start () {
        ghostZone = transform.parent.GetComponentInChildren<GhostZone>();
        resetPosition();
    }
	
	void Update () {
        if (transform.position != ghostZone.transform.position)
        {
            resetPosition();
        }
    }

    private void resetPosition()
    {
        transform.position = new Vector3(ghostZone.transform.position.x, ghostZone.transform.position.y - 0.05f, ghostZone.transform.position.z -0.1f);
    }
}
