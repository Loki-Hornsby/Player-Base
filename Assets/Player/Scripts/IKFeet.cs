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
            [System.NonSerialized] public bool setup;

            public enum Alignments {
                Tip,
                Mid,
                Root,
                Reference,
                Nothing,
                Ground
            }

            // Raycast
            [Header("Raycast")]
            public LayerMask RaycastLayers;
            public float RaycastDistance;

            // Configuration
            [Header("Configuration")]
            public Alignments AlignTargetTo;

            // References
            [Header("References")]
            public TwoBoneIKConstraint reference; 
            public GameObject target;
            public GameObject hint;

            // Read Only
            [System.NonSerialized] public TwoBoneIKConstraintData data;
            [System.NonSerialized] public GameObject root;
            [System.NonSerialized] public GameObject mid;
            [System.NonSerialized] public GameObject tip;

            public void Setup(){
                // Storage
                data = reference.data;
                root = data.root.gameObject;
                mid = data.mid.gameObject;
                tip = data.tip.gameObject;

                // Reference
                reference.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                // Setup
                setup = true;
            }

            public void Update(){
                // Target
                switch (AlignTargetTo){
                    case Alignments.Tip:
                        target.transform.position = tip.transform.position;
                        break;
                    case Alignments.Mid:
                        target.transform.position = mid.transform.position;
                        break;
                    case Alignments.Root:
                        target.transform.position = root.transform.position;
                        break;
                    case Alignments.Reference:
                        target.transform.position = reference.gameObject.transform.position;
                        break;
                    case Alignments.Ground:
                        RaycastHit hit;

                        Physics.Raycast(
                            root.transform.position, 
                            -Vector3.up, 
                            out hit, 
                            RaycastDistance, 
                            RaycastLayers
                        );

                        target.transform.position = hit.point;

                        break;
                    default:
                        break;
                }

                // Hint
                hint.transform.position = reference.transform.position + reference.transform.forward * 5f;
            }
        }

        public List<Joint> joints;

        /// <summary>
        /// Setup Joints and Feet
        /// </summary>
        public IEnumerator Setup(){
            Debug.Log("Setting up IK handles...");

            foreach (var joint in joints){
                joint.setup = false;
            }

            yield return new WaitForSeconds(5); // Wait for IK to setup

            foreach (var joint in joints){
                joint.Setup();
            }

            Debug.Log("Finished setting up IK handles!");
        }

        /// <summary>
        /// Update Joints and Feet
        /// </summary>
        public void Update(){
            foreach (var joint in joints){
                if (joint.setup) {
                    joint.Update();
                } else {
                    // Keep the local position at zero until joint is setup
                    joint.reference.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
            }
        }
    }   

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
        // Feet
        foreach (var foot in Feet){
            foot.Update();
        }
    }
}
