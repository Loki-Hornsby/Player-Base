/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;

/// <summary>
/// P_Controller.cs <-- P_*.cs <-- IKHead.cs <-- IKNeck.cs <-- * IKJoints.cs *
/// </summary>

namespace IK {
    public class IKJoints : MonoBehaviour {
        [Serializable]
        public class IKJoint {
            [System.NonSerialized] public bool setup;

            [Header("References")]
            [SerializeField] private TwoBoneIKConstraint reference; 
            [SerializeField] private Transform target;
            [SerializeField] private Transform hint;

            /// <summary>
            /// Setup this joint
            /// </summary>
            public IEnumerator Setup(){
                setup = false;

                // Position
                reference.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);

                yield return new WaitForSeconds(2.5f); // Wait for IK to setup

                setup = true;
            }

            /// <summary>
            /// Modify this joint
            /// </summary>
            public void Modify(Vector3 _target, Vector3 _hint){
                // Target
                target.transform.position = _target;

                // Hint
                hint.position = _hint;
            }

            /// <summary>
            /// Get the referenced target's position
            /// </summary>
            public Vector3 GetTargetPosition(){
                return target.transform.position;
            }

            /// <summary>
            /// Get the referenced hint's position
            /// </summary>
            public Vector3 GetHintPosition(){
                return hint.transform.position;
            }

            /// <summary>
            /// Get the root transform from the Two Bone IK Constraint
            /// </summary>
            public Transform GetRootTransform(){
                return reference.data.root.gameObject.transform;
            }

            /// <summary>
            /// Get the mid transform from the Two Bone IK Constraint
            /// </summary>
            public Transform GetMidTransform(){
                return reference.data.mid.gameObject.transform;
            }

            /// <summary>
            /// Get the tip transform from the Two Bone IK Constraint
            /// </summary>
            public Transform GetTipTransform(){
                return reference.data.tip.gameObject.transform;
            }

            /// <summary>
            /// Get the reference transform
            /// </summary>
            public Transform GetReferenceTransform(){
                return reference.transform;
            }
        }

        [System.NonSerialized] public bool setup;
        [SerializeField] private List<IKJoints.IKJoint> list;

        /// <summary>
        /// Setup Joint
        /// </summary>
        void Start(){
            setup = false;

            // Setup Joints
            foreach (var joint in list){
                StartCoroutine(joint.Setup());
            }

            setup = true;
        }

        /// <summary>
        /// Get the amount of joints
        /// </summary>
        public int GetJoints(){
            return list.Count;
        }

        private IKJoints.IKJoint GetJoint(int i){
            return list[i];
        }

        /// <summary>
        /// Modify a joint given index
        /// </summary>
        public void Modify(int i, Vector3 target, Vector3 hint){
            IKJoints.IKJoint joint = GetJoint(i);

            if (joint.setup && joint.setup) {
                joint.Modify(target, hint);
            }
        }

        /// <summary>
        /// Get target position given index
        /// </summary>
        public Vector3 GetTargetPosition(int i){
            return GetJoint(i).GetTargetPosition();
        }

        /// <summary>
        /// Get hint position given index
        /// </summary>
        public Vector3 GetHintPosition(int i){
            return GetJoint(i).GetHintPosition();
        }

        /// <summary>
        /// Get the root transform given index
        /// </summary>
        public Transform GetRootTransform(int i){
            return GetJoint(i).GetRootTransform();
        }

        /// <summary>
        /// Get the mid transform given index
        /// </summary>
        public Transform GetMidTransform(int i){
            return GetJoint(i).GetMidTransform();
        }

        /// <summary>
        /// Get the tip transform given index
        /// </summary>
        public Transform GetTipTransform(int i){
            return GetJoint(i).GetTipTransform();
        }

        /// <summary>
        /// Get the reference transform given index
        /// </summary>
        public Transform GetReferenceTransform(int i){
            return GetJoint(i).GetReferenceTransform();
        }
    }
}
