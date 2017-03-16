using UnityEngine;

public class GhostZoneOperator : MonoBehaviour {

    public GameObject ghostObject;

    private GhostZone ghostZone;
    private RoomManager roomManager;

    private void Start()
    {
        resetPosition();
        roomManager = GetComponentInParent<RoomManager>();
        ghostZone = transform.parent.GetComponentInChildren<GhostZone>();
    }

    void OnSelect () {
        ghostZone.selected();
    }
	
	void Update () {
        if (transform.position != ghostObject.transform.position && roomManager.editionMode)
        {
            resetPosition();
        }
	}

    private void resetPosition()
    {
        transform.position = ghostObject.transform.position;
        transform.rotation = ghostObject.transform.rotation;
    }
}
