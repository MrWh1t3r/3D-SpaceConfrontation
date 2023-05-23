using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int scorePerHit = 20;
    [SerializeField] private int scoreForKill = 200;
    [SerializeField] private int health = 5;
    
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject hitVFX;

    private Transform _parent;
    private ScoreBoard _scoreBoard;

    private void Start()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        _parent = GameObject.FindWithTag("SpawnAtRuntime").transform;
    }

    private void OnParticleCollision(GameObject other)
    { 
        ProcessHit();
    }
   
    private void ProcessHit()
    {
        var vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = _parent;
        _scoreBoard.IncreaseScore(scorePerHit);
        health--;
        if (health <= 0)
            KillEnemy();
    }
    
    private void KillEnemy()
    {
        _scoreBoard.IncreaseScore(scoreForKill);
        var fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = _parent;
        Destroy(gameObject);
    }
}
