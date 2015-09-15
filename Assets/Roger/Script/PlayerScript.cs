using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	bool isGetBall;
	public bool isSendTarget;
	public Transform snowball;

	// Use this for initialization
	void Start () {
	}

	void Awake() {
		isGetBall = false;
		isSendTarget = false;
	}

	// Update is called once per frame
	void Update () {

		float offset = 15;
		float dis = Vector3.Distance (transform.position, snowball.transform.position);

		if (Input.GetAxisRaw ("Horizontal") > 0) 
		{
			Debug.Log ("按下了右鍵");
			Debug.Log ("" + Input.GetAxisRaw ("Horizontal"));

			var speed = Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
			transform.Translate (0, 0, speed * offset);
			//gameObject.renderer.material.color = Color.red;
		} 
		else if (Input.GetAxisRaw ("Horizontal") < 0) {
			Debug.Log ("按下了左鍵");
			var speed = Input.GetAxisRaw ("Horizontal") * Time.deltaTime;
			transform.Translate (0, 0, speed * offset);
			//gameObject.renderer.material.color = Color.green;
		}

		if (Input.GetAxisRaw ("Vertical") < 0) {
			Debug.Log ("按下了上鍵");
			var speed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;
			transform.Translate (-speed * offset, 0, 0);
			//gameObject.renderer.material.color = Color.blue;
		}else if (Input.GetAxisRaw ("Vertical") > 0) {
			Debug.Log ("按下了下鍵");
			var speed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;
			transform.Translate (-speed * offset, 0, 0);
			//gameObject.renderer.material.color = Color.yellow;
		}

		if (Input.GetKeyDown ("space")) {
			//send ball

			if(isGetBall)
			{
				//			/* 丟球
				snowball.GetComponent<Rigidbody>().isKinematic = false;
				
				snowball.transform.parent = null;
				snowball.transform.position = transform.position - Vector3.forward;
				snowball.GetComponent<Rigidbody>().velocity = Vector3.zero;
				snowball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
				Vector3 temp = Vector3.right;
				snowball.GetComponent<Rigidbody>().AddForce(temp * 100); 
			}
			else if(dis < 2)
			{
				//get ball

				isGetBall = true;
				snowball.GetComponent<Rigidbody>().AddForce(0, 0, 0);
				snowball.GetComponent<Rigidbody>().isKinematic = false;
				snowball.transform.position = transform.position + transform.forward * -1;
				snowball.transform.parent = transform;
			}
		}
	}
}
