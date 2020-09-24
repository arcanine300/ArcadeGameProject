using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool Enabled { get; private set; }

    public List<GameObject> Effects;

    public Animator Animator;

    public CapsuleCollider Collider;

    private void Start()
    {
        DisableEffects();
        Enabled = false;
    }

    IEnumerator Raise()
    {
        Animator.Play("Spawner-Raise");
        yield return new WaitForSeconds(1);
        EnableEffects();
        Enabled = true;
        Collider.enabled = true;
    }

    IEnumerator Lower()
    {
        Collider.enabled = false;
        DisableEffects();        
        yield return new WaitForSeconds(0.1f);
        Animator.Play("Spawner-Lower");
        Enabled = false;
    }

    public void Enable()
    {
        StartCoroutine(Raise());
    }

    public void Disable()
    {
        StartCoroutine(Lower());
    }

    private void EnableEffects()
    {
        foreach (var effect in Effects)
        {
            effect.SetActive(true);
        }
    }

    private void DisableEffects()
    {
        foreach (var effect in Effects)
        {
            effect.SetActive(false);
        }
    }
}
