/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour {
    // References
    [Header("References")]
    public GameObject head;
    public Animation anim;
    CharacterController cont;
    
    // Properties
    [Header("Properties")]
    public float speed = 1.5f;
    public float sensitivity = 0.025f;
    public const float gravity = -9.81f;
    public float jump = 5f;

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
		head.transform.eulerAngles = rotation * sensitivity;
        transform.eulerAngles = new Vector3(0f, rotation.y, 0f) * sensitivity;
    }

    void ApplyPhysics(){
        Debug.Log(cont.isGrounded);

        // Gravity
        vel.y += gravity * Time.deltaTime;

        // Apply
        cont.Move(vel * Time.deltaTime);

        // Gravity
        // it's crucial this is set afterwards since downwards motion needs to be applied to check wether the player is grounded or not
        if (cont.isGrounded){
            // Reset gravity
            vel.y = 0f;

            // Movement
            Vector2 move = Controls.Movement.GetAxis(speed); 

            if (!(move.x == 0f && move.y == 0f)){
                anim.Move();

                vel = new Vector3(
                    (this.transform.forward.x * move.y) + (this.transform.right.x * move.x),
                    vel.y,
                    (this.transform.forward.z * move.y) + (this.transform.right.z * move.x)
                );
            } else { // Coming to a idle
                anim.Idle();

                vel = new Vector3(
                    0f,
                    0f,
                    0f
                );
            }

            // Jump
            if (Controls.Movement.GetJump()){
                anim.Jump();

                vel.y += jump;
            }
        }
    }

    void Update(){
        ApplyRotation();
        ApplyPhysics();
    }
}
