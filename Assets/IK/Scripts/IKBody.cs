/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Animations.Rigging;
using Player;

/// <summary>
/// This is the <root> body of the IK system
/// It handles <fetching> and <updating> of my IK scripts using <IKPass.cs>
/// I am convinced this is the incorrect way to do all this but hay i'm testing - even if its wrong it helps me learn!
/// </summary>

/// <summary>
/// Order of operation
/// <IKBody.cs> -(Creates)-> <IKChild.cs> -(Creates)-> <IK2_*.cs> <and> <IKJoint.cs>
/// </summary>

namespace IK {
    public class IKBody : MonoBehaviour {
        // Config
        public IKChild[] children;
        
        public void Start(){
            // Update every child of children once (Setup each child)
            foreach (var child in children){
                child.Setup();
            }
        }

        public void Update(){
            foreach(var child in children){
                child.Update();
            }
        }
    }
}
