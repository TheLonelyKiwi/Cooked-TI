using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
{
    public int playerScore = 0;
    public int scorePerObject = 10; 
    public TMP_Text scoreText;
    
    private string _defaultText;

    private void Start()
    {
        _defaultText = scoreText.text;
        scoreText.text = $"{_defaultText} {playerScore}";
    }

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore += scorePerObject;
        scoreText.text = $"{_defaultText} {playerScore}";
    }
}

