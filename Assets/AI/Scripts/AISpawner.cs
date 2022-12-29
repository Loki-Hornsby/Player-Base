/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns AI in a grid and defines behaviors
/// </summary>

public class AISpawner : MonoBehaviour {
    [Header("Spawning")]
    public Vector2 Amount;
    public Vector2 Spacing;
    public GameObject AIBase;

    [Header("Behaviors")]
    [Header("Required")]
    //public Transform Home; // Optional

    [Header("Optional")]
    public Transform Home; // Optional

    public void Start(){
        // Spawn AI
        for (int x = 0; x < Amount.x; x++){
            for (int y = 0; y < Amount.y; y++){
                Instantiate(
                    AIBase, 
                    this.transform.position + new Vector3(
                        ((Amount.x * Spacing.x) / 2f) - ((x + 1) * Spacing.x), 
                        0f,
                        ((Amount.y * Spacing.y) / 2f) - ((y + 1) * Spacing.y)
                    ),
                    Quaternion.identity
                );
            }
        }
    }
}
