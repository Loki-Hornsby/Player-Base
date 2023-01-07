/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;

/// <summary>
/// This script handles coordination of P_Scripts (Player Scripts)
/// </summary>

namespace Player {  
    [RequireComponent(typeof(P_Movement))]
    [RequireComponent(typeof(P_Look))]
    public class P_Controller : MonoBehaviour {
        [Header("Movement")]
        public P_Movement movement;
        public float MovementSpeed;
        
        [Header("Look")]
        public P_Look look;
        public float LookSpeed;
        Vector2 rotation;

        [Header("AI (Optional)")]
        public AIControls? AI;
        
        void Start(){
            // Lock Mouse
            P_ControlsLib.Mouse.LockMouse();
        }

        void Update(){
            // Get axis
            Vector3 axis = P_ControlsLib.Movement.GetAxis(AI);

            // Calculate direction of travel
            Vector3 direction = P_ControlsLib.Movement.CalculateDirection(axis, this.transform);

            // Calculate speed
            float speed = P_ControlsLib.Movement.CalculateSpeed(AI, MovementSpeed);

            // Calculate wanted velocity
            Vector3 velocity = P_ControlsLib.Movement.CalculateVelocity(direction, speed, Time.deltaTime);  

            // Send variables to movement script
            movement.Send(
                velocity,
                direction
            );

            // Update look to look script
            //Vector2 mouse = controls.GetMousePosition(false, Time.deltaTime);
            //rotation.y += mouse.x * Time.deltaTime; 
            //rotation.x += -mouse.y * Time.deltaTime;
            //rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);

            /*Vector3 rot = new Vector3(
                0f,
                rotation.x,
                0f
            );

            look.Send( 
                Time.deltaTime,
                rot
            );*/
        }
    }
}