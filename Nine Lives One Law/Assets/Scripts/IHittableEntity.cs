using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittableEntity
{
    public void HandleBulletHit(Bullet b);

    public void HandleDamageHit(float damage);
}
