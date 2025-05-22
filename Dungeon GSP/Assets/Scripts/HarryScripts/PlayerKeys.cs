using UnityEngine;
using System.Collections.Generic; // Required for using Lists or HashSets

public class PlayerInventory : MonoBehaviour
{
    // Singleton pattern: A way to have a single, globally accessible instance of this class.
    public static PlayerInventory Instance { get; private set; }

    // Using a HashSet for efficient checking if a key exists.
    // Stores the unique IDs of the keys the player has collected.
    private HashSet<string> collectedKeyIDs = new HashSet<string>();

    void Awake()
    {
        // Implement the singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); // Optional: if your player persists between scenes
        }
    }

    public void AddKey(string keyID)
    {
        if (!string.IsNullOrEmpty(keyID))
        {
            collectedKeyIDs.Add(keyID);
            Debug.Log("Added key to inventory: " + keyID);
        }
    }

    public bool HasKey(string keyID)
    {
        if (string.IsNullOrEmpty(keyID)) // If no specific key is required, consider it as having the key
        {
            Debug.LogWarning("HasKey called with null or empty keyID. This might indicate an unassigned requiredKeyID on a door/trigger. Assuming key is not required.");
            return true; // Or false, depending on desired behavior for unassigned requiredKeyID
        }
        return collectedKeyIDs.Contains(keyID);
    }

    public bool HasAnyKey() // Optional: if some triggers just need *a* key, not a specific one
    {
        return collectedKeyIDs.Count > 0;
    }
}