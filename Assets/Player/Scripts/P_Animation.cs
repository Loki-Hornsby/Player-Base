/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Animation : MonoBehaviour {
    public enum States {
        Idle,
        Jumping,
        Falling,
        Moving,
        Sprinting,
        Crouching
    }

    public States state;
    Animator anim;

    void Start(){
        state = States.Idle;
        anim = GetComponent<Animator>();
    }

    void SetState(P_Animation.States _state){
        if (state != _state){
            switch (state){
                case P_Animation.States.Idle:
                    // 
                    break;
                case P_Animation.States.Jumping:
                    // 
                    break;
                case P_Animation.States.Moving:
                    // 
                    break;
                case P_Animation.States.Sprinting:
                    // 
                    break;
                case P_Animation.States.Crouching:
                    // 
                    break;
            }

            state = _state;
        }
    }

    public void Jump(){
        SetState(P_Animation.States.Jumping);
    }

    public void Idle(){
        SetState(P_Animation.States.Idle);
    }

    public void Move(){
        SetState(P_Animation.States.Moving);
    }
}
