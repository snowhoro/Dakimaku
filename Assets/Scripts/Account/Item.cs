using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {

    public string ItemID;
    public string CharacterMaxID;
    private bool _selected;
    public bool Selected { get { return _selected; } set { _selected = value; } }
    public bool _canEvolve = false;
    public Button itemButton;    

    public Character _character { get; private set; }
    public Image _CharImg;
    public Transform _transform;
    public Animator _animator;

    public void Initialize(string name, int baseHP, int level, int rarity, int baseMAtt, int basePAtt, int baseMDef, int basePDef, string itemID, int experience, string maxCharID, string portrait, string sprite, List<string> skills)
    {
        //Debug.Log("Item " + baseHP + " " + baseMAtt + " " + basePAtt + " " + baseMDef + " " + basePDef + " ");

        Image imageCo = this.GetComponent<Image>();
        imageCo.sprite = LoadAsset.Portrait(portrait);

        Sprite charSprite = LoadAsset.CharacterSprite(sprite);
        _character = gameObject.AddComponent<Character>();
        _character.Initialize(name, baseHP, level, rarity, baseMAtt, basePAtt, baseMDef, basePDef, experience, portrait, charSprite, itemID);
        foreach (string skill in skills)
        {
            if ( skill != "HealI" && skill != "HealII" && skill != "RegenI")
                _character._skillList.Add(BaseCharacter.AddSkill(skill));
        }
        
        _CharImg = imageCo;
        ItemID = itemID;
        CharacterMaxID = maxCharID;
    }

    void Awake()
    {
        _selected = false;
        _transform = this.transform;
        _animator = GetComponent<Animator>();
        itemButton = GetComponent<Button>();

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Select()
    {
        _selected = !_selected;
        _animator.SetTrigger("Selected");
    }

    public void setImage(string imagePath) 
    {
        //_CharImg.sprite = Resources.Load<Sprite>(imagePath);
        _CharImg.sprite = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
    }

    public void ItemClick()
    {
        UiController.Instance.ItemClick(this);
        //_animator.SetTrigger("Selected");
    }
    public bool IsMaxLevel()
    {
        return _character.MaxLevelCheck();
    }
}
