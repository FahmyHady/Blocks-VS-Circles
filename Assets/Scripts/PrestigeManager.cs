using Boo.Lang.Environments;
using PE2D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PrestigeManager : MonoBehaviour
{
    public GameObject blackHole;
    public static ParticleSpawner circleExplosionSpawner;
    double prestigeCost;
    private void OnEnable()
    {
        EventManager.StartListening("Cube Is Disappearing", BlackHoleIsAppearing);
        EventManager.StartListening("Data Loaded", IntialiseCost);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Cube Is Disappearing", BlackHoleIsAppearing);
        EventManager.StopListening("Data Loaded", IntialiseCost);
    }
    public void AttemptPrestige()
    {
        if (PlayerDataManager.GetVerticies() >= prestigeCost)
        {
            PlayerDataManager.AddVerticies(-prestigeCost);
            PlayerDataManager.prestigeLevel++;
            IntialiseCost();
            EventManager.TriggerEvent("Prestiged");
            AudioManager.PlaySound("Prestiged");
        }
        else
        {
            //play sound
        }
    }
    void IntialiseCost()
    {
        prestigeCost = Mathf.Pow(10, 6 + PlayerDataManager.prestigeLevel);
        EventManager.TriggerEvent("Prestige Cost Updated",prestigeCost);
    }
    private void BlackHoleIsAppearing()
    {
        StartCoroutine(BlackHoleIsAppearingRoutine());
    }

    private void Awake()
    {
        circleExplosionSpawner = GetComponent<ParticleSpawner>();
    }
    public static void SpawnExplosion(Vector2 position)
    {
        circleExplosionSpawner.SpawnExplosion(position);
    }
    IEnumerator BlackHoleIsAppearingRoutine()
    {
        float t = 0;
        blackHole.SetActive(true);
        Vector3 currentScale = blackHole.transform.localScale;
        while (blackHole.transform.localScale.x != 1)
        {
            t += Time.deltaTime;
            blackHole.transform.localScale = Vector3.Lerp(currentScale, Vector3.one, t / 3);
            yield return null;
        }
        EventManager.TriggerEvent("Explode Circle");
        Vibration.MediumVibrate();
        AudioManager.PlaySound("Circles Exploded");
        yield return new WaitForSeconds(6);
        t = 0;
        currentScale = blackHole.transform.localScale;
        EventManager.TriggerEvent("Black Hole Is Disappearing");
        while (blackHole.transform.localScale.x != 0)
        {
            t += Time.deltaTime;
            blackHole.transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, t / 3);
            yield return null;
        }
        blackHole.SetActive(false);
        EventManager.TriggerEvent("Prestige Process Over");
    }
}
