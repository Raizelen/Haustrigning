using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    [SerializeField] private Transform player1Health;
    [SerializeField] private Transform player2Health;
    private float player1HP;
    private float player2HP;
    private bool Iframe1 = false;
    private bool Iframe2 = false;
    public GameSet gameSet;
    [SerializeField] private Animator anim1;
    [SerializeField] private Animator anim2;
    



    // Start is called before the first frame update
    void Start()
    {
        player1HP = 100f;
        player2HP = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player1HP <= 0f && player2HP <= 0f)
        {
            Debug.Log("Draw");
            gameSet.GameOver("Draw");
        }
        else if (player1HP <= 0)
        {
            Debug.Log("Player 2 won");
            gameSet.GameOver("Player 2 won");
        }
        else if (player2HP <= 0)
        {
            Debug.Log("Player 1 won");
            gameSet.GameOver("Player 1 won");
        }

    }

    public void UpdateHealth(string player,float attackValue)
    {
        if(player == "player1" && !Iframe2 && player2HP > 0)
        {
            player2Health.localScale = new Vector3((player2HP = player2HP - attackValue) * 0.01f, player2Health.localScale.y, player2Health.localScale.z);
        }
            
        else if (player == "player2" && !Iframe1 && player1HP >0)
        {
            player1Health.localScale = new Vector3((player1HP = player1HP - attackValue) * 0.01f, player1Health.localScale.y, player1Health.localScale.z);
        }
        Debug.Log("hits" + player);

    }
    public void Block(string player,bool value)
    {
        if(player == "player1")
            Iframe1 = value;
        else if(player == "player2")
            Iframe2 = value;
        Debug.Log(player + "Blocks");
    }

}
