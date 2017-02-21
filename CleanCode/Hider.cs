using UnityEngine;

public class Hider : MonoBehaviour {

    public Vector3 previousSize;
    public bool showing;

    void Start()
    {
        previousSize = this.gameObject.transform.localScale;
    }

    public void hide() {
        this.gameObject.transform.localScale = new Vector3(0,0,0);
        showing = false;
    }

    public void show()
    {
        this.gameObject.transform.localScale = previousSize;
        showing = true;
    }
}