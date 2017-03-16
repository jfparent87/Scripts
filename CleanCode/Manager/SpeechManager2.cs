using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.SceneManagement;

public class SpeechManager2 : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public List<VideoController> videoControllers;
    public List<TapToPlaceGhost> ghostZones;

    private RoomManager roomManager;

    void Start()
    {
        roomManager = GetComponent<RoomManager>();

        keywords.Add("Reset world", () =>
        {
            SceneManager.LoadScene("Scene002");
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
            foreach (var ghostZone in ghostZones)
            {
                ghostZone.resetTargetPosition();
            }
        });

        keywords.Add("Enter edition mode", () =>
        {
            roomManager.editionMode = true;
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
        });

        keywords.Add("Enter play mode", () =>
        {
            roomManager.editionMode = false;
            foreach (var videoController in videoControllers)
            {
                videoController.restartVideo();
            }
        });

        // Tell the KeywordRecognizer about our keywords.
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());

        // Register a callback for the KeywordRecognizer and start recognizing!
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
