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

<<<<<<< HEAD:Assets/Player/Scripts/P_Controls.cs
public static class P_Controls {
=======
public static class PlayerControls {
>>>>>>> main:Assets/Player/Scripts/PlayerControls.cs
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

    public static class Movement {
        public static Vector2 GetAxis(float mult = 1f){
            return new Vector2(Input.GetAxis("Horizontal") * mult, Input.GetAxis("Vertical") * mult);
        }

        public static bool GetJump(){
            return Input.GetKeyDown(KeyCode.Space);
        }

        public static bool GetCrouching(){
            return Input.GetKey(KeyCode.LeftControl);
        }
    }
}
