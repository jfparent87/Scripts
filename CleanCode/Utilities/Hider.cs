﻿using UnityEngine;

public class Hider : MonoBehaviour {

    public Vector3 previousSize;
    public bool showing;

    void Start()
    {
        resetPreviousSize();
    }

    public void hide() {
        gameObject.transform.localScale = new Vector3(0,0,0);
        showing = false;
    }

    public void show()
    {
        gameObject.transform.localScale = previousSize;
        showing = true;
    }

    public void resetPreviousSize()
    {
        previousSize = gameObject.transform.localScale;
    }
}