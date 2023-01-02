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
        [Header("Configuration")]
        [Header("Curve")]
        public float distance;
        public float height;
        
        [Header("Animation")]
        public float range;
        public Vector2 duration;

        Vector2 at; // animation timer
        bool playing; // is animation playing?
        bool wasInRange; // was in foot in range of goal?

        // Target the foot needs to reach
        Vector3 goal;

        // Last recorded position when new goal position was calculated
        Vector3 lastPos;
       
        /// <summary>
        /// Calculates raycast height to allow the foot to adhere to terrain
        /// </summary>
        Vector3 CalculateRaycastHeight(
            int i, IKFeet feet){

            // Transform References
            Transform root = joints.GetRootTransform(i);
            Transform mid = joints.GetMidTransform(i);
            Transform tip = joints.GetTipTransform(i);

            // Raycast
            RaycastHit hit;

            Physics.Raycast(
                root.position,
                -Vector3.up, 
                out hit, 
                feet.RaycastDistance,
                feet.RaycastLayers
            );

            // Record Y only
            Vector3 result = new Vector3(
                0f,
                hit.point.y,
                0f
            );

            return result;
        }

        /// <summary>
        /// Calculate the foot's animation
        /// </summary>
        Vector3 CalculateAnimation(Transform body_transform, Vector3 velocity, float t){
            // Animation is playing
            if (at.x < 1f || at.y < 1f){
                playing = true;
            } else {
                playing = false;
            }

            // Position of body
            Vector3 bodyPos = body_transform.position;
            bodyPos.y = 0f; // Set the y to 0 since we don't need to track height since the raycast takes care of this (for now atleast)
           
            // So long as the animation isn't playing we can create new goals
            if (!playing){
                // Distance from body to current goal
                float CurrentDistance = Vector3.Distance(bodyPos, goal);

                // body is in range?
                if (!wasInRange) wasInRange = (CurrentDistance < range/2f); 

                // Recalculate goal
                    // Moved away from goal after getting into range of it
                    // Goal hasn't been set yet (Will cause "bouncing" behaviour of foot)
                    // Body moved too far away from goal (need improvement)
                if (CurrentDistance > range && wasInRange || goal == Vector3.zero || CurrentDistance > range * 2f){
                    // Record current goal position
                    lastPos = new Vector3(
                        goal.x,
                        goal.y,
                        goal.z
                    );

                    // Reset animation timer
                    at = Vector2.zero;
                }
            } else {
                // increment animation timer
                at += new Vector2(t / duration.x, t / duration.y);

                // Assign a new goal position
                goal = bodyPos + ((velocity / 2f) * distance);

                // goal is no longer in range
                wasInRange = false;
            }

            // Curve in wanted direction towards goal
            Vector3 curve = Unitilities.Maths.GetCurve(
                lastPos, 
                goal, 
                body_transform.up * height,
                at
            );

            return curve;
        }

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(IKFeet feet, float t, Vector3 velocity){
            for (int i = 0; i < joints.GetJoints(); i++){
                // Current transform of body
                Transform body_transform = joints.GetReferenceTransform(i);

                // Target
                Vector3 target = CalculateAnimation(
                    body_transform,
                    velocity,
                    t
                ) + CalculateRaycastHeight(
                    i,
                    feet
                );

                // Hint
                Vector3 hint = joints.GetHintPosition(i) + body_transform.forward;

                Debug.DrawLine(target, Vector3.zero, Color.red, 0.01f);

                // Modify
                joints.Modify(i, target, hint);
            }
        }
    }
}
