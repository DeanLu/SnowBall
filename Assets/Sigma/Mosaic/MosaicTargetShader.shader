Shader "Custom/MosaicTargetShader"
{
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue"="Geometry-1" }
		LOD 200
		
		Pass
		{
			ZTest LEqual 
			ZWrite off
			Cull off
					
			Stencil {
				Ref 1
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}
			
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            struct appdata {
                float4 vertex : POSITION;
            };
            struct v2f {
                float4 pos : SV_POSITION;
            };
            v2f vert(appdata_base v) {
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            half4 frag(v2f i) : COLOR {
                return half4(1,0,0,1);
            }
            ENDCG
		}
	}
	FallBack "Diffuse"
}