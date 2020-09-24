using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnowStormGimmick : MonoBehaviour, IMapGimmick
{
    public GameObject RegularSnowFall;
    public float SnowStormDuration = 5.0f;
    public float RegularSnowDuration = 5.0f;
    public float SlowPlayerSpeed = 3.0f;
    public int EmissionLow = 2500;
    public int EmissionHigh = 20000;
    public float StartTimeRegular = 1.7f;
    public float StartTimeStorm = 0.25f;
    public float StartSpeedRegular = 8;
    public float StartSpeedStorm = 50;

    public int SpawnSize = 200;
    public float LinearXVelocity = -20;
    public float OrbitVelocity = -0.2f;

    private bool _isSnowStorm = false;
    private bool _isActive = false;

    private float _defaultPlayerSpeed;

    private void Start()
    {
        RegularSnowFall.SetActive(true);
        _defaultPlayerSpeed = GameManager.Instance.Player.GetComponent<CharacterStats>().MoveSpeed;
    }

    private void Update()
    {
        if (!_isSnowStorm)
        {
            StartCoroutine(SnowFall());
        }
    }

    private IEnumerator SnowFall()
    {
        _isSnowStorm = true;
        StartCoroutine(FogOut());
        StartCoroutine(RegularSpeed());
        StartCoroutine(LessEmission());
        StartCoroutine(RegularSnowDirection());
        GameManager.Instance.Player.GetComponent<CharacterStats>().MoveSpeed = _defaultPlayerSpeed;
        yield return new WaitForSeconds(RegularSnowDuration);
        StartCoroutine(SnowStorm());
    }

    private IEnumerator SnowStorm()
    {
        StartCoroutine(FogIn());
        StartCoroutine(StormSpeed());
        StartCoroutine(MoreEmission());
        StartCoroutine(SnowStormDirection());
        GameManager.Instance.Player.GetComponent<CharacterStats>().MoveSpeed = SlowPlayerSpeed;
        yield return new WaitForSeconds(SnowStormDuration);
        _isSnowStorm = false;
    }

    private IEnumerator SnowStormDirection()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        var vel = ps.velocityOverLifetime;
        float xDir = 0;

        while (xDir >= LinearXVelocity)
        {
            vel.x = xDir;
            xDir -= 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator RegularSnowDirection()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        var vel = ps.velocityOverLifetime;
        float xDir = LinearXVelocity;

        while (xDir <= 0)
        {
            vel.x = xDir;
            xDir += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator StormSpeed()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        var main = ps.main;
        float time = StartSpeedRegular;

        while (time <= StartSpeedStorm)
        {
            main.startSpeed = time;
            time += 2;
            yield return new WaitForSeconds(0.005f);
        }
    }

    private IEnumerator RegularSpeed()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        var main = ps.main;
        float time = StartSpeedStorm;

        while (time >= StartSpeedRegular)
        {
            main.startSpeed = time;
            time -= 2;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator MoreEmission()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        int low = EmissionLow;
        var emission = ps.emission;
        var main = ps.main;

        main.startLifetime = StartTimeStorm;
        while (low <= EmissionHigh)
        {
            emission.rateOverTime = low;
            low += SpawnSize;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator LessEmission()
    {
        ParticleSystem ps = RegularSnowFall.GetComponent<ParticleSystem>();
        int high = EmissionHigh;
        var emission = ps.emission;
        var main = ps.main;

        main.startLifetime = StartTimeRegular;
        while (high >= EmissionLow)
        {
            emission.rateOverTime = high;
            high -= SpawnSize;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator FogIn()
    {
        float fogMax = 0.045f;
        RenderSettings.fogDensity = 0.0f;
        RenderSettings.fog = true;

        while (RenderSettings.fogDensity <= fogMax)
        {
            RenderSettings.fogDensity += 0.0025f;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FogOut()
    {
        while (RenderSettings.fogDensity >= 0)
        {
            RenderSettings.fogDensity -= 0.0025f;
            yield return new WaitForEndOfFrame();
        }
        RenderSettings.fog = false;
    }

    public void Begin()
    {
        _isActive = true;
    }

    public void Stop()
    {
        _isActive = false;
    }

    public void UseItem()
    {
    }
}
