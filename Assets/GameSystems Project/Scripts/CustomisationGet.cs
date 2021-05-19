using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomisationGet : MonoBehaviour
{
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] public GameObject player;

    
    public PlayerStats playerStats;


    [Header("Names")]
    public static string characterName;
    public static int classIndex;
    public static int raceIndex;
    public static string raceName;

    [Header("Stats")]
    public static int level;
    public static int healthMax;
    public static int healthRegen;
    public static int speed;
    public static int stamina;
    public static int manaMax;
    public static int manaRegen;

    // Start is called before the first frame update
    void Start()
    {
        Load();
        
    }

    /// <summary>
    /// Loads all the saved player data
    /// </summary>
    public void Load()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        SetTexture("skin", data.visual[0]);
        SetTexture("eyes", data.visual[1]);
        SetTexture("mouth", data.visual[2]);
        SetTexture("hair", data.visual[3]);
        SetTexture("armour", data.visual[4]);
        SetTexture("clothes", data.visual[5]);
        characterName = data.name;
        classIndex = data.classIndex;
        raceIndex = data.raceIndex;
        raceName = data.raceName;


        healthMax = data.stats[0];
        healthRegen = data.stats[1];
        speed = data.stats[2];
        stamina = data.stats[3];
        manaMax = data.stats[4];
        manaRegen = data.stats[5];

    }

    public void SaveInGame()
    {
        SaveSystem.SavePlayerInGame(playerStats, PlayerData.ThePlayerData);
        Debug.Log("Saved In Game");
    }


    public void LoadInGame()
    {
        PlayerDataInGame data = SaveSystem.LoadPlayerInGame();

        SetTexture("skin", data.visual[0]);
        SetTexture("eyes", data.visual[1]);
        SetTexture("mouth", data.visual[2]);
        SetTexture("hair", data.visual[3]);
        SetTexture("armour", data.visual[4]);
        SetTexture("clothes", data.visual[5]);
        characterName = data.name;
        classIndex = data.classIndex;
        raceIndex = data.raceIndex;
        raceName = data.raceName;
        level = data.level;

        healthMax = data.stats[0];
        healthRegen = data.stats[1];
        speed = data.stats[2];
        stamina = data.stats[3];
        manaMax = data.stats[4];
        manaRegen = data.stats[5];

        playerStats.SetValues();

    }

    /// <summary>
    /// Sets the saved textures to the character
    /// </summary>
    /// <param name="type">Name of texture</param>
    /// <param name="index">Texture index number</param>
    void SetTexture(string type, int index)
    {
        Texture2D texture = null;
        int matIndex = 0;
        switch (type)
        {
            case "skin":
                texture = Resources.Load("Character/Skin_" + index) as Texture2D;
                matIndex = 1;
                break;
            case "eyes":
                texture = Resources.Load("Character/Eyes_" + index) as Texture2D;
                matIndex = 2;
                break;
            case "mouth":
                texture = Resources.Load("Character/Mouth_" + index) as Texture2D;
                matIndex = 3;
                break;
            case "hair":
                texture = Resources.Load("Character/Hair_" + index) as Texture2D;
                matIndex = 4;
                break;
            case "armour":
                texture = Resources.Load("Character/Armour_" + index) as Texture2D;
                matIndex = 5;
                break;
            case "clothes":
                texture = Resources.Load("Character/Clothes_" + index) as Texture2D;
                matIndex = 6;
                break;
        }
                
        Material[] mats = characterRenderer.materials;
        mats[matIndex].mainTexture = texture;
        characterRenderer.materials = mats;




    }
    
}
