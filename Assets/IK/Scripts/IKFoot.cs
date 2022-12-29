/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

namespace IK {
    [Serializable]
    public class IKFoot {
        bool setup;

        [Header("Joints")]
        public List<IKJoint> Joints;

        /// <summary>
        /// Check if foot is setup
        /// </summary>
        public bool isSetup(){
            return setup;
        }

        /// <summary>
        /// Setup Joints and Feet
        /// </summary>
        public IEnumerator Setup(){
            Debug.Log("Setting up IK handles...");

            setup = false;

            foreach (var joint in Joints){
                // Setup Joint
                joint.Setup();

                yield return new WaitForSeconds(2.5f); // Wait for IK to setup

                // Complete Setup
                joint.Complete();
            }

            setup = true;

            Debug.Log("Finished setting up IK handles!");
        }

        /// <summary>
        /// Update Joints and Feet
        /// </summary>
        public void Update(IKFeet feet, int index, float t){
            if (setup){
                foreach (var joint in Joints){
                    if (joint.isSetup()){
                        joint.Update(feet, index, t);
                    }
                }
            }
        }
    }
}
