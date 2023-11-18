using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 dir = Vector3.forward;
    public void Update()
    {
        transform.Translate(dir * _speed * Time.deltaTime);
    }
}
