using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MonoMono : MonoBehaviour
{

    public event Action<MonoMono> OnDestruction;

    protected bool _isInitialized = false;

    void OnDestroy()
    {
        if (OnDestruction == null)
        {
            return;
        }

        OnDestruction(this);
    }



}