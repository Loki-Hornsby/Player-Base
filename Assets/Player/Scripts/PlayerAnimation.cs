/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
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

    void SetState(PlayerAnimation.States _state){
        if (state != _state){
            switch (state){
                case PlayerAnimation.States.Idle:
                    // 
                    break;
                case PlayerAnimation.States.Jumping:
                    // 
                    break;
                case PlayerAnimation.States.Moving:
                    // 
                    break;
                case PlayerAnimation.States.Sprinting:
                    // 
                    break;
                case PlayerAnimation.States.Crouching:
                    // 
                    break;
            }

            state = _state;
        }
    }

    public void Jump(){
        SetState(PlayerAnimation.States.Jumping);
    }

    public void Idle(){
        SetState(PlayerAnimation.States.Idle);
    }

    public void Move(){
        SetState(PlayerAnimation.States.Moving);
    }
}
