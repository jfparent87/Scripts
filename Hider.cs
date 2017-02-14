using UnityEngine;
using System.Collections;

public class Hider : MonoBehaviour {

    Vector3 previousSize; 

    void Start()
    {
        previousSize = this.gameObject.transform.localScale;
    }

    public void hide() {
        this.gameObject.transform.localScale = new Vector3(0,0,0);
    }

    public void show()
    {
        this.gameObject.transform.localScale = previousSize;
    }
}