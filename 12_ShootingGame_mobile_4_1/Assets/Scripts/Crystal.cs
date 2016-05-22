using UnityEngine;
using System.Collections;

public class Crystal : MonoBehaviour {

    public int speed = -5;

	// Use this for initialization
	void Start () {

        // 下方向に移動
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	}

	// Update is called once per frame
	void Update () {

	}
}
