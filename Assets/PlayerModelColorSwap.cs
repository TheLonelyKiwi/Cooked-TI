using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelColorSwap : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] Renderer _renderer;
    private void Start()
    {
        Material[] material = _renderer.materials;
        if (material[1].name.Contains("MrFreshColour"))
        {
            material[1].SetColor("_Color", _player.color);
        }
    }
}
