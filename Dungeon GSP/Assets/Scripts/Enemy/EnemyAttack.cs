using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int attackTime = 5;
    public bool attack = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(attackPlayer());
    }

    IEnumerator attackPlayer()
    {
        while(true)
        {
            // need an if statement to check if in range for attack...
            bool attack = true;
            yield return null;
            attack = false;
            yield return new WaitForSeconds(attackTime);
        }
    }
}
