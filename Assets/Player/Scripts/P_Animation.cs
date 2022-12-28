/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD:Assets/Player/Scripts/P_Animation.cs
public class P_Animation : MonoBehaviour {
=======
public class PlayerAnimation : MonoBehaviour {
>>>>>>> main:Assets/Player/Scripts/PlayerAnimation.cs
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

<<<<<<< HEAD:Assets/Player/Scripts/P_Animation.cs
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
=======
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
>>>>>>> main:Assets/Player/Scripts/PlayerAnimation.cs
                    // 
                    break;
            }

            state = _state;
        }
    }

    public void Jump(){
<<<<<<< HEAD:Assets/Player/Scripts/P_Animation.cs
        SetState(P_Animation.States.Jumping);
    }

    public void Idle(){
        SetState(P_Animation.States.Idle);
    }

    public void Move(){
        SetState(P_Animation.States.Moving);
=======
        SetState(PlayerAnimation.States.Jumping);
    }

    public void Idle(){
        SetState(PlayerAnimation.States.Idle);
    }

    public void Move(){
        SetState(PlayerAnimation.States.Moving);
>>>>>>> main:Assets/Player/Scripts/PlayerAnimation.cs
    }
}
