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
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UseSkill();
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
