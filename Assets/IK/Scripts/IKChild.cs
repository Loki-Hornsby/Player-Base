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
/// This is a child of the IKBody
/// </summary>

namespace IK {
    public enum IKTypes {
        Foot
    }

    [Serializable]
    public class IKChild {
        [Header("General")]
        public IKTypes Type;
        public Transform Container;
        [System.NonSerialized] public dynamic[] list;

        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;

        [Header("Animation")]
        [Header("Curve")]
        public float distance;
        public float height;
        
        [Header("Settings")]
        public float range;
        public Vector2 duration;


        /// <summary>
        /// Return the type for the body to find
        /// </summary>
        public System.Type GetFind(){
            switch (Type){
                default:
                    return typeof(IKJoints);
                    break;
            }
        }

        /// <summary>
        /// Return the type for the body to create
        /// </summary>
        public System.Type GetCreate(){
            switch (Type){
                default:
                    return typeof(IKFoot);
                    break;
            }
        }
    }
}