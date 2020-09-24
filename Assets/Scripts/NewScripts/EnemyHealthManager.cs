using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    [SerializeField]
    private float _startingHealth;
    [SerializeField]
    private List<GameObject> _modelParts;
    [SerializeField]
    private float _scoreValue = 100.0f;
    [SerializeField]
    private GameObject deathVFX;

    private float _currentHealth;
    private bool _isAlive;


    private void Start()
    {
        _currentHealth = _startingHealth;
        _isAlive = true;
    }

    public void RemoveHealth(float amount)
    {
        if (_isAlive)
        {
            _currentHealth -= amount;
            StartCoroutine(LerpColor());

            if (_currentHealth <= 0)
            {
                OnDeath();
            }
        }
        else
        {
            OnDeath();
        }
    }

    private IEnumerator LerpColor()
    {
        float perc = ((_currentHealth / _startingHealth)) * 100f;

        Color changeToColor = Color.red;

        if (perc >= 75 && perc <= 100)
        {
            changeToColor = Color.green;
        }
        else if (perc >= 50 && perc < 75)
        {
            changeToColor = Color.yellow;
        }
        else if (perc >= 25 && perc < 50)
        {
            changeToColor = new Color(1, 0.647f, 0);
        }
        else if (perc >= 0 && perc < 25)
        {
            changeToColor = Color.red;
        }

        foreach (GameObject part in _modelParts)
        {
            Material[] hpMats = part.GetComponent<Renderer>().materials;
            foreach (Material mat in hpMats)
            {
                mat.SetColor("_BaseColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, (perc / 100f)));
                mat.SetColor("_EmissionColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, 1));
            }
        }
        yield return new WaitForSeconds(0.2f);
    }

    private void OnDeath()
    {
        _isAlive = false;
        EnemyManager.Instance.RemoveEnemy(gameObject);
        if (deathVFX != null)
        {
            Instantiate(deathVFX, transform.position, transform.rotation);
        }
        GameManager.Instance.IncreaseScore(_scoreValue);
        ItemDropper.Instance.EnemyDropChance(gameObject);
        Destroy(gameObject);
    }
}