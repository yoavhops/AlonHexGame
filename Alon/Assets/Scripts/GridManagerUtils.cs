using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GridManagerUtils
{

    public static Vector3 GetHexTilePosition(HexTileData hexData)
    {
        var hexSize = GridManager.Singleton.HexSize;
        var x = hexSize * (Mathf.Sqrt(3) * hexData.X + ((Mathf.Sqrt(3) * hexData.Y) / 2));
        var y = (hexSize * 3 * hexData.Y) / 2;

        return new Vector3(x, y, 0);
    }

}
