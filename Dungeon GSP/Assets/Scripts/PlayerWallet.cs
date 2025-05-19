using UnityEngine;
using UnityEngine.UI; 

public class PlayerWallet : MonoBehaviour
{
    public static PlayerWallet Instance { get; private set; }

    public int currentGold = 0;
    
    public Text goldTextDisplay;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateGoldDisplay();
    }

    public void AddGold(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot add negative gold. Use SpendGold for that.");
            return;
        }
        currentGold += amount;
        Debug.Log(amount + " gold added. Current Gold: " + currentGold);
        UpdateGoldDisplay();
    }

    public bool SpendGold(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("Cannot spend negative gold.");
            return false;
        }
        if (currentGold >= amount)
        {
            currentGold -= amount;
            Debug.Log(amount + " gold spent. Current Gold: " + currentGold);
            UpdateGoldDisplay();
            return true; 
        }
        else
        {
            Debug.Log("Not enough gold to spend " + amount + ". Current Gold: " + currentGold);
            return false; 
        }
    }

    void UpdateGoldDisplay()
    {
        if (goldTextDisplay != null)
        {
            goldTextDisplay.text = "Gold: " + currentGold;
        }
    }

    
    public int GetCurrentGold()
    {
        return currentGold;
    }
}