using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions
{
    public string factionname;
    float _approval;
    public float Approval
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
}


public class FactionsManager : MonoBehaviour
{
    public static FactionsManager theManagerOfFactions;
    
    Dictionary<string, Factions> factions;

    private void Awake()
    {
        if (theManagerOfFactions == null)
            theManagerOfFactions = this;        
        else
            Destroy(this);

        factions = new Dictionary<string, Factions>();
        factions.Add("VampiresThralls", new Factions());
    }

    public float? FactionsApproval(string factionName, float value)
    {
        if (factions.ContainsKey(factionName))
        {
            factions[factionName].Approval += value;
            return factions[factionName].Approval;
        }
        return null;
    }

    public float? getFactionsApproval(string factionName)
    {
        if (factions.ContainsKey(factionName))
        {            
            return factions[factionName].Approval;
        }
        return null;
    }
}
