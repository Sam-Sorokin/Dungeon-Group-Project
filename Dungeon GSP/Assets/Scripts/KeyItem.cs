using UnityEngine;

public class KeyItem : MonoBehaviour
{
    [Tooltip("Unique identifier for this key (e.g., 'RedKey', 'BasementKey', 'Key_01')")]
    public string keyID = "DefaultKey"; // Assign unique IDs in the Inspector for each key instance
    public GameObject keyPickupEffect;
    public KeyCode pickupKey = KeyCode.E;

    private bool playerInRange = false;
    // No longer need playerObject here if we use the PlayerInventory singleton

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(pickupKey))
        {
            PickupKey();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered range of key: " + keyID + ". Press " + pickupKey.ToString() + " to pick up.");
            // Optional: Show UI prompt
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited range of key: " + keyID);
            // Optional: Hide UI prompt
        }
    }

    void PickupKey()
    {
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.AddKey(keyID); // Add this specific key's ID to the inventory
            Debug.Log("Key Picked Up: " + keyID);

            if (keyPickupEffect != null)
            {
                Instantiate(keyPickupEffect, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false); // Disable the key object
        }
        else
        {
            Debug.LogError("PlayerInventory.Instance not found! Make sure the Player has the PlayerInventory script.");
        }
    }
}