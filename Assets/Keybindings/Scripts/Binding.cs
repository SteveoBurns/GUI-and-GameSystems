using UnityEngine;

using System;

[Serializable]
public class Binding
{
    public string Name { get { return name; } }
    public KeyCode Value { get { return value; } }
    public string ValueDisplay { get { return BindingUtils.TranslateKeycode(value); } }

    [SerializeField]
    private string name;
    [SerializeField]
    private KeyCode value;

    public Binding(string _name, KeyCode _defaultValue)
    {
        name = _name;
        value = _defaultValue;
    }

    /// <summary>
    /// Saves the keycode into playerprefs so that the binding is persistant
    /// between game sessions
    /// </summary>
    public void Save()
    {
        // Stores the value of this binding to the system under its name
        PlayerPrefs.SetInt(name, (int)value);
        // Actually saves all the values to the system
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Loads the stored value of this keybinding, if it is not set, it uses the default value.
    /// </summary>
    public void Load()
    {
        // Gets the int version of the keycode from player prefs and sets our value to it.
        value = (KeyCode)PlayerPrefs.GetInt(name, (int)value);
    }

    /// <summary>
    /// Rebinds the binding to the new keybinding and then saves to the player prefs.
    /// </summary>
    /// <param name="_new">The key the binding will now be bound to.</param>
    public void Rebind(KeyCode _new)
    {
        value = _new;
        Save();
    }

    /// <summary>
    /// Returns whether or not the key this binding is mapped to was pressed this frame.
    /// </summary>
    public bool Pressed()
    {
        return Input.GetKeyDown(value);
    }

    /// <summary>
    /// Returns whether or not the key this binding is mapped is being pressed this frame.
    /// </summary>
    public bool Held()
    {
        return Input.GetKey(value);
    }

    /// <summary>
    /// Returns whether or not the key this binding is mapped to was released this frame.
    /// </summary>
    public bool Released()
    {
        return Input.GetKeyUp(value);
    }
}
