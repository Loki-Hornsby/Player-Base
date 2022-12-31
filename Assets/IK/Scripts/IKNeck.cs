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
/// P_Controller.cs <-- P_Look.cs <-- IKHead.cs <-- * IKNeck.cs * --> IKJoints.cs
/// </summary>

namespace IK {
    [Serializable]
    public class IKNeck {
        [Header("References")]
        public IKJoints joints;

        /// <summary>
        /// Update Joint
        /// </summary>
        public void Update(IKHead head, Vector3 rotation){
            for (int i = 0; i < joints.GetJoints(); i++){
                joints.GetReferenceTransform(i).eulerAngles = rotation;

                joints.Modify(i, 
                    head.transform.position + head.transform.forward, 
                    head.transform.position + head.transform.forward
                );
            }
        }
    }
}
