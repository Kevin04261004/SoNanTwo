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
            if (Input.GetMouseButtonUp(1))
            {
                UseSkill();
            }
            if (Input.GetMouseButton(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 m_dir = Vector3.zero;
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    m_dir = (hit.point - transform.position).normalized;
                    transform.rotation = Quaternion.LookRotation(new Vector3(m_dir.x, 0, m_dir.z)); // 신 주 환
                }

                // ==== 실 패 작 ====
                //transform.forward = new Vector3(hit.point.x, hit.point.y, hit.point.z) - transform.position;
                //transform.rotation = Quaternion.FromToRotation(transform.forward, new Vector3(hit.point.x, 0, hit.point.z));
                //Vector3 dir = GetMousePositon() - transform.position;
                //transform.rotation = Quaternion.Euler(0, Mathf.Atan2(dir.x, dir.) * Mathf.Rad2Deg, 0);
                //Quaternion.Euler(0, Quaternion.FromToRotation(Vector3.forward, transform.position - GetMousePositon()).eulerAngles.z, 0);
                // ==================
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
                Debug.Assert(true, "default bug!!!");
                break;
        }
    }

    // 마우스 위치 구하는 거였으나 버려짐
    //private Vector3 GetMousePositon()
    //{
    //    Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    //    return point;
    //}
}
