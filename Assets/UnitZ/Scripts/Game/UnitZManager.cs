using UnityEngine;
using System.Collections;

public class UnitZManager : MonoBehaviour
{
    void Awake()
    {
        UnitZ.gameNetwork = (GameNetwork)GameObject.FindObjectOfType(typeof(GameNetwork));
        UnitZ.gameManager = (GameManager)GameObject.FindObjectOfType(typeof(GameManager));
        UnitZ.characterManager = (CharacterManager)GameObject.FindObjectOfType(typeof(CharacterManager));
        UnitZ.itemManager = (ItemManager)GameObject.FindObjectOfType(typeof(ItemManager));
        UnitZ.itemCraftManager = (ItemCrafterManager)GameObject.FindObjectOfType(typeof(ItemCrafterManager));
        UnitZ.playerManager = (PlayerManager)GameObject.FindObjectOfType(typeof(PlayerManager));
        UnitZ.playerSave = (PlayerSave)GameObject.FindObjectOfType(typeof(PlayerSave));
        UnitZ.levelManager = (LevelManager)GameObject.FindObjectOfType(typeof(LevelManager));
        UnitZ.aiManager = (AIManager)GameObject.FindObjectOfType(typeof(AIManager));
        UnitZ.Hud = (PlayerHUDCanvas)GameObject.FindObjectOfType(typeof(PlayerHUDCanvas));
        UnitZ.GameKeyVersion = Application.version;
    }
}

public static class UnitZ
{
    public static AIManager aiManager;
    public static GameNetwork gameNetwork;
    public static GameManager gameManager;
    public static CharacterManager characterManager;
    public static ItemManager itemManager;
    public static ItemCrafterManager itemCraftManager;
    public static PlayerManager playerManager;
    public static PlayerSave playerSave;
    public static LevelManager levelManager;
    public static PlayerHUDCanvas Hud;
    public static string GameKeyVersion = "";
    public static bool IsOnline = false;
    public static NetworkSyncObject NetworkGameplay;

    public static NetworkSyncObject NetworkObject()
    {
        if (NetworkGameplay != null)
            return NetworkGameplay;

        NetworkGameplay = (NetworkSyncObject)GameObject.FindObjectOfType(typeof(NetworkSyncObject));
        return NetworkGameplay;
    }

}