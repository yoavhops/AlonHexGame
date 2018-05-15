using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class HexTileData
{
    public int X;
    public int Y;
    public List<HexEdgeData> HexEdgesData = new List<HexEdgeData>();
    public Player _owner;

    public Player Owner
    {
        get { return _owner; }
        set
        {
            _owner = value;
        }
    }


    public HexTileData(int x, int y, Player owner)
    {
        X = x;
        Y = y;
        Owner = owner;

        var hexDiractions = Utils.EnumUtil.GetValues<HexDiraction>();

        foreach (var hexDiraction in hexDiractions)
        {
            HexEdgesData.Add(new HexEdgeData(this, HexEdgeType.Desert, hexDiraction));
        }
    }

    public void SetOwner(Player newOwner)
    {
        Owner = newOwner;
    }

    public Vector2Int GetVector()
    {
        return new Vector2Int(X, Y);
    }

}

public enum HexTileRole
{
    InUI,
    InGrid,
}


public class HexTile : MonoMono
{
    public List<Transform> EdgesLocations;
    public Renderer OutRenderer;
    public List<HexEdge> Edges;

    // +++++++++++++

    public HexTileData Data;
    public HexTileRole HexTileRole;
    public Action<HexTile> OnClickCallback;


    public static List<HexEdgeType> HexEdgeTypes = new List<HexEdgeType>()
    {
        HexEdgeType.Wood,
        HexEdgeType.Stone,
        HexEdgeType.Gold,
        HexEdgeType.Food,
        HexEdgeType.Desert,
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

    void Awake()
    {
    }

    public void Init(HexTileData hexTileData, Player owner, HexTileRole hexTileRole)
    {
        HexTileRole = hexTileRole;
        Data = hexTileData;
        Data.Owner = owner;

        CreateEdges();

        _isInitialized = true;
        gameObject.name = "X : " + hexTileData.X + "Y : " + hexTileData.Y;
    }

    private void CreateEdges()
    {
        Edges.Clear();
        for (int i = 0; i < EdgesLocations.Count; i++)
        {
            if (EdgesLocations[i].childCount > 0)
            {
                Destroy(EdgesLocations[i].GetChild(0).gameObject);
            }
            var hexEdge = Instantiate(HexMediator.HexPrefabDictionary[Data.HexEdgesData[i].HexEdgeType], EdgesLocations[i]);
            hexEdge.Init(Data.HexEdgesData[i]);
            Edges.Add(hexEdge);
        }
    }


    public void SetOwner(Player newOwner)
    {
        Data.Owner = newOwner;
        Debug.Log("Set owner worked - " + Player.MyPlayer);
        SyncVisual();
    }

    public void SyncVisual()
    {
        if (Data.Owner != null)
        {
            OutRenderer.material = Data.Owner.PlayerMaterial;
        }

        CreateEdges();
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
        switch (HexTileRole)
        {
            case HexTileRole.InGrid:
                GameManager.Singleton.TileClicked(this);
                break;
            case HexTileRole.InUI:

                if (OnClickCallback == null)
                {
                    return;
                }

                OnClickCallback(this);

                break;
        }

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
