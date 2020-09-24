using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour, IMapGimmick
{
    public List<GameObject> Lasers;

    public float ActiveTime = 4.0f;

    public int AmountActiveAtATime = 1;

    public float ActiveTimeDelta = 2.0f;

    public bool IsActive;

    private bool _inProgress;

    private void Start()
    {
        _inProgress = false;
    }

    public void Begin()
    {
        Debug.Log("laser start");
        IsActive = true;
    }

    public void Stop()
    {
        Debug.Log("laser end");
        IsActive = false;

        foreach (var laser in Lasers)
        {
            laser.GetComponent<LaserBeam>().Disable();
        }
    }

    public void UseItem()
    {
        Debug.Log("laser start");
    }

    // some real ghetto code needs to be reworked. like it works but its ugly.
    private List<GameObject> GetRandomLasers()
    {
        List<int> nums = new List<int>();
        List<GameObject> lasers = new List<GameObject>();

        for (int i = 0; i < AmountActiveAtATime; i++)
        {            
            int val = Random.Range(0, Lasers.Count);
            if (!nums.Contains(val))
            {
                nums.Add(val);
                lasers.Add(Lasers[val]);
            }
            else
            {
                i--;
            }
        }

        return lasers;
    }

    private IEnumerator ActivateLasersFor(List<GameObject> lasers)
    {
        float time = ActiveTime + Random.Range(-ActiveTimeDelta, ActiveTimeDelta + 1);
        _inProgress = true;
        foreach (var laser in lasers)
        {
            laser.GetComponent<LaserBeam>().ActivateFor(time);
        }
        yield return new WaitForSeconds(time + 1f + 0.75f);
        _inProgress = false;
    }

    private void Update()
    {
        if (IsActive)
        {
            if (!_inProgress)
            {
                StartCoroutine(ActivateLasersFor(GetRandomLasers()));
            }
        }
    }
}
