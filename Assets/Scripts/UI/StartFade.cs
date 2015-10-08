using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartFade : MonoBehaviour {

    GameObject _gameObject;
    Text _text;
    Color colorFade;

    void Awake()
    {
        _gameObject = this.gameObject;
        _text = this.GetComponent<Text>();
        colorFade = Color.white;
    }

	// Update is called once per frame
	void Update () {

        if (_text.color == Color.white)
            colorFade = Color.black;
        if (_text.color == Color.black)
            colorFade = Color.white;
            
        _text.CrossFadeColor(colorFade, 1, false, false);
	}
}
