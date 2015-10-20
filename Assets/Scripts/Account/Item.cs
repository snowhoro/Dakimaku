﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    private bool _selected;
    public bool Selected { get { return _selected; } set { _selected = value; } }

    public Character _character { get; private set; }
    public Image _CharImg {get; private set;}

    public void Initialize()
    {
        _character = new Character();
    }

    void Awake()
    {
        _selected = false;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
