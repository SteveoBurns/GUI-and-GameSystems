using UnityEngine;

[System.Serializable]
public class LineOfDialogue 
{
    [TextArea(3,6)]
    public string topic, response;
    
    public float minApproval = -1f;
    public float changeApproval = 0f;


    public Dialogue nextDialogue;

}
