using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the controls
/// </summary>

public static class Controls {
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
    }
}
