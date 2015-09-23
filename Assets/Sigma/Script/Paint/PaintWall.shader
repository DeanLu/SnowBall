Shader "Custom/PaintWall" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _BlendTex ("Alpha Blended (RGBA) ", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM  
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _BlendTex;

		struct Input {  
		    float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {  
		    half4 c1 = tex2D (_MainTex, IN.uv_MainTex);
		    half4 c2 = tex2D (_BlendTex, IN.uv_MainTex);
		    o.Albedo = lerp (c1.rgb, c2.rgb, c2.a);
		    o.Alpha = 1;
		}
		ENDCG  
	}
}
