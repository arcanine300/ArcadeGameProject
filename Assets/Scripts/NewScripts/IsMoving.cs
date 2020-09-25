using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMoving : MonoBehaviour
{
    public bool Result { get; private set; }

    private Vector3 previousPosition = Vector3.zero;

    private void Update()
    {
        Result = (Vector3.Distance(transform.position, previousPosition) > 0);
        previousPosition = transform.position;
    }
}
