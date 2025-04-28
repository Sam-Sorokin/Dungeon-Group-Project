using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBase : Damagable
{
    private Renderer[] renderers; // Store all renderers
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        duration = Time.time + 0.2f;
        StartCoroutine(damageIndication());
    }

    public virtual void Attack()
    {

    }

    IEnumerator damageIndication()
    {
        List<Color> originalColors = new List<Color>();

        // Store original colors
        foreach (Renderer r in renderers)
        {
            originalColors.Add(r.material.color);
            r.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.2f);

        // Reset colors
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = originalColors[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Death();
        }
    }
}
