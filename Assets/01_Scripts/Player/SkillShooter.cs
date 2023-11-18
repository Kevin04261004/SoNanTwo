using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

enum ESkillType
{
    RedBullet, // 기본 불렛

}

public class SkillShooter : MonoBehaviour
{
    private ESkillType skillType;
    [SerializeField] private GameObject redBullet_Prefab;
    private PhotonView PV;

    private void Awake()
    {
        TryGetComponent<PhotonView>(out PV);
    }

    private void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetMouseButtonDown(1))
            {
                UseSkill();
            }   
        }
    }

    private void UseSkill()
    {
        switch (skillType)
        {
            case ESkillType.RedBullet:
                PhotonNetwork.Instantiate(redBullet_Prefab.name, transform.position, transform.rotation);
                break;
            default:
                Debug.Assert(true,"default bug!!!");
                break;
        }
    }
}
