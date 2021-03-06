﻿using UnityEngine;

public class EditionLabel : MonoBehaviour {
    
    private Quaternion videoRotation;

    void Update () {
        if (GetComponentInParent<Hider>().showing)
        {
            rotateToFaceCamera();
        }
    }

    private void rotateToFaceCamera()
    {
        videoRotation = Camera.main.transform.localRotation;
        videoRotation.x = 0;
        videoRotation.z = 0;
        this.transform.rotation = videoRotation;
    }
}
