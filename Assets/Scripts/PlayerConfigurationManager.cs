using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 2;

    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance!=null)
        {
            Debug.Log("Try create anothe instance");

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }
    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        Debug.Log(playerConfigs.Count);
        return playerConfigs;
    }
    public void SetPlayerColor(int index,Material color)
    {
        Debug.Log(playerConfigs.Count);
        playerConfigs[index].PlayerMaterial = color;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count==MaxPlayers && playerConfigs.All(p=>p.IsReady==true))
        {
            SceneManager.LoadScene("FightScene");
        }
    }
    public void HadlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("PLayer Joined " + pi.playerIndex);
        if(!playerConfigs.Any(p=> p.PlayerIndex==pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
        }
    }
}
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }
    public int PlayerIndex{ get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }
}
