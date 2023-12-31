using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject item;
    [SerializeField] private float MAPTOP = 19f;
    [SerializeField] private float MAPBOTTOM = -19f;
    [SerializeField] private float MAPLEFT = -19f;
    [SerializeField] private float MAPRIGHT = 19f;
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
