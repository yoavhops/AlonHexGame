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
    Desert,
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
public class HexEdgeData
{
    public HexEdgeType HexEdgeType;
    public HexDiraction HexDiraction;
    public HexTileData MyHexTile;
    public bool IsOpen = false;

    public HexEdgeData(HexTileData myHexTile, HexEdgeType hexEdgeType, HexDiraction hexDiraction, bool isOpen = false)
    {
        MyHexTile = myHexTile;
        HexEdgeType = hexEdgeType;
        HexDiraction = hexDiraction;
        IsOpen = isOpen;
    }
}


public class HexEdge : MonoBehaviour
{
    public HexEdgeData HexEdgeData;

    void Awake()
    {
    }

    public void Init(HexEdgeData hexEdgeData)
    {
        HexEdgeData = hexEdgeData;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
