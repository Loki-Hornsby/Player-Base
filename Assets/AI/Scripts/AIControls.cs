/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movement for the AI (Soon to be replaced by "Player Base")
/// </summary>

namespace AI {
    public class AIControls : MonoBehaviour{
        public enum Jobs {
            Wander,
            Visit,
            Escape,
            Idle
        }

        [Header("References")]
        public AIGut gut;

        [Header("Job")]
        public AIControls.Jobs Job;

        // Controls
        [System.NonSerialized] public Vector2 axis;
        [System.NonSerialized] public bool isJumping;
        [System.NonSerialized] public bool isCrouching;
        [System.NonSerialized] public bool isRunning;

        void Start(){
            axis = new Vector2(0f, 1f);
        }
    }
}