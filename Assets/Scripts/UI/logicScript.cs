using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
    {
    public int playerScore;
    public int scorePerObject = 10; 
    public TMP_Text scoreText; 

    [ContextMenu("Increase Score")]
    public void addScore ()
    {playerScore = playerScore + scorePerObject;
    scoreText.text = playerScore.ToString();
    }

    

    }

