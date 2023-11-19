using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class KItem : MonoBehaviour
{
    private ESkillType type = ESkillType.RedBullet;
    private int amount = 1;
    private SkillShooter skillShooter;
    private void SetRandomType()
    {
        type = (ESkillType)Random.Range(0, (int)ESkillType.Size);
        amount = Random.Range(1, 3);
    }

    private void OnEnable()
    {
        SetRandomType();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent(out skillShooter);
            skillShooter.GetItem(type,amount);
           PhotonNetwork.Destroy(gameObject);
        }
    }
}