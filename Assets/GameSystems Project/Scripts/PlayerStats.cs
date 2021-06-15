using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Holds all the players data other than movement.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    public static PlayerStats ThePlayerStats;

    [Header("Name")]
    public string characterName;
    [SerializeField] private TMP_Text nameText;

    [Header("Class")]
    public int classIndex;
    public string className;

    [Header("Race")]
    public int raceIndex;
    public string raceName;

    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text level;    
    public int levelInt;

    [Header ("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    public int healthMax;
    public float health;
    public int healthRegen;
    
    [Header("Mana")]
    [SerializeField] private Slider manaSlider;
    [SerializeField] private TMP_Text manatext;
    public int manaMax;
    public float mana;
    public int manaRegen;

    [Header("Damage UI")]
    [SerializeField] private Transform popUpLocation;
    [SerializeField] private GameObject damagePrefab;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private float damage = 3f;

    [Header("Death UI")]
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private AudioSource deathSound;
    private bool alreadyDead = false;

    private void Awake()
    {
        if (ThePlayerStats == null)
        {
            ThePlayerStats = this;
        }
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        ClassName(classIndex);

        description.text = raceName + " " + className;        
        nameText.text = characterName;
        deathPanel.SetActive(false);
        alreadyDead = false;

        // Don't think this is working properly as potions and food can increase above the max amount.
        health = Mathf.Clamp(health, 0, healthMax);
    }

    // Update is called once per frame
    void Update()
    {
        UseMana();
        UpdateSliders();
        GetHurt();
        LevelUp();
    }

    /// <summary>
    /// Sets all the saved data from CustomisationGet
    /// </summary>
    public void SetValues()
    {
        characterName = CustomisationGet.characterName;
        classIndex = CustomisationGet.classIndex;
        raceIndex = CustomisationGet.raceIndex;
        raceName = CustomisationGet.raceName;

        levelInt = CustomisationGet.level;
        level.text = "Level: " + levelInt;
        healthMax = CustomisationGet.healthMax;
        health = healthMax;
        healthRegen = CustomisationGet.healthRegen;
        healthSlider.maxValue = healthMax;
        manaMax = CustomisationGet.manaMax;
        mana = manaMax;
        manaRegen = CustomisationGet.manaRegen;
        manaSlider.maxValue = manaMax;
        

        
    }

    /// <summary>
    /// Gets tha class index and sets the Name
    /// </summary>
    /// <param name="index"></param>
    private void ClassName(int index)
    {
        switch (index)
        {
            case 0:
                className = "Barbarian";
                break;
            case 1:
                className = "Ranger";
                break;
            case 2:
                className = "Mage";
                break;
        }
    }

    /// <summary>
    /// The function that controls mana usage and mana regen
    /// </summary>
    private void UseMana()
    {
        if (Input.GetButton("Cast") && mana > 0)
        {
            mana -= Time.deltaTime;
        }
        if(mana < manaMax && !Input.GetButton("Cast"))
        {
            mana += (manaRegen * 0.1f) * Time.deltaTime;
        }
    }

    /// <summary>
    /// Update Sliders with correct & updated values 
    /// </summary>
    private void UpdateSliders()
    {
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
        healthText.text = "Health: " + Mathf.RoundToInt(health) + "/" + healthMax ;

        manaSlider.maxValue = manaMax;
        manaSlider.value = mana;
        manatext.text = "Mana: " + Mathf.RoundToInt(mana) + "/" + manaMax;
    }

    /// <summary>
    /// Function for taking damage and health regen
    /// </summary>
    private void GetHurt()
    {
        if (Input.GetButtonDown("Damage"))
        {
            health -= damage;
            // This is the damage pop up when getting hurt.
            GameObject popUp = Instantiate(damagePrefab, popUpLocation);
            damageText = popUp.GetComponentInChildren<TMP_Text>();
            damageText.text = damage.ToString("0");            
            Destroy(popUp, .5f);
        }
        if(health < healthMax)
        {
            health += (healthRegen*0.1f) * Time.deltaTime;
        }
        
        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// For when the character Dies.
    /// </summary>
    public void Die()
    {
        // Using a bool so this only happens once as GetHurt is happening in Update.
        if (!alreadyDead)
        {
            deathSound.Play();
            Debug.Log("Dead");
            // Death panel is animated and then runs the LoadScene function as an event within the animation.
            deathPanel.SetActive(true);
            alreadyDead = true;
        }        
    }

    /// <summary>
    /// Function for leveling up. Sets level text, level int and max of mana & health bars.
    /// </summary>
    public void LevelUp()
    {
        if (Input.GetButtonDown("LevelUp"))
        {
            levelInt++;
            healthMax += Mathf.RoundToInt(healthMax * 0.3f);
            manaMax += Mathf.RoundToInt(manaMax * 0.3f);
            level.text = "Level:" + levelInt;
        }
    }
}
