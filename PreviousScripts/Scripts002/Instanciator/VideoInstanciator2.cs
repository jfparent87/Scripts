using UnityEngine;
using System.Collections;

public class VideoInstanciator2 : Instanciator2 {

    public override void OnSelect()
    {
        if (!isCreated)
        {
            instantiatedObject = (GameObject)Instantiate(video, position, rotation);
            instantiatedObject.transform.parent = gameObject.transform.parent;
            isCreated = true;
        }
        else
        {
            instantiatedObject.GetComponent<VideoController>().movie.Stop();
            position = instantiatedObject.transform.position;
            rotation = instantiatedObject.transform.rotation;
            Destroy(instantiatedObject);
            isCreated = false;
        }
    }
    public override void resetPosition()
    {
        position = this.transform.position;
        position.z = position.z + 0.02f;
        position.y = position.y + 0.35f;
        rotation = this.gameObject.transform.rotation;
        rotation.w -= 1.0f;
        rotation.x = 0.0f;
        rotation.y -= 0.7f;
        rotation.z += 0.7f;
    }
}