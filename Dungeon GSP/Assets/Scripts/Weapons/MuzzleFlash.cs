using System.Collections;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    MeshRenderer m_Renderer;

    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Renderer.enabled = false;
    }

    public void doFlash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        m_Renderer.enabled = true;
        yield return new WaitForSeconds(0.1f);
        m_Renderer.enabled = false;
    }
}