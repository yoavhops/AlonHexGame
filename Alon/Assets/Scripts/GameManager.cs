using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    Idle,
    BuyingTile,
}


public class GameManager : MonoBehaviour
{

    public static GameManager Singleton;
    public GameState GameState;


    void Awake()
    {
        Singleton = this;
    }

    public void ChangeState(GameState gameState)
    {
        GameState = gameState;
    }


    public void TileClicked(HexTile hexTile)
    {
        switch (GameState)
        {
            case GameState.Idle:
                ChangeState(GameState.BuyingTile);
                EventManager.FireHexClicked(hexTile);
                break;

        }
        return;
    }


    public void RetrunToNormal()
    {
        
    }




    // Use this for initialization
    void Start()
    {
        GridManager.Singleton.Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
