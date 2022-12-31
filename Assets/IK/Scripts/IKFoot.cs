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
/// P_Controller.cs <-- P_Movement.cs <-- IKFeet.cs <-- * IKFoot.cs * --> IKJoints.cs
/// </summary>

namespace IK {
    [Serializable]
    public class IKFoot {
        [Header("References")]
        public IKJoints joints;

        // Animation
        [System.NonSerialized] public Vector3 walkAnim;

        /// <summary>
        /// Calculate averages between 2 raycasts
        /// </summary>
        public Vector3 CalculateRaycastAverage(
            int i, LayerMask RaycastLayers, float RaycastDistance, float RaycastTipForward, float RaycastOriginForward){

            // Transform References
            Transform root = joints.GetRootTransform(i);
            Transform mid = joints.GetMidTransform(i);
            Transform tip = joints.GetTipTransform(i);
            Transform reference = joints.GetReferenceTransform(i);

            // Raycast Origin
            Vector3 OriginForward = reference.forward * RaycastOriginForward;
            RaycastHit hitOrigin;

            Physics.Raycast(
                root.position + OriginForward,
                -Vector3.up, 
                out hitOrigin, 
                RaycastDistance, 
                RaycastLayers
            );

            // Raycast Tip
            Vector3 TipForward = reference.forward * RaycastTipForward;
            RaycastHit hitTip;

            Physics.Raycast(
                root.position + TipForward,
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
            for (int i = 0; i < joints.GetJoints(); i++){
                // Calculate Animations
                walkAnim = CalculateWalkAnimResult(feet, index);
            
                // Target
                Vector3 target = CalculateTargetPosition(
                    walkAnim,

                    CalculateRaycastAverage(
                        i,
                        feet.RaycastLayers,
                        feet.RaycastDistance,
                        feet.RaycastTipForward,
                        feet.RaycastOriginForward
                    )
                );

                // Hint
                Vector3 hint = joints.GetHintPosition(i);

                // Modify
                joints.Modify(i, target, hint);
            }
        }
    }
}
