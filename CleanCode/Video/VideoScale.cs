using UnityEngine;
using System.Text.RegularExpressions;

public class VideoScale : MonoBehaviour {

    public GameObject videoScreen;
    public float scaleFactor;

    public GameObject database;
    private Vector3 screen;

    private void Start()
    {
        database = GameObject.Find("DataBase");
    }
    void OnSelect()
    {
        screen = videoScreen.transform.localScale;
        videoScreen.transform.localScale = new Vector3(screen.x * scaleFactor, screen.y * scaleFactor, screen.z * scaleFactor);
        videoScreen.GetComponent<Hider>().previousSize = videoScreen.transform.localScale;
        videoScreen.GetComponent<Hider>().resetPreviousSize();
        database.GetComponent<Database>().setVideoSize(videoScreen.transform.localScale, Regex.Match(gameObject.name, @"\d+").Value);
    }
}
