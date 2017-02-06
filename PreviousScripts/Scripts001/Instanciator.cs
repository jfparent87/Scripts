using UnityEngine;
using System.Collections;

public class Instanciator : MonoBehaviour
{

    public bool isCreated = false;
    public GameObject gameObject;
    public GameObject instantiatedObject;
    public Vector3 position;
    public Quaternion rotation;

    void OnSelect()
    {
        if (!isCreated)
        {
            resetPosition();
            instantiateObject();
        }
        else
        {
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }

    public void show()
    {
        if (!isCreated)
        {
            resetPosition();
            instantiateObject();
        }
    }

    public void hide()
    {
        if (!isCreated)
        {
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }

    public void resetPosition()
    {
        position = Camera.main.transform.position;
        position.z = 2.0f;
        position.y = -0.3f;
        rotation = this.gameObject.transform.rotation;
    }

    public void instantiateObject()
    {
        instantiatedObject = (GameObject)Instantiate(gameObject, position, rotation);
        instantiatedObject.transform.parent = this.transform.parent;
        isCreated = true;
    }
}