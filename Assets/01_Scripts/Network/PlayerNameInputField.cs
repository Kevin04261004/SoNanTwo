using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    private const string playerNamePrefKey = "PlayerName";
    private TMP_InputField _inputField;
    private void Start()
    {
        string defaultName = String.Empty;
        _inputField = this.GetComponent<TMP_InputField>();
        // if (_inputField != null)
        // {
        //     if (PlayerPrefs.HasKey(playerNamePrefKey))
        //     {
        //         defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        //         _inputField.text = defaultName;
        //     }
        // }

        PhotonNetwork.NickName = defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or Empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}