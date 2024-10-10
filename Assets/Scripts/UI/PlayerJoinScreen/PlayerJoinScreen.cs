using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayerJoinScreen : UIScreen<PlayerJoinScreen>
{
    [SerializeField] private PlayerJoinWidget _playerJoinWidget;
    [SerializeField] private TMP_Text _timerText;

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

    public void SetTimer(int time)
    {
        if (time == -1) {
            _timerText.gameObject.SetActive(false);
        } else {
            _timerText.gameObject.SetActive(true);
            _timerText.text = time.ToString();
        }
    }
    
    protected override void OnShow()
    {
        gameObject.SetActive(true);
        _timerText.gameObject.SetActive(false);
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