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

        [Header("Configuration")]
        public float max;
        public float height;
        public Vector2 duration;

        // Target the foot needs to reach
        Vector3 goal;

        // Last recorded position when new goal position was calculated
        Vector3 lastPos;

        // Animation timer
        Vector2 at;

        // Animation is playing
        bool playing;
       
        /*
        /// <summary>
        /// Calculate averages between 2 raycasts
        /// </summary>
        Vector3 CalculateRaycastAverage(
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
        Vector3 CalculateTargetPosition(Vector3 walk, Vector3 avg){
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
        Vector3 CalculateWalkAnimResult(IKFeet feet, int index){
            return ( // Calculates walk motion (produces a kind of circle shape)
                new Vector3( 
                    Unitilities.Maths.CalculateSinOrCos(
                        feet.speed, feet.amplitude, false),
                    Unitilities.Maths.CalculateSinOrCos(
                        feet.speed, feet.amplitude, true) * -1f,
                    0f

                ) * ((index % 2f == 0f) ? 1f : -1f) // Allows the player to moonwalk haha!
            );
        }*/

        /// <summary>
        /// Calculate a new goal position
        /// </summary>
        Vector3 CalculateNewGoal(Vector3 pos, Vector3 offset){
            return pos + offset;
        }

        /// <summary>
        /// Calculate target position
        /// </summary>
        Vector3 CalculateTarget(int i, float t, Vector3 velocity){
            // Animation is playing
            if (at.x < 1f || at.y < 1f){
                playing = true;
            } else {
                playing = false;
            }

            // Current transform of reference
            Transform reference_transform = joints.GetReferenceTransform(i);
           
            // So long as the animation isn't playing we can create new goals
            if (!playing){
                // Current position of target
                Vector3 current_target_pos = joints.GetTargetPosition(i);

                // Distance from body to  current goal
                float CurrentDistance = Vector3.Distance(reference_transform.position, goal);

                // Distance from previous goal to body
                float LastDistance = Vector3.Distance(lastPos, reference_transform.position);

                // Recalculate goal and reset lerp timer
                if (CurrentDistance > max && LastDistance > CurrentDistance || goal == Vector3.zero){
                    // Reset animation timer
                    at = Vector2.zero;

                    // Record current goal position
                    lastPos = new Vector3(
                        goal.x,
                        goal.y,
                        goal.z
                    );

                    // Create a new goal position
                    goal = CalculateNewGoal(
                        new Vector3(
                            reference_transform.position.x,
                            0f,
                            reference_transform.position.z
                        ), 
                        reference_transform.forward * max
                    );
                }
            } else {
                // increment animation timer
                at += new Vector2(t / duration.x, t / duration.y);
            }

            // Curve in wanted direction towards goal
            return Unitilities.Maths.GetCurve(
                lastPos, 
                goal, 
                reference_transform.up * height,
                at
            );
        }

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(IKFeet feet, int index, float t, Vector3 velocity){
            for (int i = 0; i < joints.GetJoints(); i++){
                // Target
                /*Vector3 target = CalculateTargetPosition(
                    walkAnim,

                    CalculateRaycastAverage(
                        i,
                        feet.RaycastLayers,
                        feet.RaycastDistance,
                        feet.RaycastTipForward,
                        feet.RaycastOriginForward
                    )
                );*/

                Vector3 target = CalculateTarget(i, t, velocity);

                Debug.DrawLine(Vector3.zero, target, Color.red, 0.1f);

                // Hint
                Vector3 hint = joints.GetHintPosition(i);

                // Modify
                joints.Modify(i, target, hint);
            }
        }
    }
}
