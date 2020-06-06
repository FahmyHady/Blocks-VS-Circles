using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    ParticleSystem bulletHitEffect;
    ParticleSystem tappedEffect;
    ParticleSystem.EmitParams emitBulletHitParams;
    public void OnPointerClick(PointerEventData eventData)
    {
        //Play Clicked Effect
        EventManager.TriggerEvent("Tapped");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Play Particle Effect At Collision Point
    }
}
