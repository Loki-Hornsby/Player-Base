/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Passes the controls to the correct scripts
/// </summary>
namespace Player {  
    [RequireComponent(typeof(P_Controls))]
    public class P_Controller : MonoBehaviour {
        public P_Movement movement;
        public P_Look look;
        //public P_Weapons weapons;
        //public P_Melee melee;

        P_Controls controls;
        
        void Start(){
            controls = GetComponent<P_Controls>();
        }

        void Update(){
            // Send movement to movement script
            movement.Send(
                Time.deltaTime,
                controls.GetVelocity(), 
                controls.GetJumping(), 
                controls.GetCrouching(), 
                controls.GetRunning()
            );

            // Send look to look script
            look.Send( 
                Time.deltaTime,
                Vector3.zero//controls.Mouse.GetLook()
            );
        }
    }
}