using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int bulletsInPool = 10;
    static List<Bullet> bulletsAvailable = new List<Bullet>();
    static List<Bullet> bulletsUsed = new List<Bullet>();
    private void Awake()
    {
        PopulatePool();
    }
    void PopulatePool()
    {
        for (int i = 0; i < bulletsInPool; i++)
        {
            Bullet temp = Instantiate(bulletPrefab, this.transform);
            bulletsAvailable.Add(temp);
        }
    }
    public static Bullet PullABullet()
    {
        Bullet temp = bulletsAvailable[0];
        bulletsAvailable.Remove(temp);
        bulletsUsed.Add(temp);
        return temp;
    }
    public static void ReturnABullet(Bullet bulletToReturn)
    {
        bulletsUsed.Remove(bulletToReturn);
        bulletsAvailable.Add(bulletToReturn);
    }
}
