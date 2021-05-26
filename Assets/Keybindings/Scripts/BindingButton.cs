using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class BindingButton : MonoBehaviour
{
    [SerializeField]
    private string bindingToMap;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI bindingName;

    private bool isRebinding = false;

    /// <summary>
    /// Sets up the rebinding button with the passed value.
    /// </summary>
    /// <param name="_toMap">The binding that requires mapping.</param>
    public void Setup(string _toMap)
    {
        bindingToMap = _toMap;

        // Automatically set the onclick function and change the name text to the binding
        button.onClick.AddListener(OnClick);
        bindingName.text = _toMap;

        // Update the button text with the binding's value and make the GO active
        BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Have we set the bindingToMap variable
        if(string.IsNullOrEmpty(bindingToMap))
        {
            // We haven't so turn this gameObject off
            gameObject.SetActive(false);
            return;
        }

        // We have actually set the binding so set this script up!
        Setup(bindingToMap);
    }

    // Update is called once per frame
    void Update()
    {
        // Are we rebinding the key?
        if(isRebinding)
        {
            // Try to get any key in the input and check if it was sucessful
            KeyCode pressed = BindingUtils.GetAnyPressedKey();
            if(pressed != KeyCode.None)
            {
                // Rebind the key and update the button text
                BindingManager.Rebind(bindingToMap, pressed);
                BindingUtils.UpdateTextWithBinding(bindingToMap, buttonText);

                // Reset the isRebinding flag as we have now rebound the key
                isRebinding = false;
            }
        }
    }

    private void OnClick()
    {
        isRebinding = true;
    }
}
