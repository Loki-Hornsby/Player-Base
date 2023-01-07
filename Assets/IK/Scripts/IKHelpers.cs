using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IK {
    public static class IKHelpers {
        /// <summary>
        /// Find a type
        /// </summary>
        public static dynamic[] Find(MonoBehaviour mono, System.Type find){
            return mono.GetComponentsInChildren(find) as dynamic[];
        }

        /// <summary>
        /// Create an instance of a type dynamically and pass arguments to it
        /// </summary>
        public static dynamic Create(ref System.Type create, params dynamic[] parameters){
            return Activator.CreateInstance(create, parameters) as dynamic; 
        }
    }
}
