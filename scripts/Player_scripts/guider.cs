using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guider : MonoBehaviour {
    public List<Vector3> webPath;
    public LineRenderer path;
    // Use this for initialization
    void Start () {
        path = gameObject.GetComponent<LineRenderer>();
        webPath.Add(transform.position);
	}
	
	// Update is called once per frame
	void Update()
    {


        /*
        for (int j = 0; j < paths.Count; j++)
        {
            
        }*/

        path.positionCount = webPath.Count;
        
        for (int i = 0; i < webPath.Count; i++)
        {
            path.SetPosition(i, webPath[i]);
        }
	}
    public void addposition(Vector3 point)
    {
        webPath.Add(point);
    }

}
