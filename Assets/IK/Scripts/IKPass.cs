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
/// Dynamic fun using ".Net framework" (API compatibility level)!
/// I'm not sure if this is correct or not (i'm pretty sure it isn't) but i thought i'd experiment
/// </summary>

namespace IK {
    public static class IKPass {
        /// <summary>
        /// Create dynamic items from an array and send the command "IKUpdate" to them
        /// </summary>
        public static void IKUpdate(MonoBehaviour mono, dynamic items, params dynamic[] values){
            foreach (dynamic item in items){
                mono.StartCoroutine(item.IKUpdate(values));
            }
        }

        /// <summary>
        /// Find every <F> 
        /// in children of a <MonoBehaviour>
        /// then create a list of <C> using the amount of <F> found
        /// pass <F> into each newly created <C> as a dynamic parameter
        /// </summary>
        public static dynamic?[] FindAndCreate(MonoBehaviour mono, System.Type find, System.Type create) {
            try {
                var comps = mono.GetComponentsInChildren(find.GetType());
                var list = new dynamic?[comps.Length];

                // https://stackoverflow.com/questions/6410340/generics-in-c-sharp-how-can-i-create-an-instance-of-a-variable-type-with-an-ar
                for (int i = 0; i < comps.Length; i++){
             
                    // basically this is just `list[i] = new C?(options);`
                    var options = new object[] { comps[i] };
                    var instance = Activator.CreateInstance(create.GetType(), options); 
                    list[i] = instance;
                }

                return list;
            } catch {
                return null;
            }
        }
    }
}