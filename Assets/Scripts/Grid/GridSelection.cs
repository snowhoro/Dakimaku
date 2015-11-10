using UnityEngine;
using System.Collections;

public class GridSelection : MonoBehaviour {

    private GameObject selected;
 
	void Start () 
    {
	
	}
	
	void Update () 
    {
	    if(Input.GetMouseButtonUp(0))
        {
            //Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null)
            {
                //Debug.Log(hit.transform.position);
                if (hit.collider.tag == "Hero" || hit.collider.tag == "Enemy")
                {
                    if(selected != null)
                        selected.transform.localScale = new Vector3(0.85f, 0.85f, 1f);

                    hit.transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
                    selected = hit.transform.gameObject;
                }
                else
                {
                    if (selected != null)
                    {
                        Vector2 selectPos = GridManager.instance.GetGridPosition(selected.transform.position);
                        Vector2 hitPos = GridManager.instance.GetGridPosition(hit.transform.position);

                        //UPDATE GRID POSITIONS
                        GridManager.instance.UpdateMapPositions(selected.GetComponent<BaseCharacter>());

                        selected.GetComponent<GridMovement>().SetPath(AStar.AStarSearch(selectPos, hitPos));
                        selected.transform.localScale = new Vector3(0.85f, 0.85f, 1f);
                        selected = null;
                        /*GameObject charac = (GameObject)Instantiate(selected, hit.transform.position - Vector3.forward, Quaternion.identity);
                        charac.transform.localScale = new Vector3(0.85f, 0.85f, 1f);*/
                    }
                }
            }
        }
	}
}
