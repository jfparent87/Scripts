using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{

    public RoomManager roomManager;
    public bool showMenu;
    public VideoHider videoOne;

    private bool fadeOutStarted = false;
    private Vector3 showPosition = new Vector3(0, 0, 0);
    private Vector3 hidePosition = new Vector3(0, 0, -1);

    void Start()
    {
        showMenu = true;
    }

    void OnSelect()
    {
        showMenu = false;
        StartCoroutine(videoOne.waitAndInstanciate());
    }

    void Update()
    {
        if (roomManager.editionMode && !GetComponent<Hider>().showing && !fadeOutStarted)
        {
            GetComponent<Hider>().hide();
            transform.localPosition = hidePosition;
        }
        if (!roomManager.editionMode && GetComponent<Hider>().showing)
        {
            GetComponent<Hider>().show();
            transform.localPosition = showPosition;
        }
        if (!showMenu && !fadeOutStarted)
        {
            fadeOutStarted = true;
            StartCoroutine(fadeOut(5));
        }
    }

    IEnumerator fadeOut(float aTime)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        float labelAlpha = GetComponentInChildren<TextMesh>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = GetComponent<Renderer>().material.color;
            Color labelNewColor = GetComponentInChildren<TextMesh>().color;
            newColor.a = Mathf.Lerp(alpha, 0, t);
            labelNewColor.a = (0);
            GetComponent<Renderer>().material.color = newColor;
            GetComponentInChildren<TextMesh>().color = labelNewColor;
            if (t >= 0.95)
            {
                GetComponent<Hider>().hide();
                transform.localPosition = hidePosition;
            }
            yield return null;
        }
    }
}