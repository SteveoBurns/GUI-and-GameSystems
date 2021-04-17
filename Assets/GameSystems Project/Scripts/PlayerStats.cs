using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Name")]
    public string characterName;

    [Header("Class")]
    public int classIndex;
    public string className;

    [Header("Race")]
    public int raceIndex;
    public string raceName;

    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text level;    
    private int levelInt;

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

    // Start is called before the first frame update
    void Start()
    {
        SetValues();
        ClassName(classIndex);
        description.text = raceName + " " + className;
        levelInt = 1;
        level.text = "Level: " + levelInt;
    }

    // Update is called once per frame
    void Update()
    {
        UseMana();
        UpdateSliders();
        GetHurt();
        LevelUp();
    }

    private void SetValues()
    {
        characterName = CustomisationGet.characterName;
        classIndex = CustomisationGet.classIndex;
        raceIndex = CustomisationGet.raceIndex;
        raceName = CustomisationGet.raceName;

        healthMax = CustomisationGet.healthMax;
        health = healthMax;
        healthRegen = CustomisationGet.healthRegen;
        healthSlider.maxValue = healthMax;
        manaMax = CustomisationGet.manaMax;
        mana = manaMax;
        manaRegen = CustomisationGet.manaRegen;
        manaSlider.maxValue = manaMax;
    }

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

    private void UpdateSliders()
    {
        healthSlider.maxValue = healthMax;
        healthSlider.value = health;
        healthText.text = "Health: " + Mathf.RoundToInt(health) + "/" + healthMax ;

        manaSlider.maxValue = manaMax;
        manaSlider.value = mana;
        manatext.text = "Mana: " + Mathf.RoundToInt(mana) + "/" + manaMax;
    }

    private void GetHurt()
    {
        if (Input.GetButton("Damage"))
        {
            health -= 3 * Time.deltaTime;
        }
        if(health < healthMax)
        {
            health += (healthRegen*0.1f) * Time.deltaTime;
        }
    }

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
