using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCollider : MonoBehaviour
{

    public HexTile HexTile;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseDown()
    {
        HexTile.OnMouseDown();
    }


}
