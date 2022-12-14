/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
    /*
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
    
    /// <summary>
    /// Return current jump height for the player dependent on their mode
    /// </summary>
    public float GetCurrentJumpHeight(){
        return walk.jump;
    }

    /// <summary>
    /// Return current speed for the player dependent on their mode
    /// </summary>
    public float GetCurrentSpeed(){
        return walk.speed;
    }

    /// <summary>
    /// Return current speed for the player dependent on their mode
    /// </summary>
    public float GetCurrentGravity(){
        return walk.gravity;
    }

    /// <summary>
    /// Initialize
    /// </summary>
    void Start(){
        // Setup
        cont = GetComponent<CharacterController>();
        
        PlayerControls.Mouse.LockMouse();

        // Vars
        vel = Vector3.zero;
        rotation = Vector3.zero;
    }

    /// <summary>
    /// Applies rotation to both the body of the player and the head
    /// </summary>
    void ApplyRotation(){
        // Get Mouse Pos
        Vector2 mPos = PlayerControls.Mouse.GetMousePosition(false, Time.deltaTime);

        // Left, Right
        rotation.y += mPos.x; 
        
        // Up, Down
		rotation.x += -mPos.y;
        rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);

        // Apply
		//Head.transform.eulerAngles = original * look.sensitivity;
        /*transform.eulerAngles = new Vector3(
            transform.eulerAngles.x, 
            rotation.y, 
            transform.eulerAngles.z
        ) * look.sensitivity;*/
    
    /*
    }

    /// <summary>
    /// Applies physics to the player in terms of movement 
    /// </summary>
    void ApplyPhysics(){
        // Apply
        cont.Move(vel * Time.deltaTime);

        // Gravity
        if (cont.isGrounded){
            // Apply offset to ensure cont.isGrounded works correctly
            vel.y = -cont.stepOffset / Time.deltaTime;;

            // Movement
            Vector2 move = PlayerControls.Movement.GetAxis(walk.speed); 

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
            if (PlayerControls.Movement.GetJump()){
                Debug.Log("Jump!");
                vel.y += walk.jump;
            }
        } else {
            // Apply gravity to ensure cont.isGrounded works correctly
            vel.y += walk.gravity * Time.deltaTime;
        }
    }

    /// <summary>
    /// Update rotation and physics
    /// </summary>
    void Update(){
        ApplyRotation();
        ApplyPhysics();
    }*/
}
