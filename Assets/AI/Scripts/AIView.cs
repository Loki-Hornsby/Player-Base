/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

/// <summary>
/// View for the AI
/// </summary>

namespace AI {
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class AIView : MonoBehaviour {
        [Serializable]
        public class Item {
            public const float RayOffset = 10f;

            // Identifiers
            public int id;
            public string name;

            // References
            public GameObject self;
            public GameObject other;

            // Data
            public bool visible;
            public float visibleTime;
            public RaycastHit hit;

            /// <summary>
            /// Constructor
            /// </summary>
            public Item(ref GameObject _self, ref GameObject _other, int _id){
                // Identifiers
                id = _id;
                name = _other.name;

                // References
                self = _self;
                other = _other;
            }
            
            /// <summary>
            /// Update info
            /// </summary>
            public void Update(float t){
                // Visible     
                Physics.Linecast(
                    self.transform.position + self.transform.forward / RayOffset, 
                    other.transform.position,
                    out hit
                );

                // Bug: Linecast is hitting itself
                if (hit.collider != null){
                    GameObject hit_obj = hit.transform.gameObject;
                    GameObject other_obj = other.transform.gameObject;

                    visible = (hit_obj.GetInstanceID() == other_obj.GetInstanceID());
                }

                if (visible) visibleTime += t;

                Debug.DrawLine(
                    self.transform.position + self.transform.forward / RayOffset, 
                    other.transform.position,
                    (visible) ? Color.green : Color.red,
                    0.01f
                );
            }
        }

        [Header("References")]
        public AIGut gut;
        CapsuleCollider cc;
        Rigidbody rb;

        [Header("Items")]
        public List<AIView.Item> items;

        /// <summary>
        /// Initialize
        /// </summary>
        void Start(){
            // Collider
            cc = GetComponent<CapsuleCollider>();
            cc.isTrigger = true;

            // Rigidbody
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }

        /// <summary>
        /// Add/Remove object to/from view list 
        /// Add object to memory
        /// </summary>
        void trigger(Collider col, bool enter){
            // Ref
            GameObject self = this.gameObject;
            GameObject other = col.gameObject;

            int id = other.GetInstanceID();

            // Duplicate
            var q = items.FirstOrDefault(s => s.id == id); // Bug: Error here

            if (q == null){
                // Item
                AIView.Item item = new AIView.Item(
                    ref self, 
                    ref other,
                    id
                );

                // Add relationship to viewed object
                gut.relations.reference.Add(id, gut.relations.DefaultRelation);

                // Add to view
                items.Add(item);
            } else if (!enter) {
                // Add memory of viewed object
                gut.memories.reference.Add(q);

                // Remove from view
                items.Remove(q);
            }
        }

        /// <summary>
        /// Enter Trigger
        /// </summary>
        private void OnTriggerEnter(Collider other) {
            trigger(other, true);
        }

        /// <summary>
        /// Exit Trigger
        /// </summary>
        private void OnTriggerExit(Collider other) {
            trigger(other, false);
        }

        /// <summary>
        /// Update View
        /// </summary>
        void Update(){
            foreach (var item in items){
                item.Update(Time.deltaTime);
            }
        }
    }
}
