using UnityEngine;
using System.Collections;

public class PaintFrame : MonoBehaviour 
{
	const string PEN_TEXTURE = "NPa14";

	[SerializeField]
	RenderTexture mRenderTex = null;

	static Material mPen = null;
	static Material Pen
	{
		get
		{
			if(mPen == null)
			{
				mPen = new Material(Shader.Find("Unlit/Paint"));
				mPen.SetTexture("_MainTex",Resources.Load(PEN_TEXTURE) as Texture);
			}
			return mPen;
		}
	}

	// Use this for initialization
	void Start () 
	{
		Initial ();	
	}

	void OnDestroy()
	{
		if (mRenderTex != null)
		{
			mRenderTex.Release();
			mRenderTex = null;
		}
	}

	void Initial()
	{
		if (mRenderTex == null)
		{
			mRenderTex = new RenderTexture(256,256,16,RenderTextureFormat.ARGB32);
			mRenderTex.Create();

			Renderer render = GetComponent<Renderer>();
			if(render != null)
			{
				if(render.material.HasProperty("_BlendTex") == true)
					render.material.SetTexture("_BlendTex",mRenderTex);
			}
		}

		Clear ();
	}

	protected void PaintTexture(float _size, float _posX, float _posY)
	{
		Debug.Log (string.Format("_size = {0}, _posX = {1}, _posY = {2}",_size,_posX,_posY));
		
		float halfSize = _size / 2F;

		Graphics.SetRenderTarget(mRenderTex);
		
		GL.PushMatrix();
		
		Pen.SetPass (0);
		
		GL.LoadOrtho();
		
		GL.Begin(GL.QUADS);		
		
		
		GL.TexCoord(new Vector3(0, 0, 0));
		GL.Vertex3(_posX - halfSize, _posY - halfSize, 0);
		
		GL.TexCoord(new Vector3(0, 1, 0));
		GL.Vertex3(_posX - halfSize, _posY + halfSize, 0);
		
		GL.TexCoord(new Vector3(1, 1, 0));
		GL.Vertex3(_posX + halfSize, _posY + halfSize, 0);
		
		GL.TexCoord(new Vector3(1, 0, 0));
		GL.Vertex3(_posX + halfSize, _posY - halfSize, 0);
		
		
		GL.End();
		
		GL.PopMatrix();
		
		Graphics.SetRenderTarget(null);
	}
	
	void Clear()
	{		
		mRenderTex.MarkRestoreExpected ();
		Graphics.SetRenderTarget(mRenderTex);
		
		GL.Clear(false, true, Color.clear);
		
		Graphics.SetRenderTarget(null);
	}
}
