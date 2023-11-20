using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class InGamePlayer
{
    public Player player = null;
    public bool isReady = false;
    public GameObject playerObject;
}

public class TurnManager : MonoBehaviour
{ 
    private List<Player> players = new List<Player>();
    private int _turn = -1;
    private int _round = 0;
    private float turnTime;
    private PhotonView PV;
    private bool gameStart = false;
    public float baseTurnTime = 18f;
    public static TurnManager Instance;
    public TextMeshProUGUI Round_TMP;
    public TextMeshProUGUI TurnTime_TMP;
    public Button NextTurnButton;
    public Button StartGameButton;
    public GameObject PlayerListImage;
    public GameObject PlayerInfoButtonPrefab;
    private CameraManager _cameraManager;
    private GameManager _gameManager;
    private ItemSpawnManager _itemSpawnManager;
    private SkillShooter _skillShooter;
    [field: SerializeField] public bool _isMyTurn { get; private set; } = true;
    [SerializeField] private bool _turnSettingEnd = false;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(this.gameObject);
        }

        PV = GetComponent<PhotonView>();
        DontDestroyOnLoad(gameObject);
        _cameraManager = FindObjectOfType<CameraManager>();
        _gameManager = FindObjectOfType<GameManager>();
        _itemSpawnManager = FindObjectOfType<ItemSpawnManager>();
        turnTime = baseTurnTime;
    }

    private void FixedUpdate()
    {
        if (gameStart)
        {
            if (_isMyTurn && !_turnSettingEnd) // 턴 시작.
            {
                _turnSettingEnd = true;
                TurnSetting();
            }
            turnTime -= Time.deltaTime;
            TurnTime_TMP.text = "TimeTurn : " + Mathf.Round(turnTime);
            if (turnTime < 0)
            {
                turnTime = baseTurnTime;
                EndTurn();
                _turnSettingEnd = false;
            }
        }
        else
        {
            _cameraManager.SetTurnCamera(_gameManager._myPlayer);
        }
    }

    public void TurnSetting()
    {
        _cameraManager.SetTurnCamera(_gameManager._myPlayer);
        _itemSpawnManager.SpawnItemRandomly();
        _gameManager._myPlayer.TryGetComponent(out _skillShooter);
        _skillShooter.CanUseSkill();
    }
    public int FindMyPlayerIndex()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; ++i)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                return i;
            }
        }

        return -1;
    }

    public void StartGame()
    {
        if (gameStart)
        {
            return;
        }
        PV.RPC(nameof(StartGameRPC), RpcTarget.AllBuffered);
    }
    
    public void EndTurn()
    {
        if (!_isMyTurn)
        {
            return;
        }
        PV.RPC(nameof(TurnEndRPC), RpcTarget.AllBuffered);
    }

    public void SetPlayerList()
    {
        PV.RPC(nameof(SetPlayerListRPC), RpcTarget.AllBuffered);
    }
    
    private void EndRound()
    {
        _round++;
        Round_TMP.text = "Round: " + _round;
    }

    [PunRPC]
    private void StartGameRPC()
    {
        if (!gameStart)
        {
            gameStart = true;
            _turn = PhotonNetwork.PlayerList.Length;
            EndTurn();
            NextTurnButton.gameObject.SetActive(true);
            StartGameButton.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    private void TurnEndRPC()
    {
        _turn += 1;
        turnTime = baseTurnTime;
        if (_turn >= PhotonNetwork.PlayerList.Length)
        {
            EndRound();
            _turn = 0;
        }

        if (FindMyPlayerIndex() == _turn)
        {
            _isMyTurn = true;
            NextTurnButton.gameObject.SetActive(true);
        }
        else
        {
            _isMyTurn = false;
            NextTurnButton.gameObject.SetActive(false);
        }
    }

    [PunRPC]
    private void SetPlayerListRPC()
    {
        players.Clear();
        foreach (Transform child in PlayerListImage.transform)
        {
            Destroy(child.gameObject) ;
        }
        foreach (var i in PhotonNetwork.PlayerList)
        {
            players.Add(i);
            GameObject temp = Instantiate(PlayerInfoButtonPrefab, Vector3.zero, Quaternion.identity);
            temp.transform.parent = PlayerListImage.transform;
            temp.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.NickName;
        }
       
    }
}