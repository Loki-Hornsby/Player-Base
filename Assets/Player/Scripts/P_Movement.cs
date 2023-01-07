/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IK;

/// <summary>
/// This script handles movement of the player
/// </summary>

namespace Player {
    [RequireComponent(typeof(CharacterController))]
    public class P_Movement : MonoBehaviour {
        // Components
        CharacterController cont;

        // Physics
        [System.NonSerialized] public Vector3 direction;
        [System.NonSerialized] public Vector3 velocity;

        void Start(){
            // References
            cont = GetComponent<CharacterController>();
        }

        /// <summary>
        /// Applies physics to the player in terms of movement 
        /// </summary>
        public void Send(Vector3 _velocity, Vector3 _direction){
            // Assign
            velocity = _velocity;
            direction = _direction;

            // Apply
            cont.Move(velocity);

            /*
            // Gravity
            // it's crucial this is set afterwards since downwards motion needs to be applied to check wether the player is grounded or not
            if (cont.isGrounded){
                // Apply offset to ensure cont.isGrounded works correctly
                velocity.y = -cont.stepOffset / Time.deltaTime;

                // Movement
                Vector2 move = P_Controls.Movement.GetAxis(walk.speed); 

                if (!(move.x == 0f && move.y == 0f)){
                    velocity = new Vector3(
                        (Body.transform.forward.x * move.y) + (Body.transform.right.x * move.x),
                        velocity.y,
                        (Body.transform.forward.z * move.y) + (Body.transform.right.z * move.x)
                    );
                } else { // Coming to a idle
                    velocity = new Vector3(
                        0f,
                        0f,
                        0f
                    );
                }

                // Jump
                if (jump){
                    Debug.Log("Jump!");
                    velocity.y += walk.jump;
                }
            }
            */
        }
    }
}