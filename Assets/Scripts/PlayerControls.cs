using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up and down")] [SerializeField] private float controlSpeed = 15f;
    [Tooltip("the x-axis boundary where the ship can fly")] [SerializeField] private float xRange = 10f;
    [Tooltip("the y-axis boundary where the ship can fly")] [SerializeField] private float yRange = 7f;

    [Header("Screen position base tuning")]
    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float positionYawFactor = 2f;
    
    [Header("Player input base tuning")]
    [SerializeField] private float controlRollFactor = -10f;
    [SerializeField] private float controlPitchFactor = -10f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] private GameObject[] lasers;

    private float _xThrow, _yThrow;


    private void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        _xThrow = Input.GetAxis("Horizontal");
        _yThrow = Input.GetAxis("Vertical");

        var xOffset = _xThrow * Time.deltaTime * controlSpeed;
        var yOffset = _yThrow * Time.deltaTime * controlSpeed;

        var rawXPos = transform.localPosition.x + xOffset;
        var rawYPos = transform.localPosition.y + yOffset;

        var clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        var clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        var pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        var pitchDueToControlThrow = _yThrow * controlPitchFactor;

        var yawDueToPosition = transform.localPosition.x * positionYawFactor;

        var rollDueToControlThrow = _xThrow * controlRollFactor;

        var pitch = pitchDueToPosition + pitchDueToControlThrow;
        var yaw = yawDueToPosition;
        var roll = rollDueToControlThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessFiring()
    {
        if (Input.GetButton("Fire1"))
            SetActiveLaser(true);
        else
            SetActiveLaser(false);
    }

    private void SetActiveLaser(bool isActive)
    {
        foreach (var laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
