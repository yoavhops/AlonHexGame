using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework.Internal;
using UnityEngine;

public static class HexUtils
{

    public static Dictionary<HexDiraction, HexDiraction> OppsiteHex = new Dictionary<HexDiraction, HexDiraction>()
    {
        {HexDiraction.RightUp, HexDiraction.LeftDown},
        {HexDiraction.Right, HexDiraction.Left},
        {HexDiraction.RightDown, HexDiraction.LeftUp},
        {HexDiraction.LeftDown, HexDiraction.RightUp},
        {HexDiraction.Left, HexDiraction.Right},
        {HexDiraction.LeftUp, HexDiraction.RightDown},
    };


    public static HexTile BuildHexTile(HexTileData hexTileData, Player owner, HexTileRole hexTileRole)
    {
        return HexMediator.Singleton.CreateTile(hexTileData, owner, hexTileRole);
    }


    public static Price CalcHexPrice(HexTileData hexTileData)
    {
        Price ans = new Price();
        ans.Init();
        foreach (var hexEdgeData in hexTileData.HexEdgesData){
            ans.Add(CalcHexEdgePrice(hexEdgeData));
        }
        return ans;
    }

    public static Price CalcHexEdgePrice(HexEdgeData hexEdgeData)
    {
        return Configuration.Singleton.GetPriceOfType(hexEdgeData.HexEdgeType);
    }
    


    public static HexTileData CreateRandomData(int x, int y)
    {
        var ans = new HexTileData(x, y, null);
        var edgesData = new List<HexEdgeData>();
        foreach (var diraction in Utils.EnumUtil.GetValues<HexDiraction>())
        {
            edgesData.Add(CreateRandomEdgeData(x, y, diraction));
        }
        ans.HexEdgesData = edgesData;
        return ans;
    }

    public static HexEdgeData CreateRandomEdgeData(int x, int y, HexDiraction diraction)
    {
        var location = new Vector2Int(x, y);
        var myTile = GridManager.Singleton.GetHex(location);
        Vector2Int neighborLocation = location + HexTile.HexEdgeOffset[diraction];

        var neighborTile = GridManager.Singleton.GetHex(neighborLocation);

        if (neighborTile == null)
        {
            return new HexEdgeData(GetRandomEdgeType(), diraction);
        }

        var neighborEdge = neighborTile.Data.HexEdgesData[(int) OppsiteHex[diraction]];

        if (!neighborEdge.IsOpen)
        {
            if (neighborTile.Data.Owner == null)
            {
                return new HexEdgeData(GetRandomEdgeType(), diraction, Utils.RandomBool());
            }
            return new HexEdgeData(GetRandomEdgeType(), diraction);
        }
        
        return new HexEdgeData(neighborEdge.HexEdgeType, diraction, true);
    }


    public static HexEdgeType GetRandomEdgeType()
    {
        int allChances = 0;
        foreach (var edgeTypeChance in Configuration.Singleton.EdgeTypeChances)
        {
            allChances += edgeTypeChance.Chance;
        }

        var randomValue = UnityEngine.Random.Range(0, allChances);
        
        foreach (var edgeTypeChance in Configuration.Singleton.EdgeTypeChances)
        {
            randomValue -= edgeTypeChance.Chance;

            if (randomValue < 0)
            {
                return edgeTypeChance.HexEdgeType;
            }
        }
        
        throw new NotImplementedException();
    }

    public static HexTile GetNeighborTile(HexTile hexTile, HexDiraction diraction)
    {
        var neighborLocation = hexTile.Data.GetVector() + HexTile.HexEdgeOffset[diraction];
        return GridManager.Singleton.GetHex(neighborLocation);
    }

    public static List<HexTile> GetAllNeighborTile(HexTile hexTile)
    {
        List<HexTile> ans = new List<HexTile>();

        foreach (var diraction in Utils.EnumUtil.GetValues<HexDiraction>())
        {
            var neighbor = GetNeighborTile(hexTile, diraction);
            if (neighbor != null)
            {
                ans.Add(neighbor);
            }
        }
        return ans;
    }


}
