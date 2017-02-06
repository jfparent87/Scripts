using UnityEngine;

public class ProximityTrigger : MonoBehaviour {

    public VideoInstanciator videoInstanciator;
    public float proximityTrigger;
    float distance;
    
    void Start()
    {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        InvokeRepeating("verifyDistance", 2.0f, 2.0f);
    }

    void verifyDistance () {
        distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        if (distance < proximityTrigger && !videoInstanciator.isCreated)
        {
            videoInstanciator.proximityInstanciate();
        }
        if (distance > proximityTrigger && videoInstanciator.isCreated)
        {
            videoInstanciator.proximityDestroy();
        }
    }
}
