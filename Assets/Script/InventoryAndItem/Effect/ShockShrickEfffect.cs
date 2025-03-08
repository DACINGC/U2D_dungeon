using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shrick Effect", menuName = "Effect/Shock Shrick")]
public class ShockShrickEfffect : ItemEffect
{
    public GameObject shockPrefab;

    public override void ApplyEffect(Transform _targetTransform)
    {
        bool canShock = PlayerManager.instance.player.primaryAttack.attackNum == 2;

        if (canShock)
        {
            GameObject newShock = Instantiate(shockPrefab, _targetTransform.position, Quaternion.identity);
            Destroy(newShock, 1f);
        }
      
    }
}
