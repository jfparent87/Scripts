using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;


public class Database : MonoBehaviour
{

    public static Database Instance;

    private Vector3 firstVideoSize;
    private Vector3 secondVideoSize;
    private Vector3 thirdVideoSize;
    private Vector3 defaultSize = new Vector3(0.08f, 0.02f, 0.06f);
    public GameObject firstVideoData;
    public GameObject secondVideoData;
    public GameObject thirdVideoData;
    private GameObject video1;
    private GameObject video2;
    private GameObject video3;
    private Hider firstVideoHider;
    private Hider secondVideoHider;
    private Hider thirdVideoHider;
    private GameObject[] datas;

    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;


    void Start()
    {
        anchorManager = WorldAnchorManager.Instance;
        if (anchorManager == null)
        {
            Debug.LogError("This script expects that you have a WorldAnchorManager component in your scene.");
        }

        spatialMappingManager = SpatialMappingManager.Instance;
        if (spatialMappingManager == null)
        {
            Debug.LogError("This script expects that you have a SpatialMappingManager component in your scene.");
        }
    }

    private void Update()
    {
        if (!firstVideoHider || !video1 || !firstVideoData || !secondVideoData  || !thirdVideoData)
        {
            fetchVideoHiders();
            fetchVideos();
            findDatas();
            loadSizes();
            applySavedVideoSizes();
            hideVideos();
        }

        if (video3.transform.localScale.x == 0.0f && thirdVideoHider.showing)
        {
            video3.transform.localScale = defaultSize;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void setVideoSize(Vector3 newSize, string videoNumber)
    {
        if (videoNumber == "1")
        {
            firstVideoSize = newSize;
            freeAnchor(firstVideoData);
            firstVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName = "DBV1" + firstVideoSize.ToString("F4");
            lockAnchor(firstVideoData, "DBV1" + firstVideoSize.ToString("F4"));
        }

        if (videoNumber == "2")
        {
            secondVideoSize = newSize;
            freeAnchor(secondVideoData);
            secondVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName = "DBV2" + secondVideoSize.ToString("F4");
            lockAnchor(secondVideoData, "DBV2" + secondVideoSize.ToString("F4"));
        }
        if (videoNumber == "3")
        {
            thirdVideoSize = newSize;
            freeAnchor(thirdVideoData);
            thirdVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName = "DBV3" + thirdVideoSize.ToString("F4");
            lockAnchor(thirdVideoData, "DBV3" + thirdVideoSize.ToString("F4"));
        }
        
    }

    public void setDefaultSize()
    {
        firstVideoSize = defaultSize;
        secondVideoSize = defaultSize;
        thirdVideoSize = defaultSize;
    }

    public void applySavedVideoSizes()
    {
        firstVideoHider.previousSize = firstVideoSize;
        secondVideoHider.previousSize = secondVideoSize;
        thirdVideoHider.previousSize = thirdVideoSize;
    }

    public void fetchVideoHiders()
    {
        firstVideoHider = GameObject.Find("wendake1").GetComponent<Hider>();
        secondVideoHider = GameObject.Find("wendake2").GetComponent<Hider>();
        thirdVideoHider = GameObject.Find("wendake3").GetComponent<Hider>();
    }

    public void fetchVideos()
    {
        video1 = GameObject.Find("wendake1");
        video2 = GameObject.Find("wendake2");
        video3 = GameObject.Find("wendake3");
    }

    public void loadSizes()
    {
        if (firstVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName != "FirstVideoData")
        {
            firstVideoSize = stringToVector3(firstVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));
        }
        else
        {
            firstVideoSize = defaultSize;
        }

        if (secondVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName != "SecondVideoData")
        {
            secondVideoSize = stringToVector3(secondVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));
        }
        else
        {
            secondVideoSize = defaultSize;
        }

        if (thirdVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName != "ThirdVideoData")
        {
            thirdVideoSize = stringToVector3(thirdVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));
        }
        else
        {
            thirdVideoSize = defaultSize;
        }

        video1.transform.localScale = firstVideoSize;
        video2.transform.localScale = secondVideoSize;
        video3.transform.localScale = thirdVideoSize;
    }

    private void freeAnchor(GameObject videoData)
    {
        anchorManager.RemoveAnchor(videoData);
    }

    private void lockAnchor(GameObject videoData, string size)
    {
        anchorManager.AttachAnchor(videoData, size);
    }

    private void hideVideos()
    {
        firstVideoHider.hide();
        secondVideoHider.hide();
        thirdVideoHider.hide();
    }

    public Vector3 stringToVector3(string vector3)
    {
        if (vector3.StartsWith("(") && vector3.EndsWith(")"))
        {
            vector3 = vector3.Substring(1, vector3.Length - 2);
        }

        string[] sArray = vector3.Split(',');

        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }

    private void findDatas()
    {
        datas = GameObject.FindGameObjectsWithTag("Data");
        foreach (var data in datas)
        {
            if (data.name == "FirstVideoData" || data.name[3] == '1')
            {
                firstVideoData = data;
            }
            if (data.name == "SecondVideoData" || data.name[3] == '2')
            {
                secondVideoData = data;
            }
            if (data.name == "ThirdVideoData" || data.name[3] == '3')
            {
                thirdVideoData = data;
            }
        }
    }
}

