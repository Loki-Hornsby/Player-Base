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

            [Header("References")]
            public TwoBoneIKConstraint reference; 
            public GameObject target;
            public GameObject hint;

            [Header("Offsets")]
            public float forwardOffset;
            public float heightOffset;
            
            // Read Only
            [System.NonSerialized] public TwoBoneIKConstraintData data;
            [System.NonSerialized] public GameObject root;
            [System.NonSerialized] public GameObject mid;
            [System.NonSerialized] public GameObject tip;

            public bool isSetup(){
                return setup;
            }

            public void Setup(){
                reference.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                setup = false;
            }

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

        // Configuration
        [Header("Configuration")]
        public Vector2 maxTargetBounds;

        // References
        [Header("Joints")]
        public List<Joint> joints;

        // Movement Variables
        float tf;
        float dff;
        float percJump;

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
        /// Update Joints and Feet
        /// </summary>
        public void Update(float t, PlayerMovement move){
            if (setup){
                foreach (var joint in joints){
                    if (joint.isSetup()){
                        // Raycast
                        RaycastHit hit;

                        Physics.Raycast(
                            joint.root.transform.position, 
                            -Vector3.up, 
                            out hit, 
                            RaycastDistance, 
                            RaycastLayers
                        );

                        // Hit point
                        Vector3 hp = hit.point;

                        // Distance from floor
                        dff = Vector3.Distance(joint.root.transform.position, hp);

                        // Percentage of jump
                        //percJump = ((move.GetCurrentJumpHeight()*move.GetCurrentGravity())/dff);

                        // Offsets
                        Vector3 HO = new Vector3(0f, joint.heightOffset, 0f);
                        Vector3 FO = joint.reference.transform.forward * joint.forwardOffset;

                        // Automatic movement handling (i think this part is pretty nifty!)
                        tf += t;
                        joint.target.transform.position = hp + HO + FO; 
                            
                            /*- new Vector3(
                            0f, 
                            Mathf.Lerp(
                                0f, maxTargetBounds.y,
                                dff
                            ),
                            0f
                        );*/

                        // Hint
                        joint.hint.transform.position = joint.root.transform.position + HO + FO;
                    }
                }
            }
        }
    }   

    [Header("References")]
    public PlayerMovement move;

    [Header("List o' Feet")]
    public List<Foot> Feet;

    //public void 

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
        // Feet
        foreach (var foot in Feet){
            foot.Update(Time.deltaTime, move);
        }
    }
}
