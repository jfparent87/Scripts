using UnityEngine;

public class CursorHider : MonoBehaviour {

	void Update () {
        if (GetComponent<Hider>().previousSize.x != 1)
        {
            GetComponent<Hider>().previousSize = new Vector3(1,1,1);
        }
	}
}
