using UnityEngine;

public class EditionLabel : MonoBehaviour {

    private GameObject objectToLabel;
    private RoomManager roomManager;

    private void Start()
    {
        objectToLabel = transform.parent.gameObject;
        roomManager = GetComponentInParent<RoomManager>();
    }

    void Update () {
        if (roomManager.editionMode)
        {
            transform.position.Set(objectToLabel.transform.position.x, objectToLabel.transform.position.y + 0.9f, objectToLabel.transform.position.z);
        }
	}
}
