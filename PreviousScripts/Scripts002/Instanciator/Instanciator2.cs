using UnityEngine;

abstract public class Instanciator2 : MonoBehaviour
{

    public bool isCreated = false;
    public GameObject video;
    public GameObject instantiatedObject;
    public Vector3 position;
    public Quaternion rotation;
    public float proximityTrigger = 2.5f;
    float distance;

    void Start()
    {
        resetPosition();
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        InvokeRepeating("verifyDistance", 2.0f, 2.0f);
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    virtual public void OnSelect()
    {
        if (!isCreated)
        {
            instantiatedObject = (GameObject)Instantiate(video, position, rotation);
            instantiatedObject.transform.parent = gameObject.transform.parent;
            isCreated = true;
        }
        else
        {
            //instantiatedObject.GetComponent<VideoController>().movie.Stop();
            position = instantiatedObject.transform.position;
            rotation = instantiatedObject.transform.rotation;
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }

    virtual public void resetPosition()
    {
        position = this.transform.position;
        position.z = position.z + 0.02f;
        position.y = position.y + 0.35f;
    }

    public void instantiateToPlace()
    {
        instantiatedObject = (GameObject)Instantiate(video, position, rotation);
        instantiatedObject.transform.parent = gameObject.transform.parent;
        isCreated = true;
        instantiatedObject.GetComponent<VideoController>().stopVideo();
    }

    public void proximityInstanciate()
    {
        if (!isCreated)
        {
            instantiatedObject = (GameObject)Instantiate(video, position, rotation);
            instantiatedObject.transform.parent = gameObject.transform.parent;
            isCreated = true;
        }
    }

    public void proximityDestroy()
    {
        if (isCreated)
        {
            instantiatedObject.GetComponent<VideoController>().movie.Stop();
            position = instantiatedObject.transform.position;
            rotation = instantiatedObject.transform.rotation;
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }

    void verifyDistance()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < proximityTrigger && !isCreated)
        {
            proximityInstanciate();
        }
        if (distance > proximityTrigger && isCreated)
        {
            proximityDestroy();
        }
    }
}