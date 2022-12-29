/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the controls
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
    public class P_Controls : MonoBehaviour {
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
        // \\ ================================================================================== // \\

        // // ==================================== Movement ==================================== \\ \\
        public Vector2 GetAxis(float mult = 1f){
            return new Vector2(Input.GetAxis("Horizontal") * mult, Input.GetAxis("Vertical") * mult);
        }

        public bool GetJumping(){
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool GetCrouching(){
            return Input.GetKey(KeyCode.LeftControl);
        }

        public bool GetRunning(){
            return Input.GetKey(KeyCode.LeftShift);
        }
        // \\ ================================================================================== // \\
    }
}