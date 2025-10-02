using System;
using UnityEngine;
using UnityEngine.Events;

public class TargetBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent<GameObject> OnTargetHit;
    
    void OnTriggerEnter(Collider other)
    {
        OnTargetHit?.Invoke(other.gameObject);
    }
}
