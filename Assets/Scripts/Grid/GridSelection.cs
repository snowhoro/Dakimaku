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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null)
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Hero" || hit.collider.tag == "Enemy")
                {
                    if(selected != null)
                        selected.GetComponent<Animator>().SetTrigger("Selected");

                    selected = hit.transform.gameObject;
                    selected.GetComponent<Animator>().SetTrigger("Selected");
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
                        selected.GetComponent<Animator>().SetTrigger("Selected");
                        selected = null;
                    }
                }
            }
        }
	}
}
