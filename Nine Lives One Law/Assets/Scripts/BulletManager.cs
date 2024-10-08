using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public enum Bullets
    {
        PlayerBullet,
        EnemyBullet
    }

    public BulletManager instance;

    public Transform bulletList;

    public GameObject bulletPrefab;

    private Dictionary<Bullets, List<string>> targetsDictionary;

    private Dictionary<Bullets, List<string>> obstaclesDictionary;



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

    public void CreateBullet()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, bulletList);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
    }
}
