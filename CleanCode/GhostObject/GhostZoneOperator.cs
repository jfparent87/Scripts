using UnityEngine;

public class GhostZoneOperator : MonoBehaviour {

    public GhostZone ghostZone;
    public GameObject ghostObject;
    public RoomManager roomManager;

	void OnSelect () {
        ghostZone.selected();
    }
	
	void Update () {
        if (this.transform.position != ghostObject.transform.position && roomManager.editionMode)
        {
            this.transform.position = ghostObject.transform.position;
            this.transform.rotation = ghostObject.transform.rotation;
        }
	}
}
