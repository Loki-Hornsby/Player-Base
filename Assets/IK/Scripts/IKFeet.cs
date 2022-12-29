/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

/// <summary>
/// Handles Procedural Feet Animation using the Animations.Rigging package
/// </summary>

namespace IK {
    [RequireComponent(typeof(Rig))]
    public class IKFeet : MonoBehaviour {
        [System.NonSerialized] public bool setup;
        int index;

        [Header("Configuration")]
        [Range(-1f, 1f)] public float speed;
        [Range(-4f, 4f)] public float speedOffset;
        [Range(-1f, 1f)] public float amplitude;
        [Range(-4f, 4f)] public float amplitudeOffset;
        [Range(-4f, 4f)] public float cosOffset;
        [Range(-4f, 4f)] public float sinOffset;

        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;
        public float RaycastTipForward;
        public float RaycastOriginForward;

        [Header("List o' Feet")]
        public List<IKFoot> Feet;

        /// <summary>
        /// Setup Feet
        /// </summary>
        void Awake(){
            foreach (var foot in Feet){
                StartCoroutine(foot.Setup());
            }
        }

        /// <summary>
        /// Update Feet
        /// </summary>
        void Update(){
            index = 0;

            // Feet
            foreach (var foot in Feet){
                // Update index counter
                if (foot.isSetup()) index++;

                // Setup condition
                if (!setup && index == Feet.Count) setup = true;
                
                // Update feet
                foot.Update(this, index, Time.deltaTime);
            }
        }

        /// <summary>
        /// Get Walk Animation Average
        /// </summary>
        public Vector3 GetWalkAnimAverage(){
            Vector3 result = Vector3.zero;

            foreach (var foot in Feet){
                foreach (var joint in foot.Joints){
                    result = result + joint.walkAnim;
                }
            }

            return result;
        }
    }
}