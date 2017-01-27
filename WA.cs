using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Persistence;
using UnityEngine.VR.WSA;

public class WA : MonoBehaviour
{
    public string AnchorName = "myAwesomeAnchor";
    WorldAnchorStore anchorStore = null;

    void Start()
    {
        WorldAnchorStore.GetAsync(AnchorStoreReady);
    }

    void AnchorStoreReady(WorldAnchorStore store)
    {
        anchorStore = store;
        if (!AttachToCachedAnchor(AnchorName))
        {
            Debug.Log("Making gnu anchor");
            WorldAnchor anchor = GetComponent<WorldAnchor>();
            if (anchor == null)
            {
                anchor = gameObject.AddComponent<WorldAnchor>();
            }

            if (anchor.isLocated)
            {
                anchorStore.Save(AnchorName, anchor);
            }
            else
            {
                anchor.OnTrackingChanged += Anchor_OnTrackingChanged;
            }
        }

    }

    private void Anchor_OnTrackingChanged(WorldAnchor self, bool located)
    {
        if (located)
        {
            self.OnTrackingChanged -= Anchor_OnTrackingChanged;
            anchorStore.Save(AnchorName, self);
        }
    }

    bool AttachToCachedAnchor(string AnchorName)
    {
        Debug.Log("Looking for " + AnchorName);
        string[] ids = anchorStore.GetAllIds();
        for (int index = 0; index < ids.Length; index++)
        {
            if (ids[index] == AnchorName)
            {
                Debug.Log("Using what we have");
                WorldAnchor wa = anchorStore.Load(ids[index], gameObject);
                return true;
            }
        }

        // Didn't find the anchor.
        return false;
    }
}