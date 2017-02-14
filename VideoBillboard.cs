// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

namespace HoloToolkit.Unity
{
    public enum PivotAxis
    {
        // Rotate about all axes.
        Free,
        // Rotate about an individual axis.
        Y
    }

    /// <summary>
    /// The Billboard class implements the behaviors needed to keep a GameObject 
    /// oriented towards the user.
    /// </summary>
    public class VideoBillboard : MonoBehaviour
    {
        /// <summary>
        /// The axis about which the object will rotate.
        /// </summary>
        [Tooltip("Specifies the axis about which the object will rotate.")]
        public PivotAxis PivotAxis = PivotAxis.Free;
        public Quaternion rotation;

        private void OnEnable()
        {
            Update();
        }

        /// <summary>
        /// Keeps the object facing the camera.
        /// </summary>
        private void Update()
        {
            if (!Camera.main)
            {
                return;
            }

            // Get a Vector that points from the target to the main camera.
            Vector3 directionToTarget = Camera.main.transform.position - transform.position;

            // Adjust for the pivot axis.
            switch (PivotAxis)
            {
                case PivotAxis.Y:
                    directionToTarget.y = 0.0f;
                    break;

                case PivotAxis.Free:
                default:
                    // No changes needed.
                    break;
            }

            // If we are right next to the camera the rotation is undefined. 
            if (directionToTarget.sqrMagnitude < 0.001f)
            {
                return;
            }

            // Calculate and apply the rotation required to reorient the object
            rotation = Quaternion.LookRotation(-directionToTarget);
            //rotation.w -= 1.0f;
            //rotation.x = 0.0f;
            //rotation.y -= 0.7f;
            //rotation.z += 0.7f

            rotation.w -= 1.0f;
            rotation.x = 0.0f;
            rotation.y -= 0.8f; 
            rotation.z += 0.7f;
            Debug.Log(rotation);
            transform.localRotation = rotation;
        }
    }
}