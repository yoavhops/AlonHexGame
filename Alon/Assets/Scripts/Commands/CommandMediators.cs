using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CommandMediators : MonoBehaviour
{
    public static CommandMediators Singleton;
    public BuyingTileCommand BuyingTileCommand;

    void Awake()
    {
        Singleton = this;
    }

}
