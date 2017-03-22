using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechManager2 : MonoBehaviour
{
    KeywordRecognizer keywordRecognizer = null;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public ResetManager resetManager;
    public EditionManager editionManager;
    public List<VideoController> videoControllers;
    public List<TapToPlaceGhost> ghostZones;
    public List<VideoHider> videoHiders;
    public List<Hider> hiders;

    private RoomManager roomManager;

    void Start()
    {
        roomManager = GetComponent<RoomManager>();

        keywords.Add("Reset scene", () =>
        {
            resetManager.OnSelect();
        });

        keywords.Add("Enter edition mode", () =>
        {
            roomManager.editionMode = true;
            editionManager.enterEditionMode();
        });

        keywords.Add("Enter play mode", () =>
        {
            roomManager.editionMode = false;
            editionManager.enterPlayMode();
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
