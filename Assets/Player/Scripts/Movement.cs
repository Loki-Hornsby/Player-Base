/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animation))]
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

    [Serializable]
    public class Look {
        public float sensitivity = 0.025f;

        public void Setup(){

        }
    }

    // References
    [Header("References")]
    public GameObject Head;
    public GameObject Body;
    CharacterController cont;
    Animation anim;
    
    // Properties
    [Header("Movement Types")]
    public Walk walk;
    public Run run;
    public Crouch crouch;
    public Crawl crawl;

    [Header("Look configuration")]
    public Look look;
    
    // Velocity
    Vector3 vel;

    // Rotation
    Vector3 original;

    void Start(){
        // Setup
        cont = GetComponent<CharacterController>();
        anim = GetComponent<Animation>();
        
        Controls.Mouse.LockMouse();

        // Variable Init
        walk.Setup();
        run.Setup();
        crouch.Setup();
        crawl.Setup();
        look.Setup();

        // Vars
        vel = Vector3.zero;
        original = Head.transform.eulerAngles;
    }

    //float GetSpeed(){
        //if (){

        //}
    //}

    void ApplyRotation(){
        // Get Mouse Pos
        Vector2 mPos = Controls.Mouse.GetMousePosition(false, Time.deltaTime);

        // Left, Right
        original.y += mPos.x; 
        
        // Up, Down
		original.x += -mPos.y;
        //rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);

        // Apply
		Head.transform.eulerAngles = original * look.sensitivity;
        Body.transform.eulerAngles = new Vector3(0f, original.y, 0f) * look.sensitivity;
    }

    void ApplyPhysics(){
        Debug.Log(cont.isGrounded);

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
