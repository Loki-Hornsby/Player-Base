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
        // External refs
        IKJoint joint;
        TwoBoneIKConstraint constraint;
        IKChild child;

        // Constraint refs
        Transform root;
        Transform mid;
        Transform tip;

        // Animation
        Vector2 at; // animation timer
        bool playing; // is animation playing?

        // Target the foot needs to reach
        Vector3 goal;

        // Last recorded position when new goal position was calculated
        Vector3 lastPos;

        /// <summary>
        /// Constructor
        /// </summary>
        public IK2Foot(params dynamic[] parameters){
            // External refs
            joint = parameters[0];
            constraint = joint.constraint;
            child = parameters[1][0];

            // Constraint refs
            root = constraint.data.root.transform;
            mid = constraint.data.mid.transform;
            tip = constraint.data.tip.transform;
        }

        /// <summary>
        /// Calculates raycast height to allow the foot to adhere to terrain
        /// </summary>
        Vector3 CalculateRaycastHeight(){
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
        Vector3 CalculateAnimation(){
            // Direction
            Vector3 direction = child.controller.movement.direction;

            // Animation is playing
            if (at.x < 1f || at.y < 1f){
                playing = true;
            } else {
                playing = false;
            }

            // Root position
            Vector3 X = root.position;
            X.y = 0f;

            // Tip position
            Vector3 Y = tip.position;
            Y.y = 0f;

            // So long as the animation isn't playing we can create new goals
            if (!playing){
                // Distance from X to Y
                float CurrentDistance = Vector3.Distance(X, Y);

                // Dot
                float dot = Unitilities.Maths.GetDotProduct(X, Y, direction);

                // Recalculate goal
                    // Moved away from goal after getting into range of it
                    // Goal hasn't been set yet (Will cause "bouncing" behaviour of foot)
                    // Body moved too far away from goal (need improvement)
                if (goal == Vector3.zero || CurrentDistance > child.back){
                    if (Mathf.Ceil(dot) >= Mathf.Ceil(direction.normalized.magnitude)){
                        // Record current goal position
                        lastPos = new Vector3(
                            goal.x,
                            goal.y,
                            goal.z
                        );

                        // Reset animation timer
                        at = Vector2.zero;
                    }
                }
            } else {
                // increment animation timer
                at += new Vector2(
                    (Time.deltaTime / child.duration.x), //* (velocity.magnitude / 2f),
                    (Time.deltaTime / child.duration.y) //* (velocity.magnitude / 2f)
                );

                // Assign a new goal position
                goal = X + (direction * child.forward);
            }

            // Curve in wanted direction towards goal (adding the raycast to it as well)
            Vector3 curve = Unitilities.Maths.GetCurve(
                lastPos, 
                goal, 
                constraint.transform.up * child.height,
                at
            ) + CalculateRaycastHeight();

            return curve;
        }

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(){
            // Target
            constraint.data.target.transform.position = CalculateAnimation();

            // Hint
            constraint.data.hint.transform.position = constraint.data.target.transform.position + (constraint.transform.forward * 10f);

            Debug.DrawLine(constraint.data.target.transform.position, Vector3.zero, Color.red, 0.01f);
        }
    }
}
