using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    private int count = 0;
	// Update is called once per frame
	void Update () {
        count++;
        Vector3 pos = transform.position;
        if(((int)(count/100)) % 2 == 0)
            pos.y += 0.01f;
        else
            pos.y -= 0.01f;
        transform.position = pos;
    }
}
