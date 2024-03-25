using System;
using UnityEngine;

public class FieldOfView3 : MonoBehaviour
{
    private SphereCollider viewCollider;

    private void Start()
    {
        viewCollider = GetComponent<SphereCollider>();
        /*var excludedLayerMask = LayerMask.GetMask("Obstacles", "Default");
        var includedLayerMask = LayerMask.GetMask("Players");
        viewCollider.excludeLayers = excludedLayerMask;
        viewCollider.includeLayers = includedLayerMask;*/
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