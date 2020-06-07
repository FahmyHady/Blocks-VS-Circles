using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tapper : Upgradable
{
    public ParticleSystem tappedParticleSystem;
    public void Init()
    {
        currentLevel = 1;
        CalculateValues();
    }
    new private void OnEnable()
    {
        base.OnEnable();
        EventManager.StartListening("Tapped", CubeTapped);
        EventManager.StartListening("Prestiged", PlayerPrestiged);

    }
    new private void OnDisable()
    {
        base.OnDisable();
        EventManager.StopListening("Tapped", CubeTapped);
        EventManager.StopListening("Prestiged", PlayerPrestiged);

    }
    void CubeTapped()
    {
        //Play Tapped Effect
        PlayerDataManager.AddVerticies(goldPerTap);
    }
    public override void PlayerPrestiged()
    {
        currentLevel = 1;
        CalculateValues();
    }
}
