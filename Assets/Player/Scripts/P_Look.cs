/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IK;

/// <summary>
/// P_Controls.cs <-- P_Controller.cs <-- * P_Look.cs * --> IKHead.cs --> IKNeck.cs --> IKJoints.cs
/// </summary> 

namespace Player {
    public class P_Look : MonoBehaviour {
        [Header("References")]
        public IKHead head;

        /// <summary>
        /// Applies rotation to both the body of the player and the head
        /// </summary>
        public void Send(float t, Vector3 rotation){
            head.Send(head, rotation);
        }
    }
}