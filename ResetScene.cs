using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

    void OnSelect()
    {
        resetScene();

    }

    public void resetScene() {
        SceneManager.LoadScene("VideoTest");
    } 
}
