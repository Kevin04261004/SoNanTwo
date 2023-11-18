using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 dir = Vector3.forward;
    [SerializeField] private int BounceTime;
    [SerializeField] private float attackPower = 20;
    private WaitForSeconds waitLivingTime = new WaitForSeconds(5);
    public GameObject _fromObject;

    protected virtual void Awake()
    {
        StartCoroutine(nameof(AliveRoutine));
    }

    protected virtual void Update()
    {
        transform.Translate(dir * (_speed * Time.deltaTime));
    }

    private IEnumerator AliveRoutine()
    {
        yield return waitLivingTime;
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        BounceTime--;
        if (BounceTime == 0)
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Player") && other.gameObject != _fromObject)
        {
            OnPlayerEnter(other);
            Destroy(gameObject);
        }
    }

    protected virtual void OnPlayerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnDamage(attackPower);
            print("!!!!!!!!!!!!!!");
        }
        else
        {
            Debug.Assert(true, "Add playerHealth component!!!");
        }
    }
}
