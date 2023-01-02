/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// P_Controls.cs <-- * P_Controller.cs * --> P_*.cs --> IK*.cs --> IK*.cs --> IKJoints.cs
/// </summary>

namespace Player {  
    [RequireComponent(typeof(P_Movement))]
    [RequireComponent(typeof(P_Look))]
    [RequireComponent(typeof(P_Controls))]
    public class P_Controller : MonoBehaviour {
        // Movement
        P_Movement movement;
        public float speed;

        // Look 
        P_Look look;
        Vector2 rotation;

        // Controls
        P_Controls controls;
        
        void Start(){
            movement = GetComponent<P_Movement>();
            look = GetComponent<P_Look>();
            controls = GetComponent<P_Controls>();
        }

        void Update(){
            // Update movement to movement script
            movement.Send(
                controls.GetVelocity(this.transform, speed, Time.deltaTime)
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