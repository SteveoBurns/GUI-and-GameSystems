using UnityEngine;
using UnityEngine.UI;

public class FactionsUI : MonoBehaviour
{
    [SerializeField] private Text vampiresApprovalText;
    [SerializeField] private Text humansApprovalText;
    private float vampiresApproval;
    private float humansApproval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vampiresApproval = (float) FactionsManager.theManagerOfFactions.FactionsApproval("Vampires");
        vampiresApprovalText.text = "Vampires Faction Approval: " + vampiresApproval.ToString();

        humansApproval = (float) FactionsManager.theManagerOfFactions.FactionsApproval("Humans");
        humansApprovalText.text = "Humans Faction Approval: " + humansApproval.ToString();

    }
}
