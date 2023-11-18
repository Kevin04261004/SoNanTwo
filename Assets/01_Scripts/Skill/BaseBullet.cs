using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 dir = Vector3.forward;
    [SerializeField] private int BounceTime;
    [SerializeField] private float attackPower = 20;
    private WaitForSeconds waitLivingTime = new WaitForSeconds(5);
    private int fromViewID = -1;
    public PlayerHealth playerHealth;
    private PhotonView PV;
    
    public void SetFromViewID(int i)
    {
        PV.RPC(nameof(SetFromViewIDRPC), RpcTarget.All, i);
    }
    [PunRPC]
    private void SetFromViewIDRPC(int i)
    {
        fromViewID = i;
    }
    protected virtual void Awake()
    {
        StartCoroutine(nameof(AliveRoutine));
        TryGetComponent(out PV);
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
        if (BounceTime <= 0)
        {
            Destroy(gameObject);
        }

        other.TryGetComponent(out PhotonView pv);
        if (other.CompareTag("Player") && fromViewID != pv.ViewID)// && fromViewID != -1)
        {
            OnPlayerEnter(other);
            Destroy(gameObject);
        }
    }

    protected virtual void OnPlayerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.OnDamage(attackPower);
        }
        else
        {
            Debug.Assert(true, "Add playerHealth component!!!");
        }
    }
}
