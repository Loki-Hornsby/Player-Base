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
/// P_Controller.cs <-- P_Movement.cs <-- * IKFeet.cs * --> IKFoot.cs --> IKJoints.cs
/// </summary>

namespace IK {
    [RequireComponent(typeof(Rig))]
    public class IKFeet : MonoBehaviour {
        [Header("Configuration")]
        public float speed;
        public float amplitude;

        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;
        public float RaycastTipForward;
        public float RaycastOriginForward;

        [Header("List o' Feet")]
        public List<IKFoot> Feet;

        /// <summary>
        /// Update Feet
        /// </summary>
        void Update(){
            for (int i = 0; i < Feet.Count; i++){
                Feet[i].Update(this, i, Time.deltaTime);
            }
        }

        /// <summary>
        /// Send data to this script
        /// </summary>
        public void Send(float _speed, float _amplitude){
            speed = _speed;
            amplitude = _amplitude;
        }

        /// <summary>
        /// Get Walk Animation Average
        /// </summary>
        public Vector3 GetWalkAnimAverage(){
            Vector3 result = Vector3.zero;

            foreach (var foot in Feet){
                result = result + foot.walkAnim;
            }

            return result;
        }
    }
}