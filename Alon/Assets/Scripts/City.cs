using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class City
{
    public List<HexEdgeData> CityEdges;
    public HexEdgeType CityResourceType {
        get { return CityEdges[0].HexEdgeType; }
    }


}

