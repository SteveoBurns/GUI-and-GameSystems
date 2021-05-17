using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataInGame
{
    public string name;
    public int classIndex;
    public int raceIndex;
    public string raceName;

    public int[] stats;
    public int[] visual;

    /// <summary>
    /// The data that is going to be saved pulls from ...
    /// </summary>
    /// <param name="player"></param>
    public PlayerDataInGame(PlayerStats player, PlayerData data)
    {

        name = player.characterName;
        classIndex = player.classIndex;
        raceIndex = player.raceIndex;
        raceName = player.raceName;

        stats = new int[6];
        stats[0] = data.stats[0];
        stats[1] = data.stats[1];
        stats[2] = data.stats[2];
        stats[3] = data.stats[3];
        stats[4] = data.stats[4];
        stats[5] = data.stats[5];

        visual = new int[6];
        visual[0] = data.visual[0];
        visual[1] = data.visual[1];
        visual[2] = data.visual[2];
        visual[3] = data.visual[3];
        visual[4] = data.visual[4];
        visual[5] = data.visual[5];

    }


}
