using UnityEngine;

[System.Serializable]
public class LineOfDialogue 
{
    [TextArea(3,6)]
    public string topic, response;
    public int minIntel;

}
