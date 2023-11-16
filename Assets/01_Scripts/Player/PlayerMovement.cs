using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    // private NavMeshAgent _agent;
    // private Animator _animator;
    // private static readonly int MotionSpeed = Animator.StringToHash("MotionSpeed");
    // private static readonly int Speed = Animator.StringToHash("Speed");
    //
    // private void Awake()
    // {
    //     _agent = GetComponent<NavMeshAgent>();
    //     _animator = GetComponent<Animator>();
    // }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //
    //         if (Physics.Raycast(ray, out RaycastHit hit))
    //         {
    //             _agent.SetDestination(hit.point);
    //             _animator.SetFloat(Speed, 2.0f);
    //             _animator.SetFloat(MotionSpeed, 2.0f);
    //         }
    //         else if (_agent.remainingDistance < 0.1f)
    //         {
    //             _animator.SetFloat(Speed, 0f);
    //             _animator.SetFloat(MotionSpeed, 0f);
    //         }
    //     }
    //     
    // }
}
