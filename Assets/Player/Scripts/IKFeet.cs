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
        [System.NonSerialized] public bool setup;

        // Raycast
        [Header("Raycast")]
        public LayerMask RaycastLayers;
        public float RaycastDistance;

        // Configuration
        [Header("Configuration")]
        public Vector2 maxTargetBounds;
        public float forwardOffset;

        // References
        [Header("References")]
        public TwoBoneIKConstraint reference; 
        public GameObject target;
        public GameObject hint;
        Rigidbody rb;

        // Read Only
        [System.NonSerialized] public TwoBoneIKConstraintData data;
        [System.NonSerialized] public GameObject root;
        [System.NonSerialized] public GameObject mid;
        [System.NonSerialized] public GameObject tip;

        // Movement Variables
        public float tf;
        public float dff;
        [Range(0f, 1f)]
        public float percJump;

        /// <summary>
        /// Setup Joints and Feet
        /// </summary>
        public IEnumerator Setup(){
            Debug.Log("Setting up IK handles...");

            rb = reference.transform.root.GetComponent<Rigidbody>();
            setup = false;

            yield return new WaitForSeconds(5); // Wait for IK to setup

            // Storage
            data = reference.data;
            root = data.root.gameObject;
            mid = data.mid.gameObject;
            tip = data.tip.gameObject;

            // Reference
            reference.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

            // Setup
            setup = true;

            Debug.Log("Finished setting up IK handles!");
        }

        /// <summary>
        /// Update Joints and Feet
        /// </summary>
        public void Update(float t, PlayerMovement move){
            if (setup){
                //if (rb.freezeRotation) rb.freezeRotation = false;

                // Raycast
                RaycastHit hit;

                Physics.Raycast(
                    root.transform.position, 
                    -Vector3.up, 
                    out hit, 
                    RaycastDistance, 
                    RaycastLayers
                );

                // Hit point
                Vector3 hp = hit.point;

                // Distance from floor
                dff = Vector3.Distance(root.transform.position, hp);

                // Percentage of jump
                //percJump = ((move.GetCurrentJumpHeight()*move.GetCurrentGravity())/dff);

                // Automatic movement handling (i think this part is pretty nifty!)
                tf += t;
                target.transform.position = hp + reference.transform.forward * forwardOffset; /*- new Vector3(
                    0f, 
                    Mathf.Lerp(
                        0f, maxTargetBounds.y,
                        dff
                    ),
                    0f
                );*/

                // Hint
                hint.transform.position = reference.transform.position + reference.transform.forward * forwardOffset;
            } else {
                // Keep the rigidbody upright until joint is setup
                //rb.freezeRotation = true;
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
