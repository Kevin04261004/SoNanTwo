using System;
using System.Collections;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    private static readonly string GAMEVERSION = "1";
    private Coroutine _showRoomCoroutine;
    private WaitForSeconds _oneSec = new WaitForSeconds(1);
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject progressLabel;
    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    
    public void Connect()
    {
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = GAMEVERSION;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void OnConnectedToServer()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
    }
}
