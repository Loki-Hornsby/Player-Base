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
/// Coordinates jobs for the AI
/// </summary>

namespace AI {
    [RequireComponent(typeof(AIMemories))]
    public class AIBrain : MonoBehaviour {
        public enum Jobs {
            IveJustBeenSpawnedIntoAWorldHowInTheHellDidIGetHere // Startup 
        }

        [Header("References")]
        public AIControls controls;
        public AIHead head;

        [Header("Job")]
        public AIBrain.Jobs Job;
        AIBrain.Jobs lastJob;

        /// <summary>
        /// Begin
        /// </summary>
        public IEnumerator Setup(){
            Job = AIBrain.Jobs.IveJustBeenSpawnedIntoAWorldHowInTheHellDidIGetHere;

            yield return new WaitForSeconds(10); // AI information and thoughts are generated in this time
        }

        /// <summary>
        /// Setup 
        /// </summary>
        public void Start(){
            // Setup
            StartCoroutine(Setup());
        }

        /// <summary>
        /// Process thoughts
        /// </summary>
        public void Proc(AIBrain.Jobs last, AIBrain.Jobs current){
            switch (current){
                case AIBrain.Jobs.IveJustBeenSpawnedIntoAWorldHowInTheHellDidIGetHere:
                    controls.Job = AIControls.Jobs.Wander;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Update Thoughts
        /// </summary>
        public void Update(){
            if (lastJob != Job){
                Proc(lastJob, Job);
                lastJob = Job;
            }
        }
    }
}