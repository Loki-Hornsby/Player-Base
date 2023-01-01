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
/// P_Controller.cs <-- P_Look.cs <-- * IKHead.cs * --> IKNeck.cs --> IKJoints.cs
/// </summary>

namespace IK {
    [RequireComponent(typeof(Rig))]
    public class IKHead : MonoBehaviour {
        [Header("List o' Necks")]
        public List<IKNeck> Necks;

        public void Send(IKHead head, Vector3 rotation){
            for (int i = 0; i < Necks.Count; i++){
                Necks[i].Update(head, rotation);
            }
        }
    }
}