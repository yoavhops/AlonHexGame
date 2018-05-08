using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                var hexTileData = new HexTileData(realX, realY);
                var newHex = Instantiate(HexTilePrefab, transform);
                newHex.Init(hexTileData);
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


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
