using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    [SerializeField] private Material[] skyboxMaterials;
    
    private PhotonView PV;

    private void Awake()
    {
        if (!TryGetComponent(out PV))
        {
            Debug.Assert(true, "PV 넣으셈...");   
        }
    }

    public void SetRandomSkyBox()
    {
        PV.RPC(nameof(SetSkyBoxWithIndexRPC),RpcTarget.All, Random.Range(0, skyboxMaterials.Length));
    }

    [PunRPC]
    public void SetSkyBoxWithIndexRPC(int i)
    {
        RenderSettings.skybox = skyboxMaterials[i];
    }
}
