/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {
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

    [Serializable]
    public class Look {
        public float sensitivity = 0.025f;

        public void Setup(){

        }
    }

    // References
    [Header("References")]
    public GameObject Head;
    CharacterController cont;
    
    // Properties
    [Header("Configuration")]
    public Walk walk;
    public Run run;
    public Crouch crouch;
    public Crawl crawl;
    public Look look;

    // Variables
    Vector3 vel;
    Vector3 rotation;

    void Start(){
        // Setup
        cont = GetComponent<CharacterController>();
        
        Controls.Mouse.LockMouse();

        // Vars
        vel = Vector3.zero;
        rotation = Vector3.zero;
    }

    void ApplyRotation(){
        // Get Mouse Pos
        Vector2 mPos = Controls.Mouse.GetMousePosition(false, Time.deltaTime);

        // Left, Right
        rotation.y += mPos.x; 
        
        // Up, Down
		rotation.x += -mPos.y;
        rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);

        // Apply
		//Head.transform.eulerAngles = original * look.sensitivity;
        transform.eulerAngles = new Vector3(0f, rotation.y, 0f) * look.sensitivity;
    }

    void ApplyPhysics(){
        // Gravity
        vel.y += walk.gravity * Time.deltaTime;

        // Apply
        cont.Move(vel * Time.deltaTime);

        // Gravity
        // it's crucial this is set afterwards since downwards motion needs to be applied to check wether the player is grounded or not
        if (cont.isGrounded){
            // Reset gravity
            vel.y = 0f;

            // Movement
            Vector2 move = Controls.Movement.GetAxis(walk.speed); 

            if (!(move.x == 0f && move.y == 0f)){
                vel = new Vector3(
                    (transform.forward.x * move.y) + (transform.right.x * move.x),
                    vel.y,
                    (transform.forward.z * move.y) + (transform.right.z * move.x)
                );
            } else { // Coming to a idle
                vel = new Vector3(
                    0f,
                    0f,
                    0f
                );
            }

            // Jump
            if (Controls.Movement.GetJump()){
                vel.y += walk.jump;
            }
        }
    }

    void Update(){
        ApplyRotation();
        ApplyPhysics();
    }
}
