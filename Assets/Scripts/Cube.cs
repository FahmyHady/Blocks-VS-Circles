using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    ParticleSpawner myExplosionSpawner;
    [SerializeField] ParticleSystem tappedEffect;
    private void Awake()
    {
        myExplosionSpawner = GetComponent<ParticleSpawner>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        tappedEffect.Emit(1);
        EventManager.TriggerEvent("Tapped");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        myExplosionSpawner.SpawnExplosion(collision.GetContact(0).point);
    }
}
