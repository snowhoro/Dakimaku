using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    private bool _selected;
    public bool Selected { get { return _selected; } set { _selected = value; } }

    public Character _character { get; private set; }
    public Image _CharImg;
    public Transform _transform;

    public void Initialize()
    {
        _character = new Character();
    }

    void Awake()
    {
        _selected = false;
        _transform = this.transform;
        _CharImg = this.GetComponent<Image>();
        _CharImg.sprite = Resources.Load<Sprite>("portraitGRILL");
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Select()
    {
        _selected = !_selected;
    }
    public void setImage(string imagePath) 
    {
        Debug.Log(Resources.Load<Image>(imagePath));
        _CharImg = Resources.Load<Image>(imagePath);
    }
}
