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
/// The gut defines the AI's varying behaviour
/// </summary>

namespace AI {
    public class AIGut : MonoBehaviour {
        [Serializable]
        public class General {
            public float age;

            public void Determine(){

            }
        }

        [Serializable]
        public class Reflexes {
            // Look --> React --> Action
            public float LookTime;
            public float ReactTime;
            public float ActionTime;

            public void Determine(){
                
            }
        }

        [Serializable]
        public class Relations {
            public AIRelationships reference;

            public AIRelationships.PossibleRelations DefaultRelation;

            public void Determine(){
                
            }
        }

        [Serializable]
        public class Memories {
            public AIMemories reference;

            //public AIMemories.PossibleMemories DefaultMemory; ~ Long or short term

            public void Determine(){
                
            }
        }

        [Serializable]
        public class View {
            public AIView reference;
            
            public float CloseBorder;

            public void Determine(){
                
            }
        }

        [Serializable]
        public class Movement {
            public AIControls reference;

            public float speed;
            public float sensitivity;
            public float gravity;
            public float jump;

            public void Determine(){
                speed = 1.5f;
                sensitivity = 0.025f;
                gravity = -9.81f;
                jump = 5f;
            }
        }

        public bool IsDetermined;
        public General general;
        public Reflexes reflexes;
        public Relations relations;
        public Memories memories;
        public View view;
        public Movement movement;

        void Start(){
            if (IsDetermined){
                general.Determine();
                reflexes.Determine();
                relations.Determine();
                memories.Determine();
                movement.Determine();
            }
        }
    }
}