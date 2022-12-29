/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player {
    public class P_Look : MonoBehaviour {
        public GameObject Head;

        /// <summary>
        /// Applies rotation to both the body of the player and the head
        /// </summary>
        public void Send(float t, Vector3 target){
            Vector3 relativePos = target - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, t); //gut.reflexes.LookTime * Time.deltaTime);

            /*
            // Get Mouse Pos
            Vector2 mPos = PlayerControls.Mouse.GetMousePosition(false, Time.deltaTime);

            // Left, Right
            rotation.y += mPos.x; 
            
            // Up, Down
            rotation.x += -mPos.y;
            rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);

            // Apply
            Head.transform.eulerAngles = original * look.sensitivity;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x, 
                rotation.y, 
                transform.eulerAngles.z
            ) * look.sensitivity;
            */
        }
    }
}