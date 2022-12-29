/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class P_Controller : MonoBehaviour {
        [Serializable]
        public class Walk {
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
        public class Run {
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
        public class Crouch {
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
        public class Crawl {
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

        [Header("References")]
        public P_Controls controls;
        public P_Movement movement;

        [Header("Configuration")]
        public Walk walk;
        public Run run;
        public Crouch crouch;
        public Crawl crawl;

        void Start(){
            // Setup
            walk.Setup();
            run.Setup(walk);
            crouch.Setup(walk);
            crawl.Setup(crouch);

            // Mouse
            controls.LockMouse();
        }

        void Update(){
            // Movement
            Vector2 move = controls.GetAxis(
                controls.GetRunning() ? 
                    run.speed 
                    : 
                    controls.GetCrouching() ?
                        crouch.speed
                        :
                        walk.speed
            ); 

            // Jumping
            bool jumping = controls.GetJumping();

            // Send movement to movement script
            movement.Send(move, jumping);
        }
    }
}