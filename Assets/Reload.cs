using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Reload : MonoBehaviour {
	public GameObject sphere;
    Vector3 position;
    void Start()
    {
        position = sphere.transform.position;
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R))
        {
            sphere.transform.position = position;
            sphere.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
            sphere.GetComponent<Rigidbody>().angularVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        }
	}
}
