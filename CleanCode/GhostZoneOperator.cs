using UnityEngine;

public class GhostZoneOperator : MonoBehaviour {

    public GhostZone ghostZone;

	void OnSelect () {
        ghostZone.selected();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
