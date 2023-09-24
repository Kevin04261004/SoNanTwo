using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Kevin
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnClick_Q;

        public void OnQSkill()
        {
            OnClick_Q?.Invoke();
        }
    }
}