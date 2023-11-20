using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

public enum ESkillType
{
    RedBullet, // 기본 불렛
    Size,
}
public class SkillShooter : MonoBehaviour
{
    private ESkillType curSkillType;
    [SerializeField] private GameObject redBullet_Prefab;
    private PhotonView PV;
    [SerializeField] private List<Skill> skills = new List<Skill>(); // 이게 인벤임.
    private bool _usedSkill = false;
    private UIManager _uiManager;
    public void CanUseSkill() // 턴에서 제어.
    {
        _usedSkill = true;
    }
    private void Awake()
    {
        if (skills.Count != (int)ESkillType.Size)
        {
            Debug.Assert(true,"게임 시작 전에 아이템 리스트에 넣어주세요.");
        }
        TryGetComponent(out PV);
        _uiManager = FindObjectOfType<UIManager>();
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
        var position = transform.position;
        Vector3 spawnPos = new Vector3(position.x, position.y + 0.75f, position.z);
        switch (curSkillType)
        {
            case ESkillType.RedBullet:
                if (skills[(int)ESkillType.RedBullet].count > 0)
                {
                    PhotonNetwork.Instantiate(redBullet_Prefab.name, spawnPos, transform.rotation).TryGetComponent(out BaseBullet temp);
                    temp.SetFromViewID(PV.ViewID);   
                }
                break;
            default:
                Debug.Assert(true, "default bug!!!");
                break;
        }
        _usedSkill = true;
        _uiManager.UpdateInven(ref skills);
    }

    public void GetItem(ESkillType skillType, int amount = 1)
    {
        foreach (var s in skills)
        {
            if(s.skillType == skillType)
            {
                s.count += amount;
            }
        }
        _uiManager.UpdateInven(ref skills);
    }

    // 마우스 위치 구하는 거였으나 버려짐
    //private Vector3 GetMousePositon()
    //{
    //    Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    //    return point;
    //}
}
