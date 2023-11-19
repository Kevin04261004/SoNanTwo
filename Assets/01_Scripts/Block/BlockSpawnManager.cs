using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

enum BLOCK
{
    low,
    normal,
    high,

}

public class BlockSpawnManager : MonoBehaviourPun
{
    public GameObject target;
    public GameObject[] Block;

    public int numberOfObject;

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < numberOfObject; i++)
            {
                SpawnObjects();
                //CreateBlock();
                /*Vector3 randomPosition = GetNonoverlapPos();
                GameObject clone = Instantiate(Block[Random.Range(0, Block.Length)], randomPosition, Quaternion.identity);*/
            }
        }

    }

    private void SpawnObjects()
    {
        bool b = true;
        while(b)
        {
            float xPos = Random.Range(-18f, 18f);
            float yPos = 1f;
            float zPos = Random.Range(-18f, 18f);
            Vector3 spawnPos = new Vector3(xPos, yPos, zPos);
            if (FindCollisions(spawnPos) < 1)
            {
                PhotonNetwork.Instantiate(Block[Random.Range(0, Block.Length)].name, spawnPos, Quaternion.identity);
                //GameObject c = Instantiate(Block[Random.Range(0, Block.Length)], spawnPos, Quaternion.identity);
                b = false;
            }
        }

    }

    private int FindCollisions(Vector3 pos)
    {
        Collider[] hits = Physics.OverlapSphere(pos, 0.5f);
        for(int i = 0; i < hits.Length; i++)
        {
            if(hits[i].CompareTag("Wall"))
            {
                return 2;
            }
        }
        return 0;
    }

    private void CreateBlock()
    {
        GameObject block = Block[Random.Range(0, Block.Length)];

        Vector3 randomPosition = new Vector3(Random.Range(-18f, 18f), 1f, Random.Range(-18f, 18f));
        int LoopStop = 0;
        while (Physics.CheckSphere(randomPosition, 0.5f))
        {
            LoopStop++;
            if (LoopStop > 10000)
            {
                Debug.Log("Something Wrong Box Position");
                break;
            }
            randomPosition = new Vector3(Random.Range(-18f, 18f), 1f, Random.Range(-18f, 18f));
        }
        if(LoopStop < 10000)
        {
            Instantiate(block, randomPosition, Quaternion.identity);
            //PhotonNetwork.Instantiate(block.name, randomPosition, Quaternion.identity);
        }

    }
}
