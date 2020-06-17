using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast (ray,out hit,100.0f)) 
            {
                if (hit.transform.name == "CellBody")
                {
                    hit.transform.gameObject.GetComponentInParent<Propagate>().OnPropagate(Random.Range(0.5f, 1f));
                }
            }
        }
    }
}
