/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class P_Movement : MonoBehaviour {
    [Serializable]
    public class Walk {
        [Header("Configuration")]
        public bool useDefaults;

        [Header("General")]
        public float speed;
        public float gravity;
        public float jump;

        public void Setup(){
            if (useDefaults){
                Debug.Log("Setup");
                speed = 1.5f;
                gravity = -9.81f;
                jump = 5f;
            }
        }
    }

    [Serializable]
    public class Run {
        [Header("Configuration")]
        public bool enabled;
        public bool useDefaults;

        [Header("General")]
        public float speed;
        public float gravity;
        public float jump;

        public void Setup(){
            if (useDefaults){
                speed = 1.5f;
                gravity = -9.81f;
                jump = 0f;
            }
        }
    }

    [Serializable]
    public class Crouch {
        [Header("Configuration")]
        public bool enabled;
        public bool useDefaults;

        [Header("General")]
        public float speed;
        public float gravity;
        public float jump;

        public void Setup(){
            if (useDefaults){
                speed = 1.5f;
                gravity = -9.81f;
                jump = 0f;
            }
        }
    }

    [Serializable]
    public class Crawl {
        [Header("Configuration")]
        public bool enabled;
        public bool useDefaults;

        [Header("General")]
        public float speed;
        public float gravity;
        public float jump;

        public void Setup(){
            if (useDefaults){
                speed = 1.5f;
                gravity = -9.81f;
                jump = 0f;
            }
        }
    }

    // References
    [Header("References")]
    CharacterController cont;
    Animation anim;
    
    // Properties
    [Header("Movement Types")]
    public Walk walk;
    public Run run;
    public Crouch crouch;
    public Crawl crawl;
    
    // Velocity
    Vector3 vel;

    void Start(){
        // Setup
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
        
        P_Controls.Mouse.LockMouse();

        // Vars
        vel = Vector3.zero;
    }

    /// <summary>
    /// Applies physics to the player in terms of movement 
    /// </summary>
    void Update(){
        /*
        // Apply
        cont.Move(vel * Time.deltaTime);

        // Gravity
        // it's crucial this is set afterwards since downwards motion needs to be applied to check wether the player is grounded or not
        if (cont.isGrounded){
            // Apply offset to ensure cont.isGrounded works correctly
            vel.y = -cont.stepOffset / Time.deltaTime;

            // Movement
            Vector2 move = P_Controls.Movement.GetAxis(walk.speed); 

            if (!(move.x == 0f && move.y == 0f)){
                vel = new Vector3(
                    (Body.transform.forward.x * move.y) + (Body.transform.right.x * move.x),
                    vel.y,
                    (Body.transform.forward.z * move.y) + (Body.transform.right.z * move.x)
                );
            } else { // Coming to a idle
                vel = new Vector3(
                    0f,
                    0f,
                    0f
                );
            }

            // Jump
            if (P_Controls.Movement.GetJump()){
                Debug.Log("Jump!");
                vel.y += walk.jump;
            }
        }*/
    }
}
