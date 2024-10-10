using System.Collections.Generic;
using UnityEngine;


public class PlayerJoinScreen : UIScreen<PlayerJoinScreen>
{
    [SerializeField] private PlayerJoinWidget _playerJoinWidget;

    private Dictionary<Player, PlayerJoinWidget> _widgets = new();

    public void AddPlayer(Player player, bool readyState)
    {
        if (_widgets.ContainsKey(player)) return;
        PlayerJoinWidget widget = Instantiate(_playerJoinWidget.gameObject, _playerJoinWidget.transform.parent).GetComponent<PlayerJoinWidget>();
        widget.gameObject.SetActive(true); // Prevents bug
        widget.Show(new PlayerJoinWidget.Context {player = player, initialReadyState = readyState});
        _widgets.Add(player, widget);
    }

    public void RemovePlayer(Player player)
    {
        if (!_widgets.TryGetValue(player, out PlayerJoinWidget widget)) return;
        Destroy(widget);
        _widgets.Remove(player);
    }
    
    protected override void OnShow()
    {
        gameObject.SetActive(true);
    }

    protected override void OnHide()
    {
        foreach (var playerJoinWidget in _widgets) {
            Destroy(playerJoinWidget.Value.gameObject);
        }
        
        _widgets.Clear();
        gameObject.SetActive(false);
    }
}