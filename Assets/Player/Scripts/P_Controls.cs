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
/// Handles a players controls
///
///    GetKeyDown
///    only runs once on the first frame that the key was pressed.
///
///    GetKeyUp
///    only runs once on the frame that the key was released.
///
///    GetKey
///    runs continuously to check if a key is currently pressed down or not.
///
/// </summary>

/// <summary>
/// * P_Controls.cs * --> P_Controller.cs --> P_*.cs --> IK*.cs --> IK*.cs --> IKJoints.cs
/// </summary> 

namespace Player {
    public class P_Controls : MonoBehaviour {
        /*
        [Serializable]
        public struct Walk {
            [Header("Configuration")]
            public bool enabled; // TODO
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
        public struct Run {
            [Header("Configuration")]
            public bool enabled; // TODO
            public bool useDefaults;

            [Header("General")]
            public float speed;
            public float gravity;
            public float jump;

            public void Setup(Walk walk){
                if (useDefaults){
                    speed = walk.speed * 2f;
                    gravity = -9.81f;
                    jump = 0f;
                }
            }
        }

        [Serializable]
        public struct Crouch {
            [Header("Configuration")]
            public bool enabled; // TODO
            public bool useDefaults;

            [Header("General")]
            public float speed;
            public float gravity;
            public float jump;

            public void Setup(Walk walk){
                if (useDefaults){
                    speed = walk.speed * 0.5f;
                    gravity = -9.81f;
                    jump = 0f;
                }
            }
        }

        [Serializable]
        public struct Crawl {
            [Header("Configuration")]
            public bool enabled; // TODO
            public bool useDefaults;

            [Header("General")]
            public float speed;
            public float gravity;
            public float jump;

            public void Setup(Crouch crouch){
                if (useDefaults){
                    speed = crouch.speed * 0.5f;
                    gravity = -9.81f;
                    jump = 0f;
                }
            }
        }

        // // ==================================== General ==================================== \\ \\

        [Header("Configuration")]
        public Walk walk;
        public Run run;
        public Crouch crouch;
        public Crawl crawl;*/

        [Header("AI (Optional)")]
        public AIControls AI;
        bool isAI;
        
        void Start(){
            // Setup
            //walk.Setup();
            //run.Setup(walk);
            //crouch.Setup(walk);
            //crawl.Setup(crouch);

            // Mouse
            LockMouse();

            // AI
            isAI = (AI != null);
        }

        // // ==================================== Mouse ==================================== \\ \\

        int reset;

        bool ResetCheck(){
            if (reset > 0){
                reset--;

                return true;
            } else {
                return false;
            }
        }

        public void Reset(){
            reset = 1;
        }

        public void LockMouse(){
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void UnlockMouse(){
            Cursor.lockState = CursorLockMode.None;
        }

        public bool GetHeld(int type){
            if (ResetCheck()) return false;

            return Input.GetMouseButton(type);
        }

        public bool GetClicked(int type){
            if (ResetCheck()) return false;

            return Input.GetMouseButtonDown(type);
        }

        public bool GetUp(int type){
            if (ResetCheck()) return false;

            return Input.GetMouseButtonUp(type);
        }

        public Vector2 GetMousePosition(bool GetRealPosition, float time=1f){
            if (GetRealPosition){
                return new Vector2(Input.mousePosition.x * time, Input.mousePosition.y * time);
            } else {
                return new Vector2(Input.GetAxis("Mouse X") * time, Input.GetAxis("Mouse Y") * time);
            }
        }

        // // ==================================== Movement ==================================== \\ \\

        Vector2 GetAxis(float mult = 1f){
            if (isAI){
                return AI.axis * mult;
            } else {
                return new Vector2(Input.GetAxis("Horizontal") * mult, Input.GetAxis("Vertical") * mult);
            }
        }

        bool GetJumping(){
            if (isAI){
                return AI.isJumping; 
            } else {
                return Input.GetKeyDown(KeyCode.Space);
            }
        }

        bool GetCrouching(){
            if (isAI){
                return AI.isCrouching;
            } else {
                return Input.GetKey(KeyCode.LeftControl);
            }
        }

        bool GetRunning(){
            if (isAI){
                return AI.isRunning;
            } else {
                return Input.GetKey(KeyCode.LeftShift);
            }
        }

        public Vector3 GetVelocity(Transform transform, float speed, float t){
            Vector2 axis = GetAxis(
                GetRunning() ? 
                    speed * 2f 
                    : 
                    GetCrouching() ?
                        speed / 2f
                        :
                        speed
            ); 

            Vector3 velocity = (
                (axis.x * transform.forward) +
                (((GetJumping()) ? 1f : 0f) * transform.up) +
                axis.y * transform.right
            ) * t;

            return velocity;
        }
    }
}