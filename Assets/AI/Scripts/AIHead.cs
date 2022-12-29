/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

/// <summary>
/// Head Movement for the AI
/// </summary>

namespace AI {
    public class AIHead : MonoBehaviour {
        public enum Jobs {
            LookAt,
            Wander
        }

        [Header("References")]
        public AIGut gut;

        [Header("Job")]
        public AIHead.Jobs Job;

        void Update(){
            switch (Job){
                case AIHead.Jobs.LookAt:
                    /*Vector3 relativePos = Target.position - transform.position;
                    Quaternion toRotation = Quaternion.LookRotation(relativePos);
                    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, gut.reflexes.LookTime * Time.deltaTime);*/

                    break;

                case AIHead.Jobs.Wander:
                    // 
                    break;
            }

            // rotation.x = Mathf.Clamp(rotation.x, -180f * 1.5f, 180f * 1.5f);
        }
    }
}
