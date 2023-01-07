using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

/// <summary>
/// IKJoints.cs --> * IKJoint.cs *
/// </summary>

namespace IK {
    [Serializable]
    public class IKJoint {
        [System.NonSerialized] public bool setup;
        TwoBoneIKConstraint constraint;

        /// <summary>
        /// Store a reference to the constraint
        /// </summary>
        public IKJoint(ref TwoBoneIKConstraint _constraint){
            constraint = _constraint;
        }

        /// <summary>
        /// Setup this joint
        /// </summary>
        public IEnumerator IKUpdate(params dynamic[] values){
            setup = false;

            // Position
            constraint.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

            yield return new WaitForSeconds(2.5f); // Wait for IK to setup

            setup = true;
        }

        /// <summary>
        /// Get the constraint
        /// </summary>
        public TwoBoneIKConstraint GetConstraint(){
            return constraint;
        }
    }
}