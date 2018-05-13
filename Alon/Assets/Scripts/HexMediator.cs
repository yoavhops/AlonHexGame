using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;




class HexMediator : MonoBehaviour
{
    public static HexMediator Singleton;

    public HexEdge Wood;
    public HexEdge Stone;
    public HexEdge Gold;
    public HexEdge Food;
    public HexEdge Desert;
    public HexTile HexTilePrefab;

    public static Dictionary<HexEdgeType, HexEdge> HexPrefabDictionary = new Dictionary<HexEdgeType, HexEdge>();

    void Awake()
    {
        Singleton = this;

        HexPrefabDictionary.Add(HexEdgeType.Wood, Wood);
        HexPrefabDictionary.Add(HexEdgeType.Stone, Stone);
        HexPrefabDictionary.Add(HexEdgeType.Gold, Gold);
        HexPrefabDictionary.Add(HexEdgeType.Food, Food);
        HexPrefabDictionary.Add(HexEdgeType.Desert, Desert);
    }

    public HexTile CreateTile(HexTileData hexTileData, Player owner, HexTileRole hexTileRole)
    {
        var ans = Instantiate(HexTilePrefab);
        ans.Init(hexTileData, owner, hexTileRole);
        return ans;
    }


}
