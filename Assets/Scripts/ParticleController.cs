using System;
using System.Collections;
using System.Collections.Generic;
using JUtils;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField, Required] private List<ParticleSystem> _particleSystems;
    [SerializeField] private bool _isTrigger = false; 
    private float remainingDuration = 0.0f;

    public void Play()
    {
        foreach(ParticleSystem ps in _particleSystems)
        {
            ps.Play();
        }
    }
    
    public void Stop()
    {
        foreach(ParticleSystem ps in _particleSystems)
        {
            ps.Stop();
        }
    }

    [Button]
    public void TriggerVFX(float duration = 3.0f)
    {
        if (!_isTrigger)
        {
            remainingDuration = duration;
            foreach(ParticleSystem ps in _particleSystems)
            {
                ps.Play();
            }
        }
        else
        {
            foreach (ParticleSystem ps in _particleSystems)
            {
                ps.Play();
            }
        }
        
    }

    private void Awake()
    {
        Stop();
    }

    private void Update()
    {
        if (remainingDuration > 0 && !_isTrigger)
        {
            remainingDuration -= Time.deltaTime;
            if (remainingDuration <= 0)
            {
                foreach(ParticleSystem ps in _particleSystems)
                {
                    ps.Stop();
                }
            }
        }
    }
}
