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

namespace Player {
    public static class P_ControlsLib {

        // // ==================================== Movement ==================================== \\ \\

        public static class Mouse {
            static int reset;

            static bool ResetCheck(){
                if (reset > 0){
                    reset--;

                    return true;
                } else {
                    return false;
                }
            }

            public static void Reset(){
                reset = 1;
            }

            public static void LockMouse(){
                Cursor.lockState = CursorLockMode.Locked;
            }

            public static void UnlockMouse(){
                Cursor.lockState = CursorLockMode.None;
            }

            public static bool GetHeld(int type){
                if (ResetCheck()) return false;

                return Input.GetMouseButton(type);
            }

            public static bool GetClicked(int type){
                if (ResetCheck()) return false;

                return Input.GetMouseButtonDown(type);
            }

            public static bool GetUp(int type){
                if (ResetCheck()) return false;

                return Input.GetMouseButtonUp(type);
            }

            public static Vector2 GetMousePosition(bool GetRealPosition, float time=1f){
                if (GetRealPosition){
                    return new Vector2(Input.mousePosition.x * time, Input.mousePosition.y * time);
                } else {
                    return new Vector2(Input.GetAxis("Mouse X") * time, Input.GetAxis("Mouse Y") * time);
                }
            }
        }

        // // ==================================== Movement ==================================== \\ \\

        public static class Movement {
            static bool GetJumping(AIControls? AI){
                if (AI != null){
                    return AI.isJumping; 
                } else {
                    return Input.GetKeyDown(KeyCode.Space);
                }
            }

            static bool GetCrouching(AIControls? AI){
                if (AI != null){
                    return AI.isCrouching;
                } else {
                    return Input.GetKey(KeyCode.LeftControl);
                }
            }

            static bool GetRunning(AIControls? AI){
                if (AI != null){
                    return AI.isRunning;
                } else {
                    return Input.GetKey(KeyCode.LeftShift);
                }
            }

            public static float CalculateSpeed(AIControls? AI, float baseSpeed){
                return GetRunning(AI) ? 
                        baseSpeed * 2f 
                        : 
                        GetCrouching(AI) ?
                            baseSpeed / 2f
                            :
                            baseSpeed;
            }

            public static Vector3 GetAxis(AIControls? AI){
                if (AI != null){
                    return AI.axis;
                } else {
                    return new Vector3(
                        Input.GetAxis("Horizontal"), 
                        GetJumping(AI) ? 1f : 0f,
                        Input.GetAxis("Vertical")
                    );
                }
            }

            public static Vector3 CalculateDirection(Vector3 axis, Transform transform){
                return (
                    axis.x * transform.forward +
                    axis.y * transform.up +
                    axis.z * transform.right
                );
            }

            public static Vector3 CalculateVelocity(Vector3 direction, float speed, float t){
                return direction * speed * t;
            }
        }
    }
}