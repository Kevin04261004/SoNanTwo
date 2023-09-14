using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Kevin
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnClick_J;
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                OnClick_J?.Invoke();
            }
        }
        
    }
}