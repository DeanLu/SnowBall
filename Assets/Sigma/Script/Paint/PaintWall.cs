using UnityEngine;
using System.Collections;

public class PaintWall : MonoBehaviour 
{
	[SerializeField]
	RenderTexture mPaintTexture = null;

	// Use this for initialization
	void Start () 
	{
		if(PaintTool.Instance != null && mPaintTexture != null) PaintTool.Instance.Clear(mPaintTexture);
	}

	void OnCollisionEnter(Collision collision)
	{
		Destroy(collision.gameObject);

		if(PaintTool.Instance != null && mPaintTexture != null) 
		{
			foreach (ContactPoint contact in collision.contacts) 
			{
				//Debug.Log("contact.point1 = " + contact.point.ToString());
				Vector3 localPos = this.transform.InverseTransformPoint(contact.point);
				//Debug.Log("localPos1 = " + localPos.ToString());
				localPos.x += 0.5F;
				localPos.y += 0.5F;
				//Debug.Log("localPos2 = " + localPos.ToString());
				float brushSize = (transform.localScale.x + transform.localScale.y) / 2F;
				//Debug.Log("brushSize1 = " + brushSize.ToString());
				brushSize = brushSize == 0F ? 0.001F : 0.3F / Mathf.Abs(brushSize);
				//Debug.Log("brushSize2 = " + brushSize.ToString());
				PaintTool.Instance.PaintTexture(mPaintTexture, 0.3F, localPos.x, localPos.y);
			}


			//PaintTexture(mRenderTex, mMatPen, 0.3F, localPos.x + 0.5F, localPos.y + 0.5F);
		}
	}
}
