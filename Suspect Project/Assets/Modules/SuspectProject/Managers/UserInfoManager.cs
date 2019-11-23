using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfoManager : MonoBehaviour
{
    static private UserInfoManager _instance = null;

    private string _playerName = "";

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if(_instance == this)
        {
            _instance = null;
        }
    }

    static public void SetPlayerName(string playerName)
    {
        _instance._playerName = playerName;
    }

    static public string GetPlayerName()
    {
        return _instance._playerName;
    }
}
