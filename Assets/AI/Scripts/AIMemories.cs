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
/// Memories for the AI
/// </summary>

namespace AI {
    [RequireComponent(typeof(AIRelationships))]
    public class AIMemories : MonoBehaviour {
        [Serializable]
        public class Item {
            // Identifiers
            public int id;
            public string name;

            // References
            public GameObject self;
            public GameObject other;

            // Relation
            public AIRelationships.PossibleRelations relation;

            /// <summary>
            /// Add the data
            /// </summary>
            public void addData(int _id, string _name, GameObject _self, GameObject _other){
                // Identifiers
                id = _id;
                name = _name;

                // References
                self = _self;
                other = _other;
            }

            /// <summary>
            /// Constructor
            /// </summary>
            public Item(int _id, string _name, GameObject _ai, GameObject _object){
                addData (
                    _id, 
                    _name, 
                    _ai, 
                    _object
                );
            }

            /// <summary>
            /// Update current memory
            /// </summary>
            public void Update(AIMemories.Item m){
                addData (
                    m.id,
                    m.name, 
                    m.self,
                    m.other
                );
            }
        }

        // Memory types
        public enum MemTypes {
            Enemy,  // The AI sees people as enemies under certain conditions
                        // you could change this so the enemy perceives people to be slightly less dangerous than enemies
            Object,
            Friendly,
        }

        [Header("References")]
        public AIGut gut;

        [Header("Items")]
        public List<AIMemories.Item> items;

        /// <summary>
        /// Find a memory by ID
        /// </summary>
        public AIMemories.Item Find(int _id){
            return items.FirstOrDefault(s => s.id == _id);
        }

        /// <summary>
        /// Add memory from view
        /// </summary>
        public void Add(AIView.Item x){
            AIMemories.Item old_m = Find(x.id);
            AIMemories.Item new_m = new AIMemories.Item (
                x.id,
                x.name,
                x.self,
                x.other
            );

            if (old_m != null){
                old_m.Update(new_m);
            } else {
                items.Add(new_m);
            }
        }

        /// <summary>
        /// Update Memories
        /// </summary>
        void Update(){
            
        }
    }
}