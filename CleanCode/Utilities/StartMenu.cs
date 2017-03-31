using UnityEngine;

public class StartMenu : MonoBehaviour {

    public RoomManager roomManager;
    public bool showMenu;
    public VideoHider videoOne;

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

    void Update () {
        if (roomManager.editionMode && !GetComponent<Hider>().showing)
        {
            GetComponent<Hider>().hide();
            transform.localPosition = hidePosition;
        }
        if (!roomManager.editionMode && GetComponent<Hider>().showing)
        {
            GetComponent<Hider>().show();
            transform.localPosition = showPosition;
        }
        if (!showMenu)
        {
            GetComponent<Hider>().hide();
            transform.localPosition = hidePosition;
        }
    }
}
