using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionPlane : MonoBehaviour
{
    public GameObject plane;
    public void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
