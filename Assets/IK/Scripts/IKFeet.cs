/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using UnityEngine.Animations.Rigging;

/// <summary>
/// Handles Procedural Feet Animation using the Animations.Rigging package
/// </summary>

[RequireComponent(typeof(Rig))]
public class IKFeet : MonoBehaviour {
    [Serializable]
    public class Foot {
        [Serializable]
        public class Joint {
            // General
            bool setup;
            [System.NonSerialized] public float t;

            [Header("References")]
            public TwoBoneIKConstraint reference; 
            public GameObject target;
            public GameObject hint;

            [Header("Configuration")]
            public float forwardOffset;
            public float speed;
            public float amplitude;
            
            // Read Only
            [System.NonSerialized] public TwoBoneIKConstraintData data;
            [System.NonSerialized] public GameObject root;
            [System.NonSerialized] public GameObject mid;
            [System.NonSerialized] public GameObject tip;

            /// <summary>
            /// Check if joint is setup
            /// </summary>
            public bool isSetup(){
                return setup;
            }

            /// <summary>
            /// Setup joint
            /// </summary>
            public void Setup(){
                reference.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                setup = false;
            }

            /// <summary>
            /// Complete setup of joint
            /// </summary>
            public void Complete(){
                // Storage
                data = reference.data;
                root = data.root.gameObject;
                mid = data.mid.gameObject;
                tip = data.tip.gameObject;

                setup = true;
            }
        }

        bool setup;

        // Raycast
        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;
        public float RaycastTipForward;
        public float RaycastOriginForward;

        // References
        [Header("Joints")]
        public List<Joint> joints;

        /// <summary>
        /// Check if foot is setup
        /// </summary>
        public bool isSetup(){
            return setup;
        }

        /// <summary>
        /// Setup Joints and Feet
        /// </summary>
        public IEnumerator Setup(){
            Debug.Log("Setting up IK handles...");

            setup = false;

            foreach (var joint in joints){
                // Setup Joint
                joint.Setup();

                yield return new WaitForSeconds(5); // Wait for IK to setup

                // Complete Setup
                joint.Complete();
            }

            setup = true;

            Debug.Log("Finished setting up IK handles!");
        }

        /// <summary>
        /// Calculate the magic sin number for the movement animation
        /// </summary>
        float CalculateAnimation(float speed, float amplitude, bool cos){
            float val = 0f;
            float p = Time.fixedTime * Mathf.PI * speed;

            if (cos){
                val += Mathf.Cos(p);
            } else {
                val += Mathf.Sin(p);
            }

            return (val * amplitude);
        }

        /// <summary>
        /// Update Joints and Feet
        /// </summary>
        public void Update(float t, P_Movement movement, int index){
            if (setup){
                foreach (var joint in joints){
                    if (joint.isSetup()){
                        // Raycast Origin
                        Vector3 OriginForward = joint.reference.transform.forward * RaycastOriginForward;
                        RaycastHit hitOrigin;

                        Physics.Raycast(
                            joint.root.transform.position + OriginForward,
                            -Vector3.up, 
                            out hitOrigin, 
                            RaycastDistance, 
                            RaycastLayers
                        );

                        // Raycast Tip
                        Vector3 TipForward = joint.reference.transform.forward * RaycastTipForward;
                        RaycastHit hitTip;

                        Physics.Raycast(
                            joint.root.transform.position + TipForward,
                            -Vector3.up, 
                            out hitTip, 
                            RaycastDistance, 
                            RaycastLayers
                        );

                        // Hit points
                        Vector3 hpA = hitOrigin.point - OriginForward;
                        Vector3 hpB = hitTip.point - TipForward;

                        // AVG between hit points
                        Vector3 hpAvg = (hpA + hpB) / 2f;

                        // Forward offset
                        Vector3 ForwardOffset = (joint.reference.transform.forward * joint.forwardOffset);

                        // Min and Max (Foot Walk Cycle Animation (F.W.C.A ~ O.W.C.A anyone?))
                        Vector3 min = hpAvg + ForwardOffset; // Adds offsets
                        Vector3 max = min + ( // Calculates walk motion (produces a kind of circle shape)
                            new Vector3( // *-1f so the foot goes in the correct direction
                                CalculateAnimation(movement.velocity.x, joint.amplitude/2f, true) * -1f, // Cos : true
                                CalculateAnimation(movement.velocity.x, joint.amplitude/2f, false), // Sin : false
                                0f
                            ) * ((index % 2f == 0f) ? 1f : -1f) // Allows the player to moonwalk haha!
                        );

                        // Reset height
                        if (max.y < min.y) max.y = min.y;

                        // Target Lerp
                        joint.t = t / 0.25f;
                        joint.target.transform.position = Vector3.Lerp(joint.target.transform.position, max, joint.t);

                        // Hint
                        joint.hint.transform.position = joint.target.transform.position;
                    }
                }
            }
        }
    }   

    [System.NonSerialized] public bool setup;
    int index;

    [Header("References")]
    public P_Movement movement;

    [Header("List o' Feet")]
    public List<Foot> Feet;

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
            foot.Update(Time.deltaTime, movement, index);
        }
    }
}
