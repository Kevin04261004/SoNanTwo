using System;
using Photon.Pun;
using UnityEngine;

public class LivingEntity : MonoBehaviourPun, IDamageable
{
    public float startingHealth = 100f;
    [field:SerializeField] public float health { get; protected set; }
    [field:SerializeField]  public bool dead { get; protected set; }
    public event Action onDeath;

    [PunRPC]
    public void ApplyUpdateHealth(float newHealth, bool newDead)
    {
        health = newHealth;
        dead = newDead;
    }

    protected virtual void OnEnable()
    {
        dead = false;
        health = startingHealth;
    }
    
    [PunRPC]
    public virtual void OnDamage(float damage)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            health -= damage;

            //호스트에서 클라이언트로 동기화
            photonView.RPC("ApplyUpdateHealth", RpcTarget.Others, health, dead);

            photonView.RPC("OnDamage", RpcTarget.Others, damage); // 다른 클라이언트도 실행
        }

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    [PunRPC]
    public virtual void RestoreHealth(float newHealth)
    {
        if (dead)
        {
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            health += newHealth;

            photonView.RPC("ApplyUpdateHealth", RpcTarget.Others, health, dead);
            photonView.RPC("RestoreHealth", RpcTarget.Others, newHealth);
        }
    }

    public virtual void Die()
    {
        if (onDeath != null)
        {
            onDeath();
        }

        dead = true;
    }
}
