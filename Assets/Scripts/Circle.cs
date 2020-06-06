using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Upgradable
{
    static Cube theCube;
    private void Awake()
    {
        if (!theCube)
        {
            theCube = FindObjectOfType<Cube>();
        }
    }
    new private void OnEnable()
    {
        base.OnEnable();
        if (currentLevel != 0)
        {
            Appear();
        }
 
    }
    void ShootCube()
    {
        Bullet bulletToFire = BulletPoolManager.PullABullet();
        bulletToFire.transform.position = transform.position;
        bulletToFire.transform.rotation = transform.rotation;
        bulletToFire.myWorth = goldPerTap;
        bulletToFire.gameObject.SetActive(true);
    }
    private void Update()
    {
        transform.right = theCube.transform.position - transform.position;
    }
    public void Appear()
    {
        InvokeRepeating("ShootCube", 1, 1);
    }
}
