using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JetBrains.Annotations;
using UnityEngine;

public static class CityUtils
{
    public static List<HexEdgeData> CityCompleteCheck(HexTile hexTile, HexEdgeData edgeDate)
    {
        var visited = new List<HexEdgeData>();//will check if the city is complete
        return visited;
    }

    public static List<City> CityCheckStart(HexTileData hexTileData)
    {
        var ans = new List<City>();

        foreach (var edge in hexTileData.HexEdgesData)
        {
            if (edge.HexEdgeType == HexEdgeType.Desert)
            {
                continue;
            }

            foreach (var city in ans)
            {
                if (city.CityEdges.Contains(edge))
                {
                    continue;
                }
            }

            var visited = new List<HexEdgeData>();

            if (CityEdgesCheck(visited, edge))
            {
                var city = new City(visited);
                ans.Add(city);
            }
        }
        return ans;
    }

    public static bool CityEdgesCheck(List<HexEdgeData> visited, HexEdgeData edge)
    {
        visited.Add(edge);

        var clockwiseEdge = HexUtils.GetClockwiseEdge(edge.MyHexTile, edge, 1);
        var antiClockwiseEdge = HexUtils.GetClockwiseEdge(edge.MyHexTile, edge, -1);
        
        if (!CityEdgeCheck(visited, edge.HexEdgeType, clockwiseEdge))
        {
            return false;
        }

        if (!CityEdgeCheck(visited, edge.HexEdgeType, antiClockwiseEdge))
        {
            return false;
        }
        
        if (edge.IsOpen)
        {
            var connected = HexUtils.GetConnectedEdge(edge.MyHexTile, edge);

            if (connected.MyHexTile.Owner != Player.MyPlayer)
            {
                return false;
            }

            if (!CityEdgeCheck(visited, edge.HexEdgeType, connected))
            {
                return false;
            }
        }

        return true;
    }

    public static bool CityEdgeCheck(List<HexEdgeData> visited, HexEdgeType hexEdgeType, HexEdgeData edge)
    {
        if (!visited.Contains(edge) &&
            edge.HexEdgeType == hexEdgeType)
        {
            if (!CityEdgesCheck(visited, edge))
            {
                return false;
            }
        }
        return true;
    }

}
