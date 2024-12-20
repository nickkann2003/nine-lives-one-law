using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using static BulletManager;
public enum Bullets
{
    PlayerBullet,
    EnemyBullet,
    All
}

public class BulletManager : MonoBehaviour
{

    public static BulletManager instance;

    public Transform bulletList;

    public GameObject bulletPrefab;

    private List<Bullet> activeBullets = new List<Bullet>();
    private List<Bullet> inactiveBullets = new List<Bullet>();

    // Disgustingly gross literal declarations, I don't like doing this but I have to
    public static Dictionary<Bullets, List<string>> targetsDictionary = new Dictionary<Bullets, List<string>>
    {
        {Bullets.PlayerBullet, new List<string>(){"Enemy", "Bullet"}},
        {Bullets.EnemyBullet, new List<string>(){"Player", "Bullet"}},
        {Bullets.All, new List<string>(){"Enemy", "Bullet", "Player"}}
    };    

    public static Dictionary<Bullets, List<string>> obstaclesDictionary = new Dictionary<Bullets, List<string>>
    {
        {Bullets.PlayerBullet, new List<string>(){ }},
        {Bullets.EnemyBullet, new List<string>(){ }},
        {Bullets.All, new List<string>(){ }}
    };

private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetBulletList()
    {
        return bulletList;
    }

    public void CreateBullet(List<string> targets, List<string> obstacles, float damage, Vector3 shootingPosition, Vector3 velocity)
    {
        GameObject bulletGO;
        Bullet bullet;
        if(inactiveBullets.Count <= 0)
        {
            bulletGO = Instantiate(bulletPrefab, bulletList);
            bullet = bulletGO.GetComponent<Bullet>();
        }
        else
        {
            bullet = inactiveBullets[0];
            bullet.gameObject.SetActive(true);
            inactiveBullets.RemoveAt(0);
        }
        bullet.targetTags = targets;
        bullet.obstacleTags = obstacles;
        bullet.damage = damage;
        bullet.Shoot(shootingPosition, velocity);
        activeBullets.Add(bullet);
    }

    public void CreateBullet(List<string> targets, List<string> obstacles, float damage, Vector3 shootingPosition, Vector3 velocity, GameObject prefab)
    {
        GameObject bulletGO;
        Bullet bullet;
        //if (inactiveBullets.Count <= 0)
        if(true)
        {
            bulletGO = Instantiate(prefab, bulletList);
            bullet = bulletGO.GetComponent<Bullet>();
        }
        else
        {
            bullet = inactiveBullets[0];
            bullet.gameObject.SetActive(true);
            inactiveBullets.RemoveAt(0);
        }
        bullet.targetTags = targets;
        bullet.obstacleTags = obstacles;
        bullet.damage = damage;
        bullet.Shoot(shootingPosition, velocity);
        activeBullets.Add(bullet);
    }

    public void CreateBullet(Bullets bulletType, float damage, Vector3 shootingPosition, Vector3 velocity)
    {
        CreateBullet(targetsDictionary[bulletType], obstaclesDictionary[bulletType], damage, shootingPosition, velocity);
    }

    public void CreateBullet(Bullets bulletType, float damage, Vector3 shootingPosition, Vector3 velocity, GameObject prefab)
    {
        CreateBullet(targetsDictionary[bulletType], obstaclesDictionary[bulletType], damage, shootingPosition, velocity, prefab);
    }

    public void DestroyBullet(Bullet bullet)
    {
        try
        {
            inactiveBullets.Add(bullet);
            activeBullets.Remove(bullet);
        }
        catch
        {
            Debug.Log("Issue disabling bullet, not found in list or not able to add - Ignoring");
        }
        bullet.gameObject.SetActive(false);
    }

    public void ClearBulletList()
    {
        foreach(Bullet b in transform.GetComponentsInChildren<Bullet>()) 
        {
            DestroyBullet(b);
        }
    }
}
