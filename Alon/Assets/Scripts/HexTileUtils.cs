using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class HexTileUtils
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



}

