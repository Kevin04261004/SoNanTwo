using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class BlockHP : LivingEntity
{
    public override void Die()
    {
        base.Die();
        PhotonNetwork.Destroy(gameObject);
    }
}
