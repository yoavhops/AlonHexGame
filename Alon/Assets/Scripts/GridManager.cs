using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GridManager : MonoBehaviour
{

    public static GridManager Singleton;

    public float HexSize = 1f;

    public HexTile HexTilePrefab;

    public Dictionary<Vector2Int, HexTile> _allTiles = new Dictionary<Vector2Int, HexTile>();


    void Awake()
    {
        Singleton = this;
    }

    public void Init()
    {
        CreateBoard();
    }

    public void CreateBoard()
    {
        var boardSize = Configuration.Singleton.BoardSize;
        for (int y = 0; y <= boardSize * 2; y++)
        {
            var realY = y - boardSize;
            for (int x = 0; x <= boardSize + boardSize - Mathf.Abs(realY) ; x++)
            {
                var startX = Mathf.Max(-y, -boardSize);
                var realX = startX + x;

                var hexTileData = new HexTileData(realX, realY, null);
                var newHex = Instantiate(HexTilePrefab, transform);
                newHex.Init(hexTileData, null, HexTileRole.InGrid);
                newHex.transform.position = GridManagerUtils.GetHexTilePosition(hexTileData);

                _allTiles[new Vector2Int(realX, realY)] = newHex;

            }
        }

        EventManager.FireBoardIsReady();
    }

    public HexTile GetHex(Vector2Int vec)
    {
        if (!_allTiles.ContainsKey(vec))
        {
            return null;
        }
        return _allTiles[vec];
    }
    public HexTile GetHex(int x, int y)
    {
        return GetHex(new Vector2Int(x, y));
    }
    public HexTile GetHex(HexTileData hexTileData)
    {
        return GetHex(hexTileData.X, hexTileData.Y);
    }

    public void TileWasBought(HexTile hexTile)
    {
        Player.MyPlayer.AddTileToPlayer(hexTile);
        NeighborsUpdate(hexTile);
        //neighbors are checked in buying tile..why?
    }

    public void NeighborsUpdate(HexTile hexTile)
    {
        foreach (var edgeData in hexTile.Data.HexEdgesData)
        {
            if (!edgeData.IsOpen)
            {
                continue;
            }
            var direction = edgeData.HexDiraction;
            var neighborTile = HexUtils.GetNeighborTile(hexTile, direction);
            if (neighborTile != null)
            {
                var neighborEdge = neighborTile.Data.HexEdgesData[(int)HexUtils.OppsiteHex[direction]];
                neighborEdge.HexEdgeType = edgeData.HexEdgeType;
                neighborEdge.IsOpen = edgeData.IsOpen;
                neighborTile.SyncVisual();
            }   
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
