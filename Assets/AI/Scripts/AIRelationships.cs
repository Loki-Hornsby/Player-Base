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
/// Relationships for the AI
/// </summary>

namespace AI {
    public class AIRelationships : MonoBehaviour {
        [Serializable]
        public class Item {
            public int to;
            public AIRelationships.PossibleRelations relation;

            /// <summary>
            /// Add the data
            /// </summary>
            public void addData(int _to, AIRelationships.PossibleRelations _relation){
                to = _to;
                relation = _relation;
            }

            /// <summary>
            /// Constructor
            /// </summary>
            public Item(int _to, AIRelationships.PossibleRelations _relation){
                addData(
                    _to, 
                    _relation
                );
            }

            /// <summary>
            /// Update current relation
            /// </summary>
            public void Update(AIRelationships.Item r){
                addData(
                    r.to, 
                    r.relation
                );
            }
        }

        // Relationship types
        public enum PossibleRelations {
            ScaredOf,
            Hates,
            Avoids,
            Neutral,
            Likes,
            Loves,
        }

        [Header("References")]
        public AIGut gut;

        [Header("Items")]
        public List<AIRelationships.Item> items;

        /// <summary>
        /// Find a relationship by ID
        /// </summary>
        public AIRelationships.Item relationship(int _to){
            AIRelationships.Item rel = items.FirstOrDefault(s => s.to == _to);

            return rel;
        }

        /// <summary>
        /// Add relation
        /// </summary>
        public void Add(int _to, AIRelationships.PossibleRelations _relation){
            AIRelationships.Item old_r = relationship(_to);
            AIRelationships.Item new_r = new AIRelationships.Item(_to, gut.relations.DefaultRelation);

            if (old_r != null){
                old_r.Update(new_r);
            } else {
                items.Add(new_r);
            }
        }
        
        /// <summary>
        /// Update Relations
        /// </summary>
        void Update(){
            
        }
    }
}