using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class PriceConfiguration
{
    public HexEdgeType HexEdgeType;
    public Price Price;
}

[Serializable]
public class EdgeTypeChance
{
    public HexEdgeType HexEdgeType;
    public int Chance;
}


public class Configuration : MonoBehaviour
{
    public static Configuration Singleton;
    public int BoardSize;
    public List<PriceConfiguration> PriceConfigurations;
    public List<PriceConfiguration> PricePerTurnConfigurations;
    public List<PriceConfiguration> RewardPerTurnConfigurations;
    public List<EdgeTypeChance> EdgeTypeChances;

    public float TimeUntilPriceReducation = 10f;

    void Awake()
    {
        Singleton = this;
    }

    public Price GetPriceOfType(HexEdgeType hexEdgeType)
    {
        foreach (var priceConfiguration in PriceConfigurations)
        {
            if (priceConfiguration.HexEdgeType == hexEdgeType)
            {
                return priceConfiguration.Price;
            }
        }
        
        throw new NotImplementedException();
    }

    public Price GetPricePerTurnOfType(HexEdgeType hexEdgeType)
    {
        foreach (var priceConfiguration in PricePerTurnConfigurations)
        {
            if (priceConfiguration.HexEdgeType == hexEdgeType)
            {
                return priceConfiguration.Price;
            }
        }

        throw new NotImplementedException();
    }

    public Price GetRewardPerTurnOfType(HexEdgeType hexEdgeType)
    {
        foreach (var priceConfiguration in RewardPerTurnConfigurations)
        {
            if (priceConfiguration.HexEdgeType == hexEdgeType)
            {
                return priceConfiguration.Price;
            }
        }

        throw new NotImplementedException();
    }

    public int GetChanceForType(HexEdgeType hexEdgeType)
    {
        foreach (var edgeTypeChance in EdgeTypeChances)
        {
            if (edgeTypeChance.HexEdgeType == hexEdgeType)
            {
                return edgeTypeChance.Chance;
            }
        }

        throw new NotImplementedException();
    }


}
