/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream:Assets/Player/Scripts/Animation.cs
[RequireComponent(typeof(Animator))]
public class Animation : MonoBehaviour {
=======
public class P_Animation : MonoBehaviour {
>>>>>>> Stashed changes:Assets/Player/Scripts/P_Animation.cs
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

<<<<<<< Updated upstream:Assets/Player/Scripts/Animation.cs
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
=======
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
>>>>>>> Stashed changes:Assets/Player/Scripts/P_Animation.cs
                    // 
                    break;
            }

            state = _state;
        }
    }

    public void Jump(){
<<<<<<< Updated upstream:Assets/Player/Scripts/Animation.cs
        SetState(Animation.States.Jumping);
    }

    public void Idle(){
        SetState(Animation.States.Idle);
    }

    public void Move(){
        SetState(Animation.States.Moving);
    }

    public void Crouch(bool crouch){
        if (crouch != anim.GetBool("Crouch")){
            anim.SetBool("Crouch", crouch);
        }
    }

    void Update(){
        Crouch(Controls.Movement.GetCrouching());
=======
        SetState(P_Animation.States.Jumping);
    }

    public void Idle(){
        SetState(P_Animation.States.Idle);
    }

    public void Move(){
        SetState(P_Animation.States.Moving);
>>>>>>> Stashed changes:Assets/Player/Scripts/P_Animation.cs
    }
}
