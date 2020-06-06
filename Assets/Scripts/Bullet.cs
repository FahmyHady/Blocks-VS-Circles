using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public double myWorth;
    Rigidbody2D rb;
    TrailRenderer trailRenderer;
    public float bulletSpeed;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        PlayerDataManager.AddVerticies(myWorth);
        gameObject.SetActive(false);
        BulletPoolManager.ReturnABullet(this);
    }
    private void OnEnable()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(transform.right * bulletSpeed);
        trailRenderer.emitting = true;
    }
    private void OnDisable()
    {
        trailRenderer.emitting = false;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
