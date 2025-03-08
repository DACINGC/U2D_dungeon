using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : ScriptableObject
{
    public virtual void ApplyEffect(Transform _targetTransform)
    {
        Debug.Log("Apply Effect");
    }
}
