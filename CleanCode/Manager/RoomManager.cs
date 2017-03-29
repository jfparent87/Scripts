using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public bool editionMode = false;
    public List<Hider> editionObjects;
    public bool show = true;

    private void Update()
    {
        if (editionMode && !show)
        {
            for (int i = 0; i < editionObjects.Count; i++)
            {
                editionObjects[i].show();
            }
            show = true;
        }
        if(!editionMode && show)
        {
            for (int i = 0; i < editionObjects.Count; i++)
            {
                editionObjects[i].hide();
            }
            show = false;
        }
    }
}
