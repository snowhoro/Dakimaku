using UnityEngine;
using System.Collections;

public class GachaUIController : MonoBehaviour {

    public Transform GachaParent;

    Vector3 vector1 = new Vector3(1, 1, 1);
    
	void Awake () {
        for (int i = 0; i < Game.Instance._gachaItems.Count; i++)
        {
            Game.Instance._gachaItems[i]._transform.parent = GachaParent;
            Game.Instance._gachaItems[i]._transform.localPosition = Vector3.zero;
            Game.Instance._gachaItems[i]._transform.localScale = vector1;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
