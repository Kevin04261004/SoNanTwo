using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealth : LivingEntity
{
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
