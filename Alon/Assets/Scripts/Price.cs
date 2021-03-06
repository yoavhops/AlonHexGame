﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[Serializable]
public class Resource
{
    public static List<HexEdgeType> ResourceTypes = new List<HexEdgeType>()
    {
        HexEdgeType.Wood,
        HexEdgeType.Stone,
        HexEdgeType.Gold,
        HexEdgeType.Food,
    };

    public int Amount;
}


[Serializable]
public class Price
{
    public Resource Wood;
    public Resource Stone;
    public Resource Gold;
    public Resource Food;
    private List<Resource> AllResource  = new List<Resource>();

    public void Init()
    {
        Wood = new Resource();
        Stone = new Resource();
        Gold = new Resource();
        Food = new Resource();
    }

    public List<Resource> GetAllResource()
    {
        AllResource.Clear();
        AllResource.Add(Wood);
        AllResource.Add(Stone);
        AllResource.Add(Gold);
        AllResource.Add(Food);
        return AllResource;
    }

    public Resource GetResources(HexEdgeType hexEdgeType)
    {
        switch (hexEdgeType)
        {
            case HexEdgeType.Wood:
                return Wood;
            case HexEdgeType.Stone:
                return Stone;
            case HexEdgeType.Gold:
                return Gold;
            case HexEdgeType.Food:
                return Food;
        }

        throw new NotImplementedException();
    }

    public bool HasResource(Price other)
    {
        var otherPrice = other.GetAllResource();
        var myPrice = GetAllResource();

        for (int i = 0; i < myPrice.Count; i++)
        {
            if (myPrice[i].Amount < otherPrice[i].Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void Reduce(Price other)
    {
        var otherPrice = other.GetAllResource();
        var myPrice = GetAllResource();

        for (int i = 0; i < myPrice.Count; i++)
        {
            myPrice[i].Amount -= otherPrice[i].Amount;
        }
    }

    public void Add(Price other)
    {
        var otherPrice = other.GetAllResource();
        var myPrice = GetAllResource();

        for (int i = 0; i < myPrice.Count; i++)
        {
            myPrice[i].Amount += otherPrice[i].Amount;
        }
    }

    public override string ToString()
    {
        return string.Format("Wood:{0}, Stone:{1}, Gold:{2}, Food:{3}", Wood.Amount, Stone.Amount, Gold.Amount, Food.Amount);
    }

    public void MultiplyPrice(int multiplier)
    {
        var myPrice = GetAllResource();
        for (int i = 0; i < myPrice.Count; i++)
        {
            myPrice[i].Amount *= multiplier;           
        }
    }
}
