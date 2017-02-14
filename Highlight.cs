using UnityEngine;

public class Highlight : MonoBehaviour {

    public GameObject cursor;
    public bool meatTrigger = false;
    private Color startcolor;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<Collider>().name == "Cursor")
        {
            hightlightOn();
            meatTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        GetComponent<Renderer>().material.color = startcolor;
        meatTrigger = false;
    }

    void hightlightOn()
    {
        startcolor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = Color.red;
    }
    void hightlightOff()
    {
        GetComponent<Renderer>().material.color = startcolor;
    }
}
