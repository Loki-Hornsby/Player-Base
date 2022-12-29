/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

namespace IK {
    [Serializable]
    public class IKJoint {
        bool setup;

        // Animation
        float trackedTime;
        [System.NonSerialized] public Vector3 walkAnim;
        
        [Header("References")]
        public TwoBoneIKConstraint reference; 
        public GameObject target;
        public GameObject hint;
        
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

        /// <summary>
        /// Calculate averages between 2 raycasts
        /// </summary>
        public Vector3 CalculateRaycastAverage(
            LayerMask RaycastLayers, float RaycastDistance, float RaycastTipForward, float RaycastOriginForward){

            // Raycast Origin
            Vector3 OriginForward = reference.transform.forward * RaycastOriginForward;
            RaycastHit hitOrigin;

            Physics.Raycast(
                root.transform.position + OriginForward,
                -Vector3.up, 
                out hitOrigin, 
                RaycastDistance, 
                RaycastLayers
            );

            // Raycast Tip
            Vector3 TipForward = reference.transform.forward * RaycastTipForward;
            RaycastHit hitTip;

            Physics.Raycast(
                root.transform.position + TipForward,
                -Vector3.up, 
                out hitTip, 
                RaycastDistance, 
                RaycastLayers
            );

            // Hit points
            Vector3 hpA = hitOrigin.point - OriginForward;
            Vector3 hpB = hitTip.point - TipForward;

            // AVG between hit points
            return (hpA + hpB) / 2f;
        }

        /// <summary>
        /// Calculate target position
        /// </summary>
        public Vector3 CalculateTargetPosition(Vector3 walk, Vector3 avg){
            // Foot Walk Cycle Animation (F.W.C.A ~ O.W.C.A anyone?)
            Vector3 min = avg;
            Vector3 max = min + walk;
            
            // Reset height
            if (max.y < min.y) max.y = min.y;

            return max;
        }

        /// <summary>
        /// Get result from walk cycle animation formula
        /// </summary>
        public Vector3 CalculateWalkAnimResult(IKFeet feet, int index){
            return ( // Calculates walk motion (produces a kind of circle shape)
                new Vector3( 
                    0f,
                    Unitilities.Maths.CalculateSinOrCos(
                        feet.speed, feet.amplitude, false),
                    Unitilities.Maths.CalculateSinOrCos(
                        feet.speed, feet.amplitude, true) * -1f

                ) * ((index % 2f == 0f) ? 1f : -1f) // Allows the player to moonwalk haha!
            );
        }

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(IKFeet feet, int index, float t){
            if (setup){
                // Calculate Animations
                walkAnim = CalculateWalkAnimResult(feet, index);
            
                // Target
                trackedTime = t / 0.25f;

                target.transform.position = Vector3.Lerp(
                    target.transform.position, 
                    CalculateTargetPosition(
                        walkAnim,

                        CalculateRaycastAverage(
                            feet.RaycastLayers,
                            feet.RaycastDistance,
                            feet.RaycastTipForward,
                            feet.RaycastOriginForward
                        )
                    ),
                    t
                );

                // Hint
                hint.transform.position = target.transform.position;
            }
        }
    }
}
