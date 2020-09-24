using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    //public float CurrentHealth { get; private set; }

    //public bool IsAlive { get; private set; }

    //public float StartingHealth = 10.0f;

    //public float Score = 10.0f;

    //public GameObject deathEffect;

    //public List<GameObject> ModelParts;

    //Color Red = Color.red;
    //public Renderer enemyMat;
    //public float lerpTime = 0.2f;

    //private void Start()
    //{
    //    IsAlive = true;
    //    CurrentHealth = StartingHealth;
    //    enemyMat = GetComponent<Renderer>();
    //}

    //public void DepleteAllHealth(CharacterStats playerStats, bool isDeath = true)
    //{
    //    if (IsAlive)
    //    {
    //        CurrentHealth = 0;
    //        if (CurrentHealth <= 0)
    //        {
    //            if (!isDeath)
    //            {
    //                playerStats.AddScore(Score);
    //            }

    //            if (deathEffect)
    //            {
    //                Instantiate(deathEffect, transform.position, Quaternion.identity);
    //            }
    //            //GameManager.Instance.EnemyManager.KillEnemy(gameObject);
    //            IsAlive = false;
    //        }
    //    }
    //}

    //public void RemoveHealth(float amount, CharacterStats playerStats)
    //{
    //    if (IsAlive)
    //    {
    //        CurrentHealth -= amount;
    //        //enemyMat.material.SetColor("1", Red);// = Color.Lerp(enemyMat.material.color, Red, (1 / 100f));

    //        if (CurrentHealth <= 0)
    //        {
    //            if (deathEffect)
    //            {
    //                Instantiate(deathEffect, transform.position, Quaternion.identity);
    //            }
    //            playerStats.AddScore(Score);
    //            //GameManager.Instance.EnemyManager.KillEnemy(gameObject);
    //            IsAlive = false;
    //        }
    //        else
    //        {
    //            StartCoroutine(lerpColor());
    //        }
    //    }
    //}

    //IEnumerator lerpColor()
    //{
    //    float perc = ((CurrentHealth / StartingHealth)) * 100f;

    //    Color changeToColor = Color.red;

    //    if (perc >= 75 && perc <= 100)
    //    {
    //        changeToColor = Color.green;
    //    }
    //    else if (perc >= 50 && perc < 75)
    //    {
    //        changeToColor = Color.yellow;
    //    }
    //    else if (perc >= 25 && perc < 50)
    //    {
    //        changeToColor = new Color(1, 0.647f, 0);
    //    }
    //    else if (perc >= 0 && perc < 25)
    //    {
    //        changeToColor = Color.red;
    //    }

    //    foreach (GameObject part in ModelParts)
    //    {
    //        Material[] hpMats = part.GetComponent<Renderer>().materials;
    //        foreach(Material mat in hpMats)
    //        {
    //            mat.SetColor("_BaseColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, (perc / 100f)));
    //            mat.SetColor("_EmissionColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, 1));
    //        }
    //    }

    //    //enemyMat.material.SetColor("_BaseColor", Color.Lerp(enemyMat.material.color, Red, (perc / 100f)));
    //    //enemyMat.material.SetColor("_EmissionColor", Color.Lerp(enemyMat.material.color, Red, (perc / 100f)));
    //    yield return new WaitForSeconds(lerpTime);
    //}
}
