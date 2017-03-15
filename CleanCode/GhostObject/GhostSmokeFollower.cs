using UnityEngine;

public class GhostSmokeFollower : MonoBehaviour {

    public GameObject ghostZone;

    void Start () {
        this.transform.position = ghostZone.transform.position;
	}
	
	void Update () {
        if (this.transform.position != ghostZone.transform.position)
        {
            this.transform.position = ghostZone.transform.position;
        }
    }
}
