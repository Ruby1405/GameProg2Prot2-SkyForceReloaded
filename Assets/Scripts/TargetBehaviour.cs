using System;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public Action<GameObject> OnTargetHit;
    void OnTriggerEnter(Collider other)
    {
        OnTargetHit?.Invoke(gameObject);
    }
}
