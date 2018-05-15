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

    public List<HexTile> OwnedHexTiles = new List<HexTile>();

    public Material PlayerMaterial;

    public List<City> MyCities = new List<City>();



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
        if (GameManager.Singleton.GameState != GameState.Idle)
        {
            return;
        }

        if (!IsNeighbor(hexTile))
        {
            return;
        }

        if (!HasResource(HexUtils.CalcHexPrice(hexTile.Data)))
        {
            Debug.Log("Not enoght resources");
            return;
        }

        BuyingTileCommand.Create(hexTile);

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

	    new Timer(SumCityPayAndReward, Configuration.Singleton.TimeUntilPriceReducation);

	}

    public void SumCityPayAndReward()
    {
        foreach (var city in MyCities)
        {
            var bill = new Price();
            bill.Init();
            var resourceType = city.CityResourceType;
            var rentPrice = Configuration.Singleton.GetPricePerTurnOfType(resourceType);
            var addReward = Configuration.Singleton.GetRewardPerTurnOfType(resourceType);
            bill.Add(addReward);
            bill.Reduce(rentPrice);
            bill.MultiplyPrice(city.CitySize);
            PlayerResources.Add(bill);

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
        AddTileToPlayer(startHex);
        //add my start point.
    }

    public void AddTileToPlayer(HexTile hexTile)
    {
        OwnedHexTiles.Add(hexTile);
        hexTile.SetOwner(this);
        NewCitiesCheker(hexTile);
    }

    public void NewCitiesCheker(HexTile hexTile)
    {
        foreach (var city in CityUtils.CityCheckStart(hexTile.Data))
        {
            MyCities.Add(city);
        }
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
        //Debug.Log("Timer ended for " + resourceType.ToString());
    }


	// Update is called once per frame
	void Update () {
        
	    foreach (var hexType in HexTile.HexEdgeTypes)
	    {
	        Configuration.Singleton.GetPricePerTurnOfType(hexType);
	    }
        
	}

    public bool HasResource(Price otherPrice)
    {
        return PlayerResources.HasResource(otherPrice);
    }

}
