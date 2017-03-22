using UnityEngine;
using HoloToolkit.Unity;
using HoloToolkit.Unity.SpatialMapping;

public class Debugger : MonoBehaviour {

    public RoomManager roomManager;
    public TextMesh hololensDebugLine1;
    public TextMesh hololensDebugLine2;
    private Hider hider;
    protected WorldAnchorManager anchorManager;
    protected SpatialMappingManager spatialMappingManager;

    void Start () {
        hider = GetComponent<Hider>();
	}
	
	void Update () {

        if (roomManager.editionMode && !hider.showing)
        {
            hider.show();
        }

        if (!roomManager.editionMode && hider.showing)
        {
            hider.hide();
        }

        if (WorldAnchorManager.Instance.AnchorStore != null)
        {
            var ids = WorldAnchorManager.Instance.AnchorStore.GetAllIds();
            hololensDebugLine1.text = "Loaded Anchors IDs: ";

            if (ids.Length > 0)
            {
                foreach (var id in ids)
                {
                    hololensDebugLine1.text = hololensDebugLine1.text + System.Environment.NewLine + id;
                }
            }
        }
    }
}
