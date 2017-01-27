using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using System.Collections.Generic;
using UnityEngine.VR.WSA;

namespace HoloToolkit.Unity
{
    public class WorldAnchorManager : MonoBehaviour
    {
        // removed some lines from original script
        public WorldAnchorStore AnchorStore { get; private set; }

        void Awake()
        {
            Debug.Log("awake");
            AnchorStore = null;
            WorldAnchorStore.GetAsync(AnchorStoreReady);
        }

        private void AnchorStoreReady(WorldAnchorStore Store)
        {
            Debug.Log("Anchor store ready");
            AnchorStore = Store;
        }
    }
}