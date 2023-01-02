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
        [Header("List o' Feet")]
        public List<IKFoot> Feet;

        [Header("Configuration")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;
        public float RaycastTipForward;
        public float RaycastOriginForward;

        public void Send(Vector3 velocity){
            for (int i = 0; i < Feet.Count; i++){
                Feet[i].Update(this, Time.deltaTime, velocity);
            }
        }
    }
}