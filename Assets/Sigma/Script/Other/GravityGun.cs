using UnityEngine;

public class GravityGun : MonoBehaviour
{
	/*
     * Class members
     */
	public float m_Force = 80.0f;
	public float m_Gravity = 1.0f;
	public float m_Radius = 10.0f;
	public float m_ScaleFactor = 10.0f;
	public Transform m_RayTarget;
	public Transform m_RayLight;
	public float m_RaySpeed = 1.0f;
	public float m_RayScale = 1.0f;
	public int m_RayZigs = 100;
	private Perlin m_Noise;
	private Particle[] m_Particles;
	private float m_RayZigsAdded;
	private GameObject m_TempObject;
	
	
	/*
     * Private start class method
     */
	private void Start()
	{
		// Set particle member fields
		this.m_RayZigsAdded = 1.0f / (float)this.m_RayZigs;
		this.GetComponent<ParticleEmitter>().Emit();
		this.m_Particles = this.GetComponent<ParticleEmitter>().particles;
		
		// Disable ray light
		this.m_RayLight.GetComponent<Light>().enabled = false;
	}
	
	
	/*
     * Private update class method
     */
	public void Update()
	{
		if(Input.GetMouseButtonUp(0) == true)
		{
			// Iterate through all gravity objects
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("Gravity Object"))
			{
				// Set temporary variable
				this.m_TempObject = go;
				
				// Calculate distance to game object
				if(Vector3.Distance(go.transform.position, this.transform.position) < this.m_Radius / 4.5f)
				{
					// Apply force to game object
					go.GetComponent<Rigidbody>().angularDrag = 2.0f;
					go.GetComponent<Rigidbody>().AddForce(this.transform.rotation * new Vector3(0, 0, this.m_Force * this.m_ScaleFactor));
				}
			}
			
			// Disable ray light
			this.m_RayLight.GetComponent<Light>().enabled = false;
		}
		
		// Check if temporary game object is set
		if(this.m_TempObject != null)
		{
			// Calculate distance to game object
			if(Vector3.Distance(this.m_TempObject.transform.position, this.transform.position) > this.m_Radius)
			{
				// Disable ray light
				this.m_RayLight.GetComponent<Light>().enabled = false;
			}
		}
	}
	
	
	/*
     * Private fixed update class method
     */
	public void FixedUpdate()
	{
		// Check if left mouse button is down
		if(Input.GetMouseButton(0) == true)
		{
			// Iterate through all gravity objects
			foreach(GameObject go in GameObject.FindGameObjectsWithTag("Gravity Object"))
			{
				go.GetComponent<Rigidbody>().AddExplosionForce(-this.m_Gravity * this.m_ScaleFactor, GameObject.Find("Atractor").transform.position, this.m_Radius);
				go.GetComponent<Rigidbody>().angularDrag = 30.0f;
				go.GetComponent<Rigidbody>().velocity *= 0.97f;
				
				// Calculate distance to game object
				if(Vector3.Distance(go.transform.position, this.transform.position) < this.m_Radius)
				{
					// Create ray particles
					this.CreateRay();
					
					// Enable ray light
					this.m_RayLight.GetComponent<Light>().enabled = true;
				}
			}
		}
	}
	
	
	/*
     * Private create ray class method
     */
	private void CreateRay()
	{
		if(this.m_Noise == null)
			this.m_Noise = new Perlin();
		
		float timex = Time.time * this.m_RaySpeed * 0.1365143f;
		float timey = Time.time * this.m_RaySpeed * 1.21688f;
		float timez = Time.time * this.m_RaySpeed * 2.5564f;
		
		for (int i=0; i < this.m_Particles.Length; i++)
		{
			Vector3 StartingOffset = new Vector3(this.transform.position.x + 0.3f, this.transform.position.y - 0.3f, this.transform.position.z);
			Vector3 StartingPosition = Vector3.Lerp(StartingOffset, this.m_RayTarget.position, this.m_RayZigsAdded * (float)i);
			Vector3 OffsetPosition = new Vector3(this.m_Noise.Noise(timex + StartingPosition.x, timex + StartingPosition.y, timex + StartingPosition.z),
			                                     this.m_Noise.Noise(timey + StartingPosition.x, timey + StartingPosition.y, timey + StartingPosition.z),
			                                     this.m_Noise.Noise(timez + StartingPosition.x, timez + StartingPosition.y, timez + StartingPosition.z));
			StartingPosition += (OffsetPosition * this.m_RayScale * ((float)i * this.m_RayZigsAdded));
			
			this.m_Particles[i].position = StartingPosition;
			this.m_Particles[i].color = Color.white;
			this.m_Particles[i].energy = 1f;
		}
		
		GetComponent<ParticleEmitter>().particles = this.m_Particles;
	}
}