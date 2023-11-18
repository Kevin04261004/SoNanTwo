using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Item : MonoBehaviour
{
    private const int SKILLCOUNT = 5;

    private PhotonView PV;
    private GameObject receivedPlayer;
    public GameObject ReceivedPlayer
    {
        set
        {
            receivedPlayer = value;
        }
    }

    private int[] skillPercentage = new int[SKILLCOUNT];
    private int[] calcedPercentage = new int[SKILLCOUNT];

    private int randNum;

    private int selectedSkill;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        InputSkillArray();
    }

    private void InputSkillArray()
    {
        skillPercentage[0] = 40;
        skillPercentage[1] = 20;
        skillPercentage[2] = 20;
        skillPercentage[3] = 10;
        skillPercentage[4] = 10;

        int num = 0;

        for (int i = 0; i < SKILLCOUNT; i++)
        {
            num += skillPercentage[i];
            calcedPercentage[i] = num;
        }
    }

    private void CheckSkill(int repeat)
    {
        if (randNum <= calcedPercentage[repeat])
        {
            selectedSkill = repeat;
        }
        else
        {
            if (repeat > SKILLCOUNT)
            {
                Debug.LogError("»óÀÚ±ø È®·ü ¹®Á¦ »ý±è");
            }
            else
            {
                CheckSkill(++repeat);
            }
        }
    }

    public void ReceiveItem()
    {
        randNum = Random.Range(1, 100);
        CheckSkill(0);

        PV.RPC(nameof(DestroyThisRPC), RpcTarget.All);
    }

    [PunRPC]
    public void DestroyThisRPC()
    {
        PhotonNetwork.Destroy(gameObject);
    }
}
