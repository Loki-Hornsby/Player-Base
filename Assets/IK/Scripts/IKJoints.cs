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
    public class IKJoints : MonoBehaviour {
        [System.NonSerialized] public bool setup;
        [System.NonSerialized] public IKJoint?[] list;

        void Start(){
            setup = false;

            // Generate the joints list
            list = (IKJoint?[]) IKPass.FindAndCreate(this, typeof(TwoBoneIKConstraint), typeof(IKJoint));

            // We only need to update the item once
            // We pass no values since there isn't a need for any!
            IKPass.IKUpdate(this, list);

            setup = true;
        }
    }
}
