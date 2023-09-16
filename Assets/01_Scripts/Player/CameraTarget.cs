using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Vector3 offset;
    [SerializeField] private Transform target;
    
    private void LateUpdate()
    {
        gameObject.transform.position = target.position + offset;
    }
}
