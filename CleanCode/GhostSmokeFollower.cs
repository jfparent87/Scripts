using UnityEngine;

public class GhostSmokeFollower : MonoBehaviour {

    public GameObject ghostZone;
    public TextMesh hololensDebugLine4;
    public TextMesh hololensDebugLine5;

    void Start () {
        this.transform.position = ghostZone.transform.position;
	}
	
	void Update () {
        if (this.transform.position != ghostZone.transform.position)
        {
            this.transform.position = ghostZone.transform.position;
            hololensDebugLine4.text = "Ghost Smoke Position = " + this.transform.position.ToString();
            hololensDebugLine5.text = "Ghost Zone Position = " + this.ghostZone.transform.position.ToString();
        }
    }
}
