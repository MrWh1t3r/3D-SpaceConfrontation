using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destroyDelay = 2f;

    private void Start()
    {
        StartCoroutine(StartDestruct());
    }

    private IEnumerator StartDestruct()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
        yield return 0;
    }
}
