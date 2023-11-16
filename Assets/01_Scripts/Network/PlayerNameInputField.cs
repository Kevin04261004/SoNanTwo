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
<<<<<<< HEAD
        // if (_inputField != null)
        // {
        //     if (PlayerPrefs.HasKey(playerNamePrefKey))
        //     {
        //         defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        //         _inputField.text = defaultName;
        //     }
        // }
=======
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
>>>>>>> a5cc017325a4ac5bf9b70b63911386b24f8c404c

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