using System;
using UnityEngine;

public class FieldOfView3 : MonoBehaviour
{
    private SphereCollider viewCollider;

    private void Start()
    {
        viewCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} in vision");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"{other.name} lost");
    }
}