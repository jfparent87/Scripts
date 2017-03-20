using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;


public class Database : MonoBehaviour
{

    public static Database Instance;
    public Vector3 firstVideoSize;
    public Vector3 secondVideoSize;
    public Vector3 thirdVideoSize;
    public Vector3 defaultSize = new Vector3(0.08f, 0.02f, 0.06f);
    public TextMesh txtTextMesh;
    public GameObject firstVideoData;
    public GameObject secondVideoData;
    public GameObject thirdVideoData;
    public GameObject video1;
    public GameObject video2;
    public GameObject video3;

    private Hider firstVideoHider;
    private Hider secondVideoHider;
    private Hider thirdVideoHider;
    public GameObject[] datas;

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
        Debug.Log("applying previous sizes");
        Debug.Log("previous size 1 : " + firstVideoSize.ToString("F4"));
        Debug.Log("previous size 2 : " + secondVideoSize.ToString("F4"));
        Debug.Log("previous size 3 : " + thirdVideoSize.ToString("F4"));
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
        firstVideoSize = StringToVector3(firstVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));
        secondVideoSize = StringToVector3(secondVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));
        thirdVideoSize = StringToVector3(thirdVideoData.GetComponent<DatabaseAnchor>().SavedAnchorFriendlyName.Remove(0, 4));

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

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
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

