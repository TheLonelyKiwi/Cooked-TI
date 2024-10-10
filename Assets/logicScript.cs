using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class logicScript : MonoBehaviour
    {
    public int playerScore;

    public TMP_Text scoreText; 

    [ContextMenu("Increase Score")]
    public void addScore ()
    {playerScore = playerScore + 10;
    scoreText.text = playerScore.ToString();
    }

    private void Update() {
      if(Input.GetKeyDown(KeyCode.Space))
      {addScore();}
    }

    }

