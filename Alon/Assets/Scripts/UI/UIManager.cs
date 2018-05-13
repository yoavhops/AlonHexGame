using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Singleton;

    public UIBuyingTile UIConfirm;
    public UIChoosingTile UIChoosingTile;

    void Awake()
    {
        Singleton = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
