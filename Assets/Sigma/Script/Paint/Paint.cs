using UnityEngine;
using System.Collections;

public class Paint : MonoBehaviour {
	
	[SerializeField]
	Material mMatPen = null;
	
	[SerializeField]
	RenderTexture mRenderTex = null;
	
	[SerializeField]
	Collider mTarget = null;
	
	// Use this for initialization
	void Start () {
			
		Clear (mRenderTex, mMatPen);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetMouseButtonDown (0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			RaycastHit hit;
			
			if(mTarget.Raycast(ray, out hit, 100F))
			{
				//Vector3 localPos = mTarget.transform.InverseTransformPoint(hit.point);
				//PaintTexture(mRenderTex, mMatPen, 0.3F, localPos.x + 0.5F, localPos.y + 0.5F);
				
				Vector2 localPos = hit.textureCoord;
				PaintTexture(mRenderTex, mMatPen, 0.3F, localPos.x, localPos.y);				
			}
		}
	}
	
	void PaintTexture(RenderTexture _canvas, Material _brush, float _size, float _posX, float _posY)
	{
		Debug.Log (string.Format("_size = {0}, _posX = {1}, _posY = {2}",_size,_posX,_posY));
		
		float halfSize = _size / 2F;
		
		_canvas.MarkRestoreExpected ();
		Graphics.SetRenderTarget(_canvas);
		
		GL.LoadOrtho();
		
		GL.Begin(GL.QUADS);
		
		_brush.SetPass (0);
		
		GL.TexCoord(new Vector3(0, 0, 0));
		GL.Vertex3(_posX - halfSize, _posY - halfSize, 0);
		
		GL.TexCoord(new Vector3(0, 1, 0));
		GL.Vertex3(_posX - halfSize, _posY + halfSize, 0);
		
		GL.TexCoord(new Vector3(1, 1, 0));
		GL.Vertex3(_posX + halfSize, _posY + halfSize, 0);
		
		GL.TexCoord(new Vector3(1, 0, 0));
		GL.Vertex3(_posX + halfSize, _posY - halfSize, 0);
		
		GL.End();
		
		Graphics.SetRenderTarget(null);
	}
	
	void Clear(RenderTexture _canvas, Material _brush)
	{		
		_canvas.MarkRestoreExpected ();
		Graphics.SetRenderTarget(_canvas);
		
		GL.Clear(false, true, Color.clear);
		
		Graphics.SetRenderTarget(null);
	}
}
