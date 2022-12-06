using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour {
    public float footSpacing;

    void Update(){
        Ray ray = new Ray(transform.position + (transform.right * footSpacing), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit info, 10)){
            transform.position = info.point;
        }
    }
}
