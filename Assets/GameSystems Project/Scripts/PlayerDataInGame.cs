using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This handles the save/load for in game information.
/// </summary>
[System.Serializable]
public class PlayerDataInGame
{
    public static PlayerDataInGame ThePlayerDataInGame;

    public string name;
    public int level;
    public int classIndex;
    public int raceIndex;
    public string raceName;

    public int health;
    public int healthRegen;
    public int manaMax;
    public int manaRegen;
    public int stamina;
    public int speed;
    

    
    public int[] visual;

    /// <summary>
    /// The data that is going to be saved pulls from ...
    /// </summary>
    /// <param name="player"></param>
    public PlayerDataInGame(PlayerStats player, Movement movement,CustomisationGet data)
    {

        name = player.characterName;
        level = player.levelInt;
        classIndex = player.classIndex;
        raceIndex = player.raceIndex;
        raceName = player.raceName;

        health = player.healthMax;
        healthRegen = player.healthRegen;
        manaMax = player.manaMax;
        manaRegen = player.manaRegen;

        stamina = movement.staminaMax;
        speed = movement.baseSpeed * 2;

        
        visual = new int[6];
        visual[0] = data.visual[0];
        visual[1] = data.visual[1];
        visual[2] = data.visual[2];
        visual[3] = data.visual[3];
        visual[4] = data.visual[4];
        visual[5] = data.visual[5];

        ThePlayerDataInGame = this;
    }


}
/*Journal
 * Man.. This took a while to get working.
 * After a couple of iterations I finally changed it to get the data from stats, moevment and customisation get, which all work.
 * before I had it taking the visual data from player data, but that didnt work if you were loading into the game and not going through the 
 * customisation screen as it got its data from customisation set. The work around/ probably better way to do it was to store that data into variables in customisation get
 * then use that. That meant after the first load i didn't need to use playerdata at all.
 * 
 */