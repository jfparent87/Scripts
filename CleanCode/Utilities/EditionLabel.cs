using UnityEngine;

public class EditionLabel : MonoBehaviour {

    public GameObject objectToLabel;
    public RoomManager roomManager;

	void Update () {
        if (roomManager.editionMode)
        {
            this.transform.position.Set(objectToLabel.transform.position.x, objectToLabel.transform.position.y + 0.9f, objectToLabel.transform.position.z);
        }
	}
}
