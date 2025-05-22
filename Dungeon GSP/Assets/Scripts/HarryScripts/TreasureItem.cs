using UnityEngine;

public class TreasureItem : MonoBehaviour
{
    public string playerTag = "Player"; 
    public int goldAmount = 100;     

    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag(playerTag))
        {
            CollectTreasure(other.gameObject); 
        }
    }

    void CollectTreasure(GameObject player) 
    {
        Debug.Log(goldAmount + " gold automatically collected by " + player.name + "!");

        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.AddGold(goldAmount);
        }
        else
        {
            Debug.LogWarning("PlayerWallet instance not found. Cannot add gold.");
        }

        // Destroy the treasure GameObject after it's collected
        Destroy(gameObject);
    }
}