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
    public class IK2Foot {
        // References
        IKChild child;

        // Animation
        Vector2 at; // animation timer
        bool playing; // is animation playing?
        bool wasInRange; // was in foot in range of goal?

        // Target the foot needs to reach
        Vector3 goal;

        // Last recorded position when new goal position was calculated
        Vector3 lastPos;

        /// <summary>
        /// Constructor
        /// </summary>
        public IK2Foot(params dynamic[] parameters){
            // References
            child = parameters[0];
        }

        /// <summary>
        /// Calculates raycast height to allow the foot to adhere to terrain
        /// </summary>
        Vector3 CalculateRaycastHeight(TwoBoneIKConstraint constraint){
            // Transform References
            Transform root = constraint.data.root.transform;
            Transform mid = constraint.data.mid.transform;
            Transform tip = constraint.data.tip.transform;

            // Raycast
            RaycastHit hit;

            Physics.Raycast(
                root.position,
                -Vector3.up, 
                out hit, 
                child.RaycastDistance,
                child.RaycastLayers
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
        Vector3 CalculateAnimation(TwoBoneIKConstraint constraint, Vector3 direction){
            // Animation is playing
            if (at.x < 1f || at.y < 1f){
                playing = true;
            } else {
                playing = false;
            }

            // Position of body
            Vector3 bodyPos = constraint.transform.position;
            bodyPos.y = 0f; // Set the y to 0 since we don't need to track height since the raycast takes care of this (for now atleast)
           
            // So long as the animation isn't playing we can create new goals
            if (!playing){
                // Distance from body to current goal
                float CurrentDistance = Vector3.Distance(bodyPos, goal);

                // body is in range?
                if (!wasInRange) wasInRange = (CurrentDistance < child.range/4f); 

                // Recalculate goal
                    // Moved away from goal after getting into range of it
                    // Goal hasn't been set yet (Will cause "bouncing" behaviour of foot)
                    // Body moved too far away from goal (need improvement)
                if (CurrentDistance > child.range && wasInRange || goal == Vector3.zero || CurrentDistance > child.range * 2f){
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
                at += new Vector2(
                    (Time.deltaTime / child.duration.x), //* (velocity.magnitude / 2f),
                    (Time.deltaTime / child.duration.y) //* (velocity.magnitude / 2f)
                );

                // Assign a new goal position
                goal = bodyPos + (direction * child.distance);

                // goal is no longer in range
                wasInRange = false;
            }

            // Curve in wanted direction towards goal (adding the raycast to it as well)
            Vector3 curve = Unitilities.Maths.GetCurve(
                lastPos, 
                goal, 
                constraint.transform.up * child.height,
                at
            ) + CalculateRaycastHeight(
                constraint
            );

            return curve;
        }

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(IKJoint joint){
            TwoBoneIKConstraint constraint = joint.constraint;

            // Target
            joint.constraint.data.target.transform.position = CalculateAnimation(
                constraint,
                child.controller.movement.direction
            ); // (Not working!?)

            // Hint
            joint.constraint.data.hint.transform.position = joint.constraint.data.target.transform.position;

            Debug.DrawLine(joint.constraint.data.target.transform.position, Vector3.zero, Color.red, 0.01f);
        }
    }
}
