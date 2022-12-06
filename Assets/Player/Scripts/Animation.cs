/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {
    public enum States {
        Idle,
        Jumping,
        Falling,
        Moving,
        Sprinting,
        Crouching
    }

    public States state;

    void Start(){
        state = States.Idle;
    }

    void SetState(Animation.States _state){
        if (state != _state){
            switch (state){
                case Animation.States.Idle:
                    // 
                    break;
                case Animation.States.Jumping:
                    // 
                    break;
                case Animation.States.Moving:
                    // 
                    break;
                case Animation.States.Sprinting:
                    // 
                    break;
                case Animation.States.Crouching:
                    // 
                    break;
            }

            state = _state;
        }
    }

    public void Jump(){
        SetState(Animation.States.Jumping);
    }

    public void Idle(){
        SetState(Animation.States.Idle);
    }

    public void Move(){
        SetState(Animation.States.Moving);
    }
}
