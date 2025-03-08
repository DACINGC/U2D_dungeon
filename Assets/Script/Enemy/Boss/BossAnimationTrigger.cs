using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationTrigger : EnemyAnimationTrigger
{
    private Boss_Enemy boss => GetComponentInParent<Boss_Enemy>();

    private void Relocate() => boss.FindPosition();

    private void MakeInvisible() => boss.fX.MakeTransprent(true);

    private void MakeUnInvisible() => boss.fX.MakeTransprent(false);
}
