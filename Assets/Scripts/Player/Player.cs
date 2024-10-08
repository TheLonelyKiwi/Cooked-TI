using System;
using JUtils;
using UnityEngine;


public class Player : MonoBehaviour
{
    [field: SerializeField, Required] public Transform rotatingTransform { get; private set; }
    [field: SerializeField, Required] public Transform visual { get; private set; }
    
    [field: Space]
    [field: SerializeField] public PlayerInput input { get; private set; }
    [field: SerializeField] public PlayerMovement movement { get; private set; }
    [field: SerializeField] public PlayerInteractor interactor { get; private set; }
    [field: SerializeField] public PlayerStateMachine stateMachine { get; private set; }
    [field: SerializeField] public Rigidbody rigidbody { get; private set; }

    private void Reset()
    {
        input = GetComponentInChildren<PlayerInput>();
        movement = GetComponentInChildren<PlayerMovement>();
        interactor = GetComponentInChildren<PlayerInteractor>();
        stateMachine = GetComponentInChildren<PlayerStateMachine>();
        rigidbody = GetComponentInChildren<Rigidbody>();
    }
}