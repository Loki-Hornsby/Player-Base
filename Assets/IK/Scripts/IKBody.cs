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
/// </summary>

namespace IK {
    public class IKBody : MonoBehaviour {
        // Config
        public IKChild[] children;

        void Start(){
            foreach (var child in children)
                // typeof(IKJoints), typeof(IKFoot)
                child.list = IKPass.FindAndCreate(
                    child.Container.GetComponent<MonoBehaviour>(), 
                    child.GetFind(), 
                    child.GetCreate()
            );
        }

        public void IKUpdate(){
            foreach (var child in children){
                // Update the foots (yes i know its "feet" but naming consistency for my sake and all that)
                /*if (child.Container != null) IKPass.IKUpdate(
                    this, // Mono
                    feet, // Items
                    this.transform, child.P_Script.direction, Time.deltaTime // Variables
                );*/
            }
        }
    }
}
