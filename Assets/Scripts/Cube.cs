using Boo.Lang.Environments;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    SpriteRenderer spriteRenderer;
    ParticleSpawner myExplosionSpawner;
    BoxCollider2D myCollider;
    [SerializeField] Sprite[] prestigeCubeSprites;
    [SerializeField] ParticleSystem tappedEffect;
    bool scaling;
    private void OnEnable()
    {
        EventManager.StartListening("Prestiged", PlayerPrestiged);
        EventManager.StartListening("Data Loaded", AssignSprite);
        EventManager.StartListening("Black Hole Is Disappearing", CubeAppearing);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Prestiged", PlayerPrestiged);
        EventManager.StopListening("Data Loaded", AssignSprite);
        EventManager.StopListening("Black Hole Is Disappearing", CubeAppearing);

    }
    void CubeAppearing()
    {
        StartCoroutine(CubeAppearingRoutine());
    }
    private void PlayerPrestiged()
    {
        StartCoroutine(CubeDisappearingRoutine());
    }
    IEnumerator CubeDisappearingRoutine()
    {
        scaling = true;
        float t = 0;
        Vector3 currentScale = transform.localScale;
        EventManager.TriggerEvent("Cube Is Disappearing");
        while (transform.localScale.x != 0)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(currentScale, Vector3.zero, t / 3);
            yield return null;
        }
        AssignSprite();
    }

    private void AssignSprite()
    {
        spriteRenderer.sprite = prestigeCubeSprites[PlayerDataManager.prestigeLevel % prestigeCubeSprites.Length];
    }

    IEnumerator CubeAppearingRoutine()
    {
        float t = 0;
        Vector3 currentScale = transform.localScale;
        while (transform.localScale.x != 1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(currentScale, Vector3.one, t / 3);
            yield return null;
        }
        tappedEffect.Emit(1);
        yield return new WaitForSeconds(0.5f);
        tappedEffect.Emit(1);
        yield return new WaitForSeconds(0.5f);
        tappedEffect.Emit(1);
        myCollider.enabled = false;
        myCollider.enabled = true;
        scaling = false;
    }
    private void Awake()
    {
        myExplosionSpawner = GetComponent<ParticleSpawner>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<BoxCollider2D>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!scaling)
        {
            tappedEffect.Emit(1);
            AudioManager.PlaySound("Cube Tap");
            EventManager.TriggerEvent("Tapped");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myExplosionSpawner.SpawnExplosion(collision.GetContact(0).point);
        AudioManager.PlaySound("Cube Hit");
    }
}
