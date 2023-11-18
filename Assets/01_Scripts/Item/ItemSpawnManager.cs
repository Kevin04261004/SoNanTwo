using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    private const float MAPTOP = 8.5f;
    private const float MAPBOTTOM = -8.5f;
    private const float MAPLEFT = -8.5f;
    private const float MAPRIGHT = 8.5f;
    private Vector3 spawnPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int i = 0; i < 10; i++)
            {
                SpawnItemRandomly();
            }
        }
    }

    public void SpawnItemRandomly()
    {
        spawnPos = new Vector3(Random.Range(MAPLEFT, MAPRIGHT), 5, Random.Range(MAPBOTTOM, MAPTOP));
        PhotonNetwork.Instantiate(item.name, spawnPos, Quaternion.identity);
    }
}
