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
    [field: SerializeField] public Inventory inventory { get; private set; }
    
    // Warning! Use with care Preferably only in Interactables, the player statemachine & the game state machine.
    // Do not keep this or the player cached. Otherwise we can have unintended side effects
    [field: SerializeField] public PlayerStateMachine stateMachine { get; private set; }
    [field: SerializeField] public Rigidbody rigidbody { get; private set; }

    public Color color;
    public string colorName;

    private void Reset()
    {
        input = GetComponentInChildren<PlayerInput>();
        movement = GetComponentInChildren<PlayerMovement>();
        interactor = GetComponentInChildren<PlayerInteractor>();
        stateMachine = GetComponentInChildren<PlayerStateMachine>();
        rigidbody = GetComponentInChildren<Rigidbody>();
    }
}