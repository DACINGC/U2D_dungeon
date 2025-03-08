using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttackController : MonoBehaviour
{
    private int transDir;

    public void SetUpFireAttack(int _transDir)
    { 

        transDir = _transDir;
        if (transDir == -1)
            transform.Rotate(0, 180, 0);
    }

}

