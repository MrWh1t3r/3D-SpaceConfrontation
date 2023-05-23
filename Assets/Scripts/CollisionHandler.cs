using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1f;
    [SerializeField] private GameObject explosion;
    
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyWithDelay());
    }

    private IEnumerator DestroyWithDelay()
    {
        GetComponent<PlayerControls>().enabled = false;
        explosion.GetComponent<AudioSource>().Play();
        explosion.GetComponent<ParticleSystem>().Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(loadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        yield return 0;
    }
}
