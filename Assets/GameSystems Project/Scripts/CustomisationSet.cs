using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This handles all the information from the customisation scene, then saves it into PlayerData so it can be loaded into the game scene.
/// </summary>
public class CustomisationSet : MonoBehaviour
{
    [Header("Character Name")]
    public string characterName;
    [SerializeField] private TMP_InputField nameInput;

    [Header ("Character Class")]
    public CharacterClass characterClass = CharacterClass.Barbarian;
    public string[] selectedClass = new string[3];
    public int selectedClassIndex = 0;
    
    [System.Serializable]
    public struct Stats
    {
        public string baseStatsName;
        public int baseStats;
        public int tempStats;
    };
    public Stats[] characterStats;

    [Header("Starting Stat Points")]
    public int statPoints = 10;

    [Header("Texture Lists")]
    public List<Texture2D> skin = new List<Texture2D>();
    public List<Texture2D> eyes = new List<Texture2D>();
    public List<Texture2D> hair = new List<Texture2D>();
    public List<Texture2D> mouth = new List<Texture2D>();
    public List<Texture2D> armour = new List<Texture2D>();
    public List<Texture2D> clothes = new List<Texture2D>();
    [Header("Current Texture Index")]
    public int skinIndex;
    public int eyesIndex, mouthIndex, hairIndex, armourIndex, clothesIndex;
    [Header ("Max amount of textures per type")]
    public int skinMax;
    public int eyesMax, mouthMax, hairMax, armourMax, clothesMax;
    [Header("Renderer")]
    public Renderer characterRenderer;
    
    private string[] matName = new string[6];

    [Header("Class Inputs")]
    [SerializeField] private TMP_Dropdown classDropdown;
    [SerializeField] private TMP_Text classAbilityText;
    List<string> names = new List<string>() { "Select Class", "Barbarian", "Ranger", "Mage" };

    [Header("Race Inputs")]
    [SerializeField] private TMP_Dropdown raceDropdown;
    [SerializeField] private TMP_Text raceAbilityText;
    public int raceIndex;
    public string raceName;
    List<string> races = new List<string>() { "Select Race", "Human", "Elf", "Dwarf" };

    [Header("Stat Text Fields")]
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text healthRegenText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text stamText;
    [SerializeField] private TMP_Text manaText;
    [SerializeField] private TMP_Text manaRegenText;
    [SerializeField] private TMP_Text statPointsText;

   

    private void Start()
    {
        matName = new string[] { "Skin", "Eyes", "Mouth", "Hair", "Armour", "Clothes" };

        selectedClass = new string[] { "Barbarian", "Ranger", "Mage" };


        classDropdown.AddOptions(names);
        raceDropdown.AddOptions(races);

        //Setting temp stats to 0
        for (int i = 0; i < characterStats.Length; i++)
        {
            characterStats[i].tempStats = 0;
        }

        // Adds all the textures from Resources into their respective lists
        #region Adding Textures to Lists
        for (int i = 0; i < skinMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Skin_" + i) as Texture2D;
            skin.Add(tempTexture);
        }
        for (int i = 0; i < eyesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyes.Add(tempTexture);
        }
        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouth.Add(tempTexture);
        }
        for (int i = 0; i < hairMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Hair_" + i) as Texture2D;
            hair.Add(tempTexture);
        }
        for (int i = 0; i < armourMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Armour_" + i) as Texture2D;
            armour.Add(tempTexture);
        }
        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D tempTexture = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothes.Add(tempTexture);
        }
        #endregion
    }
          
    /// <summary>
    /// Save player function saves data from the customiser and loads the game scene
    /// </summary>
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("saved");
        SceneManager.LoadScene("Loading Screen");
    }

    
    public void NameInput()
    {
        characterName = nameInput.text;
        Debug.Log(characterName);
    }   //Sets Character name after inputing data

    //Takes slider value and sets corresponding textures
    #region Apperance Slider Functions
    public void SkinSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;
                
        textures = skin.ToArray();
        matIndex = 1;
        
        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        skinIndex = indexint;
    }
    public void EyesSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;

        textures = eyes.ToArray();
        matIndex = 2;

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        eyesIndex = indexint;

    }
    public void MouthSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;

        textures = mouth.ToArray();
        matIndex = 3;

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        mouthIndex = indexint;

    }
    public void HairSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;

        textures = hair.ToArray();
        matIndex = 4;

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        hairIndex = indexint;

    }
    public void ClothesSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;

        textures = clothes.ToArray();
        matIndex = 5;

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        clothesIndex = indexint;

    }
    public void ArmourSlider(float index)
    {
        int indexint = Mathf.RoundToInt(index);
        Texture2D[] textures = new Texture2D[0];
        int matIndex;

        textures = armour.ToArray();
        matIndex = 6;

        Material[] mat = characterRenderer.materials;
        mat[matIndex].mainTexture = textures[indexint];
        characterRenderer.materials = mat;
        armourIndex = indexint;

    }

    #endregion  

    // functions for the dropdown boxes
    #region Dropdown Functions
    public void RaceDropdown(int index)
    {
        switch (index)
        {
            case 0:
                break;
            case 1:
                raceIndex = 1;
                raceName = "Human";
                raceAbilityText.text = "The Humans special ability is Call to Arms. Takes all the enemy aggro for 20s.";
                break;
            case 2:
                raceIndex = 2;
                raceName = "Elf";
                raceAbilityText.text = "The Elfs special ability is Call of the Wild. All Enemys become Afraid.";

                break;
            case 3:
                raceIndex = 3;
                raceName = "Dwarf";
                raceAbilityText.text = "The Dwarfs special ability is Call of the Deep. Blind all enemys for 20s.";

                break;
        }
    } //selects the race
    public void ClassDropdown(int index)
    {
        selectedClassIndex = index - 1;
        ChooseClass(index - 1);
        SetClassStats(index);
        Debug.Log("selected class" + index);
    }   //chooses class and sets stats
    #endregion      

    //All the Stats + functions for buttons
    #region Stat Points Plus  

    public void PlusHealth()
    {
        if (statPoints>0)
        {
            statPoints--;
            characterStats[0].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);            
        }
    }
    public void PlusHealthRegen()
    {
        if (statPoints > 0)
        {
            statPoints--;
            characterStats[1].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);
        }
    }
    public void PlusSpeed()
    {
        if (statPoints > 0)
        {
            statPoints--;
            characterStats[2].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);
        }
    }
    public void PlusStamina()
    {
        if (statPoints > 0)
        {
            statPoints--;
            characterStats[3].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);
        }
    }
    public void PlusMana()
    {
        if (statPoints > 0)
        {
            statPoints--;
            characterStats[4].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);
        }
    }
    public void PlusManaRegen()
    {
        if (statPoints > 0)
        {
            statPoints--;
            characterStats[5].tempStats++;
            statPointsText.text = "Points: " + statPoints.ToString();
            SetClassStats(1);
        }
    }

    #endregion

    // All the stats - functions for buttons
    #region Stat Points Minus
    public void MinusHealth()
    {
        if (statPoints < 10 && characterStats[0].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[0].tempStats--;
            SetClassStats(1);
            
        }
    }
    public void MinusHealthRegen()
    {
        if (statPoints < 10 && characterStats[1].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[1].tempStats--;
            SetClassStats(1);

        }
    }
    public void MinusSpeed()
    {
        if (statPoints < 10 && characterStats[2].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[2].tempStats--;
            SetClassStats(1);

        }
    }
    public void MinusStamina()
    {
        if (statPoints < 10 && characterStats[3].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[3].tempStats--;
            SetClassStats(1);

        }
    }
    public void MinusMana()
    {
        if (statPoints < 10 && characterStats[4].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[4].tempStats--;
            SetClassStats(1);

        }
    }
    public void MinusManaRegen()
    {
        if (statPoints < 10 && characterStats[5].tempStats > 0)
        {
            statPoints++;
            statPointsText.text = "Points: " + statPoints.ToString();
            characterStats[5].tempStats--;
            SetClassStats(1);

        }
    }
    #endregion 

    /// <summary>
    /// Sets stat textboxes with updated stats.
    /// </summary>
    /// <param name="_index"></param>
    public void SetClassStats(int _index)
    {
        switch (_index - 1)
        {            
            default:
                healthText.text = "Health: " + (characterStats[0].baseStats + characterStats[0].tempStats);
                healthRegenText.text = "Health Regen: " + (characterStats[1].baseStats + characterStats[1].tempStats);
                speedText.text = "Speed:" + (characterStats[2].baseStats + characterStats[2].tempStats);
                stamText.text = "Stamina:" + (characterStats[3].baseStats + characterStats[3].tempStats);
                manaText.text = "Mana:" + (characterStats[4].baseStats + characterStats[4].tempStats);
                manaRegenText.text = "Mana Regen:" + (characterStats[5].baseStats + characterStats[5].tempStats);
                break;
        }

    }

    /// <summary>
    /// Sets the base stats & ability for each class
    /// </summary>
    /// <param name="classIndex"></param>
    public void ChooseClass(int classIndex)
    {
        switch (classIndex)
        {
            case 0:
                characterStats[0].baseStats = 10;
                characterStats[1].baseStats = 10;
                characterStats[2].baseStats = 5;
                characterStats[3].baseStats = 7;
                characterStats[4].baseStats = 5;
                characterStats[5].baseStats = 7;
                characterClass = CharacterClass.Barbarian;
                selectedClassIndex = 0;
                classAbilityText.text = "The barbarians special ability is Blood Rage. Ignore damage for 5s. Cooldown: 30s.";
                break;
            case 1:
                characterStats[0].baseStats = 7;
                characterStats[1].baseStats = 5;
                characterStats[2].baseStats = 10;
                characterStats[3].baseStats = 10;
                characterStats[4].baseStats = 7;
                characterStats[5].baseStats = 5;
                characterClass = CharacterClass.Ranger;
                selectedClassIndex = 1;
                classAbilityText.text = "The Rangers special ability is Ghost Wolf. Calls a spectral wolf to fight for 10s. Cooldown: 45s.";
                break;
            case 2:
                characterStats[0].baseStats = 5;
                characterStats[1].baseStats = 7;
                characterStats[2].baseStats = 7;
                characterStats[3].baseStats = 5;
                characterStats[4].baseStats = 10;
                characterStats[5].baseStats = 10;
                characterClass = CharacterClass.Mage;
                selectedClassIndex = 2;
                classAbilityText.text = "The Mages special ability is Fireball. Summon a fireball from the heavens. Cooldown: 45s.";
                break;
        }
    }
    
    
}

/// <summary>
/// Enum for classes
/// </summary>
    public enum CharacterClass
    {
        Barbarian,
        Ranger,
        Mage,
    }

/*Journal
 * Well, this things a bit of a mess.
 * Definately could have done things a bit neater and more streamlined.
 * 
 */