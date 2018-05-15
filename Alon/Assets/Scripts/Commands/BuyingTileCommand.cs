using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BuyingTileCommand : MonoBehaviour
{

    public int AmountOfOption = 4;

    private HexTile _hexTile;

    public HexTile UIHexTile;


    public static void Create(HexTile hexTile)
    {
        var command = Instantiate(CommandMediators.Singleton.BuyingTileCommand, CommandMediators.Singleton.gameObject.transform);
        GameManager.Singleton.ChangeState(GameState.BuyingTile);
        command._hexTile = hexTile;
    }

    public void Init()
    {
        
    }

    void Start()
    {
        UIManager.Singleton.UIConfirm.gameObject.SetActive(true);
        UIManager.Singleton.UIConfirm.Answer = ButtonAnswer;
    }

    public void ButtonAnswer(bool yes)
    {
        Debug.Log("Commnad reviced :" + yes);

        UIManager.Singleton.UIConfirm.gameObject.SetActive(false);

        if (!yes)
        {
            GameManager.Singleton.ChangeState(GameState.Idle);
            Destroy(gameObject);
            return;
        }
        
        var uiChooseTile = UIManager.Singleton.UIChoosingTile;
        uiChooseTile.gameObject.SetActive(true);
        
        for (int i = 0; i < AmountOfOption; i++)
        {
            CreateOption(i);
        }

    }

    public void CreateOption(int index)
    {
        var uiChooseTile = UIManager.Singleton.UIChoosingTile;

        HexTile hexTile = null;
        if (index == 0)
        {
            hexTile = HexUtils.BuildHexTile(_hexTile.Data, Player.MyPlayer, HexTileRole.InUI);
        }
        else
        {
            var affordAble = false;
            var counter = 0;
            HexTileData randomData = null;
            while (!affordAble)
            {
                randomData = HexUtils.CreateRandomData(_hexTile.Data.X, _hexTile.Data.Y);
                counter++;
                if (counter == 100)
                {
                    randomData = _hexTile.Data;
                    UnityEngine.Debug.LogWarning("Could not find Random Data");
                }
                affordAble = Player.MyPlayer.HasResource(HexUtils.CalcHexPrice(randomData));
            }

            hexTile = HexUtils.BuildHexTile(randomData, Player.MyPlayer, HexTileRole.InUI);
        }
        
        hexTile.transform.SetParent(uiChooseTile.ChoosingTransforms[index].transform, false);

        hexTile.OnClickCallback = OnTileChosen;

    }

    public void OnTileChosen(HexTile hexTile)
    {
        var uiChooseTile = UIManager.Singleton.UIChoosingTile;
        uiChooseTile.gameObject.SetActive(false);

        _hexTile.Data = hexTile.Data;
        GridManager.Singleton.TileWasBought(_hexTile);

        Player.MyPlayer.PlayerResources.Reduce(HexUtils.CalcHexPrice(_hexTile.Data));

        Clear();

    }
    private void Clear()
    {
        var uiChooseTile = UIManager.Singleton.UIChoosingTile;
        uiChooseTile.gameObject.SetActive(false);

        for (int i = 0; i < AmountOfOption; i++)
        {
            Destroy(uiChooseTile.ChoosingTransforms[i].GetChild(0).gameObject);
        }
        GameManager.Singleton.ChangeState(GameState.Idle);
    }



}
