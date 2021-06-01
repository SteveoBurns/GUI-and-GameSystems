using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions
{
    public string factionName;
    [SerializeField, Range(-1,1)] float _approval;
    public float approval
    {
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _approval;
        }
    }
    public Factions(float initialApproval)
    {
        approval = initialApproval;
    }

}


public class FactionsManager : MonoBehaviour
{
    public static FactionsManager theManagerOfFactions;
    
    Dictionary<string, Factions> factions;

    [Header("Factions")]
    [SerializeField] public List<Factions> initialiseFactions = new List<Factions>();

    private void Awake()
    {
        if (theManagerOfFactions == null)
            theManagerOfFactions = this;        
        else
            Destroy(this);

        // Add all the factions into a dictionary
        factions = new Dictionary<string, Factions>();
        foreach (Factions faction in initialiseFactions)
        {
            factions.Add(faction.factionName, faction);
        }
    }

    public float? FactionsApproval(string factionName, float value)
    {
        if (factions.ContainsKey(factionName))
        {
            factions[factionName].approval += value;
            return factions[factionName].approval;
        }
        return null;
    }

    public float? FactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {            
            return factions[factionName].approval;
        }
        return null;
    }

    /*Example of adding to approval function
    void AddApproval()
    {
        
    }


    */
}
