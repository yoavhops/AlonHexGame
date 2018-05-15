using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

[Serializable]
public class City
{
    public List<HexEdgeData> CityEdges;
    public HexEdgeType CityResourceType {
        get { return CityEdges[0].HexEdgeType; }
    }

    public int CitySize
    {
        get { return CityEdges.Count; }
    }

    public City(List<HexEdgeData> cityEdges)
    {
        CityEdges = cityEdges;
    }

}

