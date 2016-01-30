using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    public Dictionary<string, KeyCode> Inputs = new Dictionary<string, KeyCode>();
    
    // The current instance of the Input manager
    public static InputManager Instance = null;
   
    [System.Serializable]
    public struct Keys
    {
        public string KeyName;
        public KeyCode Code;
    };

    [SerializeField]
    public Keys[] KeysToAdd;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        foreach(Keys key in KeysToAdd)
        {
            Inputs.Add(key.KeyName, key.Code);
        }
        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// REturns true if the key was pressed
    /// </summary>
    /// <param name="KeyName">The name of the key we are looking for</param>
    /// <returns></returns>
    public bool GetKeyDown(string KeyName)
    {
        KeyCode input;
        Inputs.TryGetValue(KeyName, out input);
        if (Input.GetKeyDown(input))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// REturns true if the key was pressed
    /// </summary>
    /// <param name="KeyName">The name of the key we are looking for</param>
    /// <returns></returns>
    public bool GetKey(string KeyName)
    {
        KeyCode input;
        Inputs.TryGetValue(KeyName, out input);
        if (Input.GetKey(input))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// REturns true if the key was pressed
    /// </summary>
    /// <param name="KeyName">The name of the key we are looking for</param>
    /// <returns></returns>
    public bool GetKeyUp(string KeyName)
    {
        KeyCode input;
        Inputs.TryGetValue(KeyName, out input);
        if (Input.GetKeyUp(input))
        {
            return true;
        }

        return false;
    }
}
