using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSet : MonoBehaviour
{
    public Text winnerText;
    public void GameOver(string winner)
    {
        gameObject.SetActive(true);
        winnerText.text = winner;
    }
}

