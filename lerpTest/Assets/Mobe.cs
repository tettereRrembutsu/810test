using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobe : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow)) { transform.position += new Vector3(0, 0.1f, 0); }

        if (Input.GetKey(KeyCode.DownArrow)) { transform.position -= new Vector3(0, 0.1f, 0); }

        if (Input.GetKey(KeyCode.RightArrow)) { transform.position += new Vector3(0.1f, 0, 0); }

        if (Input.GetKey(KeyCode.LeftArrow)) { transform.position -= new Vector3(0.1f, 0, 0); }

        if (Input.GetKey(KeyCode.Alpha1)) { transform.position += new Vector3(0, 0, 0.1f); }

        if (Input.GetKey(KeyCode.Alpha2)) { transform.position -= new Vector3(0, 0, 0.1f); }
    }
}
