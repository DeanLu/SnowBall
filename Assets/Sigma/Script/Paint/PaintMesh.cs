using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
public class PaintMesh : PaintFrame 
{
	const float BALL_SIZE = 0.3F;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag ("SnowBall") == false)
			return;

		Destroy(collision.gameObject);

		for (int Indx = 0; Indx < collision.contacts.Length; ++Indx) 
		{
			ContactPoint contact = collision.contacts[Indx];

			Ray ray = new Ray (contact.point - contact.normal * 0.05F, contact.normal);
			
			RaycastHit hit;

			if(contact.thisCollider.Raycast(ray, out hit, 0.1F))
			{
				PaintTexture((BALL_SIZE * 2)/ (transform.localScale.x + transform.localScale.y),hit.textureCoord.x,hit.textureCoord.y);
			}
		}
	}
}
