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
        // Store all original colors for every material
        List<Color> originalColors = new List<Color>();

        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                originalColors.Add(mat.color);
                mat.color = new Color(1f,0f,0f,1f);
            }
        }

        yield return new WaitForSeconds(0.2f);

        int colorIndex = 0;
        foreach (Renderer r in renderers)
        {
            foreach (Material mat in r.materials)
            {
                mat.color = originalColors[colorIndex];
                colorIndex++;
            }
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
