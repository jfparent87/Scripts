using UnityEngine;
using System.Collections.Generic;

public class ButtonsManager : MonoBehaviour {

    public List<Hider> buttons;
    public bool show = true;

    void OnSelect()
    {
        if (show)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].hide();
                show = false;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].show();
                show = true;
            }
        }
    }
}
