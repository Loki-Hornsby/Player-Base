/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;
using Player;

/// <summary>
/// This is a child of the IKBody
/// </summary>

namespace IK {
    public enum IKTypes {
        Feet
    }

    [Serializable]
    public class IKChild {
        [Header("General")]
        public P_Controller controller;
        public IKTypes Type;
        public Transform Container;
        dynamic[] joints;

        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;

        [Header("Animation Curve")]
        public float forward;
        public float back;
        public float height;
        public Vector2 duration;

        /// <summary>
        /// Return the desired IK2 script
        /// </summary>
        public System.Type GetIK2_Script(){
             switch (Type){
                default: // Feet
                    return typeof(IK2Foot);
            }
        }

        /// <summary>
        /// Return the desired player script
        /// </summary>
        public System.Type GetP_Script(){
            switch (Type){
                default: // Feet
                    return typeof(P_Movement);
            }
        }

        /// <summary>
        /// Return the desired parameters
        /// </summary>
        dynamic[] GetParams(P_Controller controller){
            switch (Type){
                default: // Feet
                    return new dynamic[]{ 
                        this
                    };
            }
        }

        /// <summary>
        /// Setup the child
        /// </summary>
        public void Setup(){
            // Define types we want to find and create
            System.Type find = typeof(TwoBoneIKConstraint); 
            System.Type create = typeof(IKJoint); 
            
            // Find the components and arguments we want
            dynamic[] child_components = IKHelpers.Find(Container.GetComponent<MonoBehaviour>(), find);
            dynamic[] child_args = GetParams(controller);

            // Create joins array
            joints = new dynamic[child_components.Length];

            // Iterate through the components
            for (int i = 0; i < joints.Length; i++){
                /// form a new class of type <create> and pass our <child_components> and <child_args> to it
                joints[i] = IKHelpers.Create(
                    ref create, // Class to create
                    child_components[i], // Component
                    child_args // Arguments
                );
            }
        }
        
        /// <summary>
        /// Update the child
        /// </summary>
        public void Update(){
            if (joints != null){
                foreach (var joint in joints) {
                    joint.Update();
                }
            }
        }
    }
}