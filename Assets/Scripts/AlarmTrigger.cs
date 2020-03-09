using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnSignalingOn, OnSignalingOff;

    private void OnTriggerEnter(Collider other)
    {
        OnSignalingOn?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        float angle = Vector3.Angle(transform.forward, other.transform.position - transform.position);
        if (angle < 90)
            OnSignalingOff?.Invoke();
        else
            OnSignalingOn?.Invoke();
    }
}
