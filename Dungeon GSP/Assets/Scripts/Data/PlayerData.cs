using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool fireUpgrade;
    public bool electricUpgrade;
    public int gold;

    public PlayerData(playerVariables playerVariables)
    {
        fireUpgrade = playerVariables.fireUpgrade;
        electricUpgrade = playerVariables.electricUpgrade;
        gold = playerVariables.gold;
    }
}
