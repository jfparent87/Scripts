using UnityEngine;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {

    public bool editionMode = false;
    public List<Hider> buttons;
    public bool show = true;

    private void Update()
    {
        if (editionMode && !show)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].show();
            }
            show = true;
        }
        if(!editionMode && show)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].hide();
            }
            show = false;
        }
    }
}
