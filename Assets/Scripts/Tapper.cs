using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapper : Upgradable
{
    public ParticleSystem tappedParticleSystem;
    private void Awake()
    {
        currentLevel = 1;
    }
    new private void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Tapped", CubeTapped);
    }
    new private void OnDisable()
    {
        base.OnDisable();
        EventManager.StopListening("Tapped", CubeTapped);
    }
    void CubeTapped()
    {
        //Play Tapped Effect
        PlayerDataManager.AddVerticies(goldPerTap);
    }
}
