using UnityEngine;
using System.Collections;

public class PaintTool : MonoBehaviour 
{
	static PaintTool mInstance = null;
	static public PaintTool Instance { get { return mInstance; } }

	[SerializeField]
	Material mMatPen = null;

	// Use this for initialization
	void Awake () 
	{
		if(mInstance != null)
		{
			Destroy(this);
		}
		else
		{
			mInstance = this;
		}
	}

	void OnDestroy ()
	{
		if(mInstance == this)
		{
			mInstance = null;
		}
	}

	public void PaintTexture(RenderTexture _canvas, float _size, float _posX, float _posY)
	{
		Debug.Log (string.Format("_size = {0}, _posX = {1}, _posY = {2}",_size,_posX,_posY));
		
		float halfSize = _size / 2F;
		
		//_canvas.MarkRestoreExpected ();
		Graphics.SetRenderTarget(_canvas);
		
		//GL.PushMatrix();
		
		GL.LoadOrtho();
		//GL.LoadIdentity();
		
		GL.Begin(GL.QUADS);
		
		mMatPen.SetPass (0);
		
		GL.TexCoord(new Vector3(0, 0, 0));
		//GL.Vertex3(_posX - halfSize, _posY - halfSize, 0);
		GL.Vertex3(0, 0, 0);
		
		GL.TexCoord(new Vector3(0, 1, 0));
		//GL.Vertex3(_posX - halfSize, _posY + halfSize, 0);
		GL.Vertex3(0, 1, 0);
		
		GL.TexCoord(new Vector3(1, 1, 0));
		//GL.Vertex3(_posX + halfSize, _posY + halfSize, 0);
		GL.Vertex3(1, 1, 0);
		
		GL.TexCoord(new Vector3(1, 0, 0));
		//GL.Vertex3(_posX + halfSize, _posY - halfSize, 0);
		GL.Vertex3(1, 0, 0);
		
		GL.End();
		
		//GL.PopMatrix();
		
		Graphics.SetRenderTarget(null);
	}
	
	public void Clear(RenderTexture _canvas)
	{		
		_canvas.MarkRestoreExpected ();
		Graphics.SetRenderTarget(_canvas);
		
		GL.Clear(false, true, Color.clear);
		
		Graphics.SetRenderTarget(null);
	}
}
