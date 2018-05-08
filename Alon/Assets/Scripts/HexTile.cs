using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HexEdgeType
{
    Wood,
    Stone,
    Gold,
    Food,
    Deset,    
}

public enum HexDiraction
{
    RightUp,
    Right,
    RightDown,
    LeftDown,
    Left,
    LeftUp
}

[Serializable]
public class HexTileData
{
    public int X;
    public int Y;
    
    public HexTileData(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2Int GetVector()
    {
        return new Vector2Int(X, Y);
    }

}


public class HexTile : MonoMono
{
    public List<Transform> EdgesLocations;
    public HexEdge HexEdgePrefab;
    public HexTileData Data;
    private Player _myOwner;

    public Renderer OutRenderer;


    public static List<HexEdgeType> HexEdgeTypes = new List<HexEdgeType>()
    {
        HexEdgeType.Wood,
        HexEdgeType.Stone,
        HexEdgeType.Gold,
        HexEdgeType.Food,
        HexEdgeType.Deset,
    };

    public static Dictionary<HexDiraction, Vector2Int> HexEdgeOffset = new Dictionary<HexDiraction, Vector2Int>()
    {
        { HexDiraction.RightUp, new Vector2Int(0, 1) },
        { HexDiraction.Right, new Vector2Int(1, 0) },
        { HexDiraction.RightDown, new Vector2Int(1, -1) },
        { HexDiraction.LeftDown, new Vector2Int(0 , -1) },
        { HexDiraction.Left, new Vector2Int(-1, 0) },
        { HexDiraction.LeftUp, new Vector2Int(-1, 1) },
    };

    public void SetOwner(Player newOwner)
    {
        _myOwner = newOwner;
        SyncVisual();
    }



    public void SyncVisual()
    {

        if (_myOwner != null)
        {
            OutRenderer.material = _myOwner.PlayerMaterial;
        }


    }


    void Awake()
    {
        foreach (var edgesLocation in EdgesLocations)
        {
            Instantiate(HexEdgePrefab, edgesLocation);
        }
    }

    public void Init(HexTileData hexTileData)
    {
        Data = hexTileData;
        _isInitialized = true;
        gameObject.name = "X : " + hexTileData.X + "Y : " + hexTileData.Y;
    }
    
    public bool IsNeighborOfTile(HexTile otherTile)
    {
        foreach (var diraction in Utils.EnumUtil.GetValues<HexDiraction>())
        {
            var checkTileLocation = Data.GetVector() + HexEdgeOffset[diraction];
            var checkTile = GridManager.Singleton.GetHex(checkTileLocation);

            if (checkTile == null)
            {
                continue;
            }

            if (checkTile == otherTile)
            {
                return true;
            }

        }

        return false;

    }


    public void OnMouseDown()
    {
        Debug.Log("Clicked");
        GameManager.Singleton.TileClicked(this);
    }



    // Use this for initialization
    void Start () {

	    if (!_isInitialized)
	    {
	        throw new NotImplementedException();
        }

	}
	
	// Update is called once per frame
	void Update () {

    }

}
