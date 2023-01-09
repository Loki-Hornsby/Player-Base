using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

namespace IK {
    [Serializable]
    public class IKJoint {
        [System.NonSerialized] public TwoBoneIKConstraint constraint;

        bool setup;
        dynamic IK2;
        
        /// <summary>
        /// Triggered by IKPass
        /// </summary>
        public IKJoint(dynamic component, params dynamic[] parameters){
            setup = false;

            // Assign constraint
            constraint = (TwoBoneIKConstraint) component;

            // Type to create
            dynamic child = parameters[0];
            System.Type create = child.GetIK2_Script();

            // Create instance
            IK2 = IKHelpers.Create(ref create, this, parameters);

            // Use the MonoBehaviour from the constraint to setup our joint
            constraint.transform.GetComponent<MonoBehaviour>().StartCoroutine(Setup());
        }

        /// <summary>
        /// Setup this joint
        /// </summary>
        IEnumerator Setup(){
            // Position
            constraint.gameObject.transform.localPosition = new Vector3(0f, 10f, 0f);
            
            yield return new WaitForSeconds(2.5f); // Wait for IK to setup

            setup = true;
        }

        /// <summary>
        /// Update the IK2 this belongs to
        /// </summary>
        public void Update(){
            // Only update if joint is setup and IK2 isn't null
            if (setup) IK2.Update();
        }
    }
}