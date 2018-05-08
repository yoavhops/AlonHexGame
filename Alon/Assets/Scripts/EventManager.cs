using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
static class EventManager
{

    public static event Action BoardIsReady;
    public static event Action<HexTile> HexClicked;

    public static void FireBoardIsReady()
    {
        if (BoardIsReady == null)
        {
            return;
        }
        BoardIsReady();
    }

    public static void FireHexClicked(HexTile hexTile)
    {
        if (HexClicked == null)
        {
            return;
        }
        HexClicked(hexTile);
    }




}
