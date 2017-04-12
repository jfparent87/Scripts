using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{

    public RoomManager roomManager;
    public bool showMenu;
    public VideoHider videoOne;
    public float speed = 0.5f;
    public float distanceToCameraWhenPlacing = 1.0f;

    private bool fadeOutStarted = false;
    private Vector3 showPosition = new Vector3(0, 0, 0);
    private Vector3 hidePosition = new Vector3(0, 0, -2);
    private Vector3 targetPosition;
    private float heightCorrection = 1.5f;
    private float step;
    private Quaternion menuRotation;
    private bool setup = false;

    void Start()
    {
        showMenu = true;
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing - 0.3985f));
        transform.position = targetPosition;
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
        if (!fadeOutStarted && !roomManager.editionMode && setup)
        {
            placeMenuInFrontOfCamera();
        }
        if (!fadeOutStarted && !roomManager.editionMode && !setup)
        {
            targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2) + heightCorrection, Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
            transform.position = targetPosition;
            setup = true;
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

    private void placeMenuInFrontOfCamera()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, (Screen.height / 2), Camera.main.nearClipPlane + distanceToCameraWhenPlacing));
        if (transform.position.x > targetPosition.x + 0.1f || transform.position.y > targetPosition.y + 0.035f || transform.position.z > targetPosition.z + 0.1f ||
            transform.position.x < targetPosition.x - 0.1f || transform.position.y < targetPosition.y - 0.035f || transform.position.z < targetPosition.z - 0.1f)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            menuRotation = Camera.main.transform.localRotation;
            menuRotation.x = 0;
            menuRotation.z = 0;
            menuRotation *= Quaternion.Euler(-90, 0, 0);
            transform.rotation = menuRotation;
        }
    }

}