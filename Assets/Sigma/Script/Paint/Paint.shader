Shader "Unlit/Paint"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "white"
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZTest Always
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 
		LOD 100

		Pass {
            SetTexture [_MainTex] 
      	}
      	
      	Pass {
            Color (1, 1, 1, 1)
      	}
	}
}
