

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingManager : MonoBehaviour
{
    /// <summary>
    /// Attempts to retrieve the pressed state of the passed key.
    /// </summary>
    /// <param name="_key">The binding we are trying to check.</param>
    /// <returns>Whether or not the binding is pressed, if the key doesn't exist, returns false</returns>
    public static bool BindingPressed(string _key)
    {
        // Attempt to retrieve the binding
        Binding binding = GetBinding(_key);

        if(binding != null)
        {
            // We got the binding so get its pressed state
            return binding.Pressed();
        }

        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the passed key: " + _key);
        return false;
    }

    /// <summary>
    /// Attempts to retrieve the held state of the passed key.
    /// </summary>
    /// <param name="_key">The binding we are trying to check.</param>
    /// <returns>Whether or not the binding is pressed, if the key doesn't exist, returns false</returns>
    public static bool BindingHeld(string _key)
    {
        // Attempt to retrieve the binding
        Binding binding = GetBinding(_key);

        if(binding != null)
        {
            // We got the binding so get its pressed state
            return binding.Held();
        }

        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the passed key: " + _key);
        return false;
    }

    /// <summary>
    /// Attempts to retrieve the pressed state of the passed key.
    /// </summary>
    /// <param name="_key">The binding we are trying to check.</param>
    /// <returns>Whether or not the binding is pressed, if the key doesn't exist, returns false</returns>
    public static bool BindingReleased(string _key)
    {
        // Attempt to retrieve the binding
        Binding binding = GetBinding(_key);

        if(binding != null)
        {
            // We got the binding so get its pressed state
            return binding.Released();
        }

        // No binding matches the passed key so log a message and return false
        Debug.LogWarning("No binding matches the passed key: " + _key);
        return false;
    }

    /// <summary>
    /// Updates the respective bindings value to the newly passed one.
    /// </summary>
    /// <param name="_name">The binding we are trying to remap</param>
    /// <param name="_value">The new value of the binding</param>
    public static void Rebind(string _name, KeyCode _value)
    {
        // Attempt to get the corresponding binding
        Binding binding = GetBinding(_name);

        if(binding != null)
        {
            // We retrieved it so rebind the key.
            binding.Rebind(_value);
        }
    }

    /// <summary>
    /// Gets all the bindings in the bindingmanager
    /// </summary>
    public static List<Binding> GetBindings()
    {
        return instance.bindingsList;
    }

    /// <summary>
    /// Attempts to get the corresponding binding from the system.
    /// </summary>
    /// <param name="_key">The key of the binding we are trying to get.</param>
    /// <returns>Returns the found binding if it exists, otherwise null.</returns>
    public static Binding GetBinding(string _key)
    {
        // First we see if the binding exists in the system.
        if(instance.bindingsMap.ContainsKey(_key))
        {
            // It does so return it.
            return instance.bindingsMap[_key];
        }

        // No binding matched this key so return null.
        return null;
    }

    // The singleton instance that will refer to the binding manager
    private static BindingManager instance = null;

    // Used to actually access the bindings by their names when handling input
    private Dictionary<string, Binding> bindingsMap = new Dictionary<string, Binding>();

    // Contains all bindings for easy iteration over all over them
    private List<Binding> bindingsList = new List<Binding>();

    [SerializeField]
    private List<Binding> defaultBindings = new List<Binding>();

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // First check if the instance isn't already set
        if(instance == null)
        {
            // It isn't so make it this instance of BindingManager
            instance = this;
        }
        // The instance is already set, but is it this instance of the BindingManager?
        else if(instance != this)
        {
            // It isn't so destroy this GameObject and return
            Destroy(gameObject);
            return;
        }

        // Setup the bindings on this manager
        PopulateBindingDictionaries();
        LoadBindings();
    }

    /// <summary>
    /// Loads all the default bindings set in the inspector into the system's dictionaries and lists
    /// </summary>
    private void PopulateBindingDictionaries()
    {
        // Loop through all the bindings set in the inspector
        foreach(Binding binding in defaultBindings)
        {
            // If the bindingsMap already contains a binding with this name
            // ignore this binding
            if(bindingsMap.ContainsKey(binding.Name))
            {
                continue;
            }

            // This binding is new, so we will add it to the system
            bindingsMap.Add(binding.Name, binding);
            bindingsList.Add(binding);
        }
    }

    /// <summary>
    /// Loads the bindings data for each binding in the system
    /// </summary>
    private void LoadBindings()
    {
        foreach(Binding binding in bindingsList)
        {
            binding.Load();
        }
    }

    /// <summary>
    /// Saves the data of every binding in the bindings list
    /// </summary>
    public void SaveBindings()
    {
        foreach(Binding binding in bindingsList)
        {
            binding.Save();
        }
    }
}
