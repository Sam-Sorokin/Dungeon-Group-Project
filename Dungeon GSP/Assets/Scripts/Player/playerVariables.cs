using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVariables : MonoBehaviour
{
    private FireAttacks fireAttacks;
    private PlayerWallet wallet;

    public bool fireUpgrade;
    public bool electricUpgrade;
    public int gold;

    void Start()
    {
        fireAttacks = GetComponent<FireAttacks>();
        if (fireAttacks != null)
            fireAttacks.altUpgrade = fireUpgrade;

        wallet = GetComponent<PlayerWallet>();
        if (wallet != null)
            wallet.currentGold = gold;
    }

    public void save()
    {
        if (fireAttacks != null)
            fireUpgrade = fireAttacks.altUpgrade;
        if (wallet != null)
            gold = wallet.currentGold;
    }
}
