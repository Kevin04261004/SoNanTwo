using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> cams;
    [SerializeField] private CinemachineVirtualCamera turnCamera;
    private int index = 0;
    private PhotonView PV;

    private void Awake()
    {
        TryGetComponent(out PV);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            turnCamera.gameObject.SetActive(false);
            cams[index].SetActive(false);
            index++;
            if (index > cams.Count - 1)
            {
                index = 0;
            }
            cams[index].SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            turnCamera.gameObject.SetActive(true);
        }
    }

    public void SetTurnCamera(GameObject target)
    {
        PV.RPC(nameof(SetTurnCameraRPC), RpcTarget.All, target.transform.position);
    }
    
    [PunRPC]
    private void SetTurnCameraRPC(Vector3 target)
    {
        Collider[] colliders = Physics.OverlapSphere(target, 0.1f);
        foreach (var i in colliders)
        {
            if (i.CompareTag("Player"))
            {
                turnCamera.LookAt = i.transform;       
            }
        }
    }
    
}
