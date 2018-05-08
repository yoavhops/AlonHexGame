using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player MyPlayer;
    public static List<Player> Enimes = new List<Player>();

    public bool IsMyPlayer = false;

    public Price PlayerResources;

    public List<Timer> ResourcesTimers = new List<Timer>();

    public HexTileData StartPoistion;

    public List<HexTile> MyTiles = new List<HexTile>();

    public List<HexTile> OwnedHexTiles = new List<HexTile>();

    public Material PlayerMaterial;



    void Awake()
    {
        if (IsMyPlayer)
        {
            MyPlayer = this;
            EventManager.HexClicked += TileClicked;
        }
        else
        {
            Enimes.Add(this);
        }

        EventManager.BoardIsReady += OnBoardReady;
    }


    public void TileClicked(HexTile hexTile)
    {
        //check GameManagerState.
        if (GameManager.Singleton.GameState != GameState.BuyingTile)
        {
            return;
        }

        if (IsNeighbor(hexTile))
        {
            Debug.Log("I am near");
        }
        else
        {
            Debug.Log("I am LD");
        }

    }


	// Use this for initialization
	void Start ()
	{
        foreach (var resourceType in Resource.ResourceTypes)
	    {
            ResourcesTimers.Add(
                new Timer(ResourceTimer(resourceType),
                Configuration.Singleton.TimeUntilPriceReducation
            ));
        }
	}

    public bool IsNeighbor(HexTile hexTile)
    {
        if (OwnedHexTiles.Contains(hexTile))
        {
            return false;
        }

        foreach (var ownedTile in OwnedHexTiles)
        {
            if (ownedTile.IsNeighborOfTile(hexTile))
            {
                return true;
            }
        }
        return false;
    }

    public void OnBoardReady()
    {
        var startHex = GridManager.Singleton.GetHex(StartPoistion);
        OwnedHexTiles.Add(startHex);
        startHex.SetOwner(this);
        //add my start point.
    }


    public Action ResourceTimer(HexEdgeType resourceType)
    {
        return () =>
        {
            ResourceTimerEnded(resourceType);
        };
    }

    public void ResourceTimerEnded(HexEdgeType resourceType)
    {
        Debug.Log("Timer ended for " + resourceType.ToString());
    }


	// Update is called once per frame
	void Update () {
        
	    foreach (var hexType in HexTile.HexEdgeTypes)
	    {
	        Configuration.Singleton.GetPricePerTurnOfType(hexType);
	    }
        
	}
}
