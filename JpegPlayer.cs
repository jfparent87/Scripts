using UnityEngine;
using System.Collections;

public class JpegPlayer : MonoBehaviour {

    public GameObject plane;  // the two spheres
    public int numberOfFrames = 3612;
    public float frameRate = 30;

    private Texture2D[] frames;

    void Start()
    {
        // load the frames
        frames = new Texture2D[numberOfFrames];
        for (int i = 1; i <= numberOfFrames; ++i)
        {
            if (i < 10)
            {
                frames[i-1] = (Texture2D)Resources.Load("Images/Wendake/scene0000" + i.ToString() + ".jpeg");
            }
            if (i >= 10 && i < 100)
            {
                frames[i-1] = (Texture2D)Resources.Load("Images/Wendake/scene000" + i);
            }
            if (i >= 100 && i < 1000)
            {
                frames[i-1] = (Texture2D)Resources.Load("Images/Wendake/scene00" + i);
            }
            else
            {
                frames[i-1] = (Texture2D)Resources.Load("Images/Wendake/scene0" + i);
            }
        }
        GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        int currentFrame = (int)(Time.time * frameRate);
        if (currentFrame >= frames.Length)
            currentFrame = frames.Length;
        plane.GetComponent<Renderer>().material.mainTexture = frames[currentFrame];
    }
}
