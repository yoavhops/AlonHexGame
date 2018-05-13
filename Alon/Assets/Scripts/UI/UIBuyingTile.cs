using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuyingTile : MonoBehaviour
{

    public Action<bool> Answer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonAnswer(bool yes)
    {
        Answer(yes);
    }

}
