using UnityEngine;
using System.Collections;

public class PaintBox : PaintFrame 
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
			
			Vector3 localPos = this.transform.InverseTransformPoint(contact.point);
			
			localPos.x += 0.5F;
			localPos.y += 0.5F;

			float brushSize = (transform.localScale.x + transform.localScale.y) / 2F;

			brushSize = brushSize == 0F ? 0.001F : BALL_SIZE / Mathf.Abs(brushSize);

			PaintTexture(brushSize, localPos.x, localPos.y);
		}
	}
}