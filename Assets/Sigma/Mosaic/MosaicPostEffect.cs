using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MosaicPostEffect : MonoBehaviour 
{
	Material mMosaicMaterial = null;
	Material MosaicMaterial  
	{  
		get  
		{  
			if(mMosaicMaterial == null)  
			{  
				mMosaicMaterial = new Material(Shader.Find("Custom/MosaicPostEffectShader"));  
				mMosaicMaterial.hideFlags = HideFlags.HideAndDontSave;     
			}  
			return mMosaicMaterial;  
		}  
	}

	Camera mCamera = null;

	RenderTexture mInput = null;

	RenderTextureFormat mFormat = RenderTextureFormat.Default;

	int mResult = 0;


	public void Start ()
	{
		mCamera = GetComponent<Camera>();

		//if(SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf)){
		//	mFormat = RenderTextureFormat.ARGBHalf;
		//}
		
		mInput = new RenderTexture(Screen.width, Screen.height, 24, mFormat);
		mInput.Create();
		mInput.name = "Input";

	}

	public void OnDisable () 
	{  
		if (mMosaicMaterial)  
		{
			DestroyImmediate (mMosaicMaterial);  
			mMosaicMaterial = null;
		}
	} 

	public void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Alpha0))
		{
			mResult = 0;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			mResult = 1;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			mResult = 2;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			mResult = 3;
		}
	}

	void OnPreRender() 
	{
		mCamera.targetTexture = mInput;
	}

	public void OnPostRender ()
	{
		//Mosaic
		RenderTexture mosaicTarget = RenderTexture.GetTemporary (Screen.width, Screen.height, 0, mFormat);
		
		Graphics.SetRenderTarget (mosaicTarget);
		
		Graphics.Blit(mInput, MosaicMaterial, 1);

		//filter Target
		RenderTexture filterTarget = RenderTexture.GetTemporary (Screen.width, Screen.height, 0, mFormat);
		
		Graphics.SetRenderTarget (filterTarget);
		
		GL.Clear (true, true, new Color (0F, 0F, 0F, 0F));
		
		Graphics.SetRenderTarget (filterTarget.colorBuffer, mInput.depthBuffer);
		
		Graphics.Blit(mosaicTarget, MosaicMaterial, 0);
		/*
		//filter Target
		RenderTexture filterTarget = RenderTexture.GetTemporary (Screen.width, Screen.height, 0, mFormat);

		Graphics.SetRenderTarget (filterTarget);

		GL.Clear (true, true, new Color (0F, 0F, 0F, 0F));

		Graphics.SetRenderTarget (filterTarget.colorBuffer, mInput.depthBuffer);

		Graphics.Blit(mInput, MosaicMaterial, 0);


		//Mosaic
		RenderTexture mosaicTarget = RenderTexture.GetTemporary (Screen.width, Screen.height, 0, mFormat);

		Graphics.SetRenderTarget (mosaicTarget);

		Graphics.Blit(filterTarget, MosaicMaterial, 1);
		*/



		//Post background to output
		RenderTexture outputTarget = RenderTexture.GetTemporary (Screen.width, Screen.height, 0, mFormat);

		Graphics.SetRenderTarget (outputTarget.colorBuffer, mInput.depthBuffer);

		Graphics.Blit(mInput, MosaicMaterial, 2);


		//Post target to output	
		Graphics.Blit(mosaicTarget, MosaicMaterial, 3);

		//Graphics.Blit(mosaicTarget, MosaicMaterial, 4);


		//Post output to screen	
		Graphics.SetRenderTarget (null);

		//Graphics.Blit(outputTarget, MosaicMaterial, 4);

		switch(mResult)
		{
		case 0:
			Graphics.Blit(outputTarget, MosaicMaterial, 4);
			break;

		case 1:
			Graphics.Blit(mInput, MosaicMaterial, 4);
			break;

		case 2:
			Graphics.Blit(mosaicTarget, MosaicMaterial, 4);
			break;

		case 3:
			Graphics.Blit(filterTarget, MosaicMaterial, 4);
			break;
		}


		RenderTexture.ReleaseTemporary (outputTarget);
		RenderTexture.ReleaseTemporary (mosaicTarget);
		RenderTexture.ReleaseTemporary (filterTarget);



		//Graphics.SetRenderTarget (null);
		mCamera.targetTexture = null;

	}
}
