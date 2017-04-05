using UnityEngine;

public class ButtonHider : MonoBehaviour {

    private Hider hider;

    void Start () {
        hider = GetComponent<Hider>();
        if (hider.previousSize.x != 0.2f)
        {
            hider.previousSize = new Vector3(0.2f, 0.2f, 0.02f);
        }
	}
	
	void Update () {
        if (hider.previousSize.x != 0.2f)
        {
            hider.previousSize = new Vector3(0.2f, 0.2f, 0.02f);
        }
    }
}
