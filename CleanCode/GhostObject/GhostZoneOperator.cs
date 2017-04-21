using UnityEngine;

public class GhostZoneOperator : MonoBehaviour {

    public GameObject ghostObject;
    public TapToPlaceGhost tapToPlaceGhost;

    private GhostZone ghostZone;
    private RoomManager roomManager;

    private void Start()
    {
        resetPosition();
        roomManager = GetComponentInParent<RoomManager>();
        ghostZone = transform.parent.GetComponentInChildren<GhostZone>();
    }

    void OnSelect () {
        if (tapToPlaceGhost.targetPositionAchieved && tapToPlaceGhost.waitingToGoBack)
        {
            tapToPlaceGhost.OnSelect();
        }
        else
        {
            ghostZone.selected();
        }
    }
	
	void Update () {
        if (transform.position != ghostObject.transform.position)
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
