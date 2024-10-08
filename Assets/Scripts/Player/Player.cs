using System;
using UnityEngine;


public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerInput input { get; private set; }
    [field: SerializeField] public PlayerMovement movement { get; private set; }
    [field: SerializeField] public PlayerInteractor interactor { get; private set; }
    [field: SerializeField] public PlayerStateMachine playerStateMachine { get; private set; }

    private void Reset()
    {
        input = GetComponentInChildren<PlayerInput>();
        movement = GetComponentInChildren<PlayerMovement>();
        interactor = GetComponentInChildren<PlayerInteractor>();
        playerStateMachine = GetComponentInChildren<PlayerStateMachine>();
    }
}