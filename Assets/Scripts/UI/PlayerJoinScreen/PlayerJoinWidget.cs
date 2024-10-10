using System;
using TMPro;
using UnityEngine;


public class PlayerJoinWidget : UIWidget<PlayerJoinWidget.Context>
{
    [SerializeField] private TMP_Text _readyText;
    [SerializeField] private TMP_Text _colorText;
    
    private Context _context;
    
    protected override void OnShow(Context context)
    {
        _context = context;
        gameObject.SetActive(true);
        UpdateState(_context.initialReadyState);
        _colorText.color = context.player.color;
        _colorText.text = "Player " + context.player.colorName;
        
        EventBus.instance.onPlayerReadyChange += HandleStateChanged;
    }

    protected override void OnHide()
    {
        gameObject.SetActive(false);
        EventBus.instance.onPlayerReadyChange -= HandleStateChanged;
    }

    private void UpdateState(bool isReady)
    {
        _readyText.text = isReady ? "Ready" : "Not Ready\nPress A/X/SPACE\nTo ready up";
    }

    private void HandleStateChanged(Player player, bool newState)
    {
        if (player != _context.player) return;
        UpdateState(newState);
    }


    public class Context
    {
        public Player player;
        public bool initialReadyState;
    }
}