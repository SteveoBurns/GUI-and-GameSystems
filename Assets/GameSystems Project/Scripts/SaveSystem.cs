using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /// <summary>
    /// The save player function that can be called to save the data in PlayerData. Used after customisation scene & New Game.
    /// </summary>    
    public static void SavePlayer(CustomisationSet player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// The load player function that loads PlayerData. Used after customisation scene & New Game
    /// </summary>
    /// <returns>PlayerData</returns>
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data =  formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            
            return data;

        }
        else
        {
            Debug.LogError("save file not found in" + path);
            return null;
        }
    }


    /// <summary>
    /// This Loads the data from SavePlayerInGame. This is used from the Continue and Load Game buttons from main menu.
    /// </summary>
    /// <returns>PlayerDataInGame</returns>
    public static PlayerDataInGame LoadPlayerInGame()
    {
        string path = Application.persistentDataPath + "/playerInGame.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerDataInGame data = formatter.Deserialize(stream) as PlayerDataInGame;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("save file not found in" + path);
            return null;
        }
    }

    /// <summary>
    /// This saves the updated data when saving In-Game.
    /// </summary>
    /// <param name="player">PlayerStats</param>
    /// <param name="movement">Movement</param>
    /// <param name="_data">CustomisationGet</param>
    public static void SavePlayerInGame(PlayerStats player, Movement movement ,CustomisationGet _data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerInGame.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerDataInGame data = new PlayerDataInGame(player, movement, _data);


        formatter.Serialize(stream, data);
        stream.Close();
    }

}
