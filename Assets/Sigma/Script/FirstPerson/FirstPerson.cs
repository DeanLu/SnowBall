using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class FirstPerson : MonoBehaviour 
{
	Rigidbody mRigidbody = null;

	Vector3 mMousePos = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		mRigidbody = GetComponent<Rigidbody>();

		mRigidbody.freezeRotation = true;

		mMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Movement ();

		Shoot ();
	}

	void Movement ()
	{
		float movForward = 0F;
		float movRight = 0F;
		
		if (Input.GetKey (KeyCode.W) == true)
			movForward += 1F;
		
		if (Input.GetKey (KeyCode.S) == true)
			movForward -= 1F;
		
		if (Input.GetKey (KeyCode.A) == true)
			movRight -= 1F;
		
		if (Input.GetKey (KeyCode.D) == true)
			movRight += 1F;
		
		mRigidbody.velocity = this.transform.TransformDirection( new Vector3 (movRight, 0F, movForward) );
		
		
		
		
		Vector3 lastMousePos = Input.mousePosition;
		
		Vector3 diff = lastMousePos - mMousePos;
		
		float Yaw = (diff.x / (Screen.width >> 1)) * 360F;
		
		this.transform.rotation = this.transform.rotation * Quaternion.Euler (0F, Yaw, 0F);
		
		
		float Pitch = (diff.y / (Screen.height >> 1)) * -30F;
		
		Camera.main.transform.rotation = Camera.main.transform.rotation * Quaternion.Euler (Pitch, 0F, 0F);
		
		mMousePos = lastMousePos;
	}

	void Shoot ()
	{
		if (Input.GetMouseButton (1)) 
		{
			GameObject snowBall = GameObject.Instantiate(Resources.Load("SnowBall")) as GameObject;
			if (snowBall == null) return;

			Physics.IgnoreCollision(this.GetComponent<Collider>(), snowBall.GetComponent<Collider>());

			snowBall.transform.position = this.transform.position + (this.transform.up * 0.5F) + (this.transform.forward * 0.5F);
			snowBall.transform.rotation = Quaternion.LookRotation(this.transform.forward);
			
			Rigidbody rigidbody = snowBall.GetComponent<Rigidbody>();
			if (rigidbody == null) rigidbody = snowBall.AddComponent<Rigidbody>();
			
			//Vector3 throwForce = (this.transform.up * Random.Range(-0.5F,2F)) + 
			//	(this.transform.forward * Random.Range(0F,2F)) + 
			//		(this.transform.right * Random.Range(-0.25F,0.25F));

			Vector3 throwForce = Camera.main.transform.forward + (Camera.main.transform.up * 0.5F);
			
			
			rigidbody.AddForce(throwForce, ForceMode.Impulse);
		}
	}
}
