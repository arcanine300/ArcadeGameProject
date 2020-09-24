using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerHealth : MonoBehaviour
{
    public int Lives = 1;

    public float Health;

    public float BaseHealth = 100;

    public bool IsAlive;

    public bool CanRespawn;

    public GameObject ReviveBlast;
    public float blastDuration = 0.15f;

    public float shakeMagnitude = 1.25f;
    public float shakeDuration = 0.2f;

    private Camera theCam;
    private Vector3 originPos;

    public List<GameObject> ModelParts;

    Color Red = Color.red;
    public Material DefaultMaterial;
    public float lerpTime = 0.2f;

    private Volume postProVolume;
    private ColorAdjustments colorCorrection;
    private ChromaticAberration distortion;
    private Vignette hitNotification;

    private bool isGodMode = false;

    private void Start()
    {
        Health = BaseHealth;
        CanRespawn = false;
        theCam = GetComponentInChildren<Camera>();
        originPos = theCam.transform.localPosition;
        postProVolume = GetComponentInChildren<Volume>();
        postProVolume.profile.TryGet(out colorCorrection);
        postProVolume.profile.TryGet(out distortion);
        postProVolume.profile.TryGet(out hitNotification);
    }

    private void Update()
    {
        if (colorCorrection && distortion)
        {
            if (Health < BaseHealth / 2)
            {
                colorCorrection.saturation.Override(-50 + Health);
                distortion.intensity.Override(1 - ((Health / 100) + (float)0.25));
            }
            else
            {
                colorCorrection.saturation.Override(0);
                distortion.intensity.Override((float)0.11);
            }
        }
    }

    public void AddLife()
    {
        Lives++;
    }

    public void RemoveHealth(float amount)
    {
        if (IsAlive)
        {
            if (!isGodMode)
            {                
                Health -= amount;
                StartCoroutine(ShakeScreen((shakeDuration / 2), (shakeMagnitude / 2)));
                if (Health <= 0)
                {
                    IsAlive = false;
                    Lives--;
                    GameManager.Instance.KillStreak = 0;
                    GameManager.Instance.RemoveMultiplier();
                    if (Lives <= 0)
                    {
                        EndGame();
                    }
                    else
                    {
                        Health = BaseHealth;
                        StartCoroutine(BlastRemoveDelay());
                        StartCoroutine(deathEffect());
                        foreach (GameObject part in ModelParts)
                        {
                            part.GetComponent<Renderer>().material = DefaultMaterial;
                        }
                    }
                }
                else
                {
                    StartCoroutine(lerpColor());
                }
                StartCoroutine(iFrames());
            }
        }
    }

    IEnumerator iFrames()
    {
        isGodMode = true;
        yield return new WaitForSeconds(0.35f);
        isGodMode = false;
    }

    IEnumerator lerpColor()
    {
        float perc = ((Health / BaseHealth)) * 100f;

        Color changeToColor = Color.red;

        if (perc >= 75 && perc <= 100)
        {
            changeToColor = Color.green;
        }
        else if (perc >= 50 && perc < 75)
        {
            changeToColor = Color.yellow;
        }
        else if (perc >= 33 && perc < 50)
        {
            changeToColor = new Color(1, 0.647f, 0);
        }
        else if (perc >= 0 && perc < 33)
        {
            changeToColor = Color.red;
        }

        foreach (GameObject part in ModelParts)
        {
            part.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, (perc / 100f)));
            part.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(part.GetComponent<Renderer>().material.color, changeToColor, 1));
        }

        //enemyMat.material.SetColor("_BaseColor", Color.Lerp(enemyMat.material.color, Red, (perc / 100f)));
        //enemyMat.material.SetColor("_EmissionColor", Color.Lerp(enemyMat.material.color, Red, (perc / 100f)));
        yield return new WaitForSeconds(lerpTime);
    }

    IEnumerator BlastRemoveDelay()
    {
        //Debug.Log("Active");
        ReviveBlast.SetActive(true);
        yield return new WaitForSeconds(blastDuration);
        ReviveBlast.SetActive(false);
        IsAlive = true;
    }

    private void EndGame()
    {
        foreach (GameObject part in ModelParts)
        {
            part.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.Lerp(part.GetComponent<Renderer>().material.color, Color.red, 1));
            part.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(part.GetComponent<Renderer>().material.color, Color.red, 1));
        }

        GameManager.Instance.EndGame();
    }

    public void Kill()
    {
        if (IsAlive)
        {
            if (!isGodMode)
            {
                IsAlive = false;
                Lives--;
                GameManager.Instance.KillStreak = 0;
                GameManager.Instance.RemoveMultiplier();
                if (Lives <= 0)
                {
                    EndGame();
                }
                else
                {
                    StartCoroutine(BlastRemoveDelay());
                    Health = BaseHealth;
                    StartCoroutine(ShakeScreen(shakeDuration, shakeMagnitude));
                    StartCoroutine(deathEffect());
                    StartCoroutine(lerpColor());
                }
                StartCoroutine(iFrames());
            }
        }
    }

    IEnumerator deathEffect()
    {
        float timeElapsed = 0f;
        float duration = 0.25f;
        float intensity = 0f;
        while (timeElapsed < duration)
        {
            if (Time.timeScale != 0f)
            {
                timeElapsed += Time.deltaTime;
                if (intensity <= 0.6f) { intensity += Mathf.Lerp(0f, 0.6f, 0.1f); }
                hitNotification.intensity.Override(intensity);
                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                break;
            }
        }

        //yield return new WaitForSeconds(0.2f);
        timeElapsed = 0f;
        duration = 0.25f;

        while (timeElapsed < duration)
        {
            if (Time.timeScale != 0f)
            {
                timeElapsed += Time.deltaTime;
                intensity -= Mathf.Lerp(0f, 0.6f, 0.1f);
                hitNotification.intensity.Override(intensity);
                yield return new WaitForSeconds(0.005f);
            }
            else
            {
                break;
            }
        }

        yield return null;     
    }

    
    public IEnumerator ShakeScreen(float duration, float magnitude)
    {
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            //Debug.Log((theCam.transform.localPosition.z - 1f));
            float z = Random.Range((theCam.transform.localPosition.z - magnitude), (theCam.transform.localPosition.z + magnitude));
            theCam.transform.localPosition = new Vector3(x, originPos.y, z);
            //if the game isnt paused, continue incremeting the timeElapsed variable
            if (Time.timeScale != 0f)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                //if the game is paused, stop shaking
                break;
            }

            yield return null; //before continuing to the next iteration of the while loop, wait until the next frame is drawn.
        }

        theCam.transform.localPosition = originPos; //after the screen shake is done, reset cam pos
    }
    
}
