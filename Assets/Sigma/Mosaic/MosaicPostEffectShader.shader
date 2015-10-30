Shader "Custom/MosaicPostEffectShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		ZTest Always
		ZWrite off
		
		//filter : 0
		Pass
		{		
			Stencil {
				Ref 1
				Comp Equal
				Pass Keep
				ZFail Keep
			}
			
			CGPROGRAM
			
			#pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc"  
  
            struct appdata_t {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;  
            };  
  
            struct v2f {  
                float4 vertex : SV_POSITION;  
                half2 texcoord : TEXCOORD0;  
            };  
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST; 
	        
	        v2f vert(appdata_t v) {
	        	v2f o;  
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);  
                  
                return o;  
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        
	        	fixed4 col = tex2D(_MainTex, i.texcoord);  
	        	
	        	//UNITY_OPAQUE_ALPHA(col.a);  
              
                return col;  
	        }
			ENDCG
		}
		//Mosaic : 1
		Pass {
				
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc" 
	        
	        struct appdata_t {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;  
            };
	        struct v2f {  
                float4 vertex : SV_POSITION;  
                half2 texcoord : TEXCOORD0;  
            };  
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST; 
	        half4 _MainTex_TexelSize;
	        
	        v2f vert(appdata_t v) {
	        	v2f o;
	        	
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);  
	            return o;
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        
	        //	float2 uv = (i.texcoord*_MainTex_TexelSize.zw) ;  
            //    uv = floor(uv/4.0f)*4;  
            //    i.texcoord = uv*_MainTex_TexelSize.xy;  
            //    fixed4 col = tex2D(_MainTex, i.texcoord);  
              
            //    UNITY_OPAQUE_ALPHA(col.a);  
              
            //    return col; 
                
                
                const float TR = 0.866025f;//TR=√3  
				float2 xyUV = (i.texcoord*_MainTex_TexelSize.zw);  
				int wx = int (xyUV.x/1.5f/4.0f);  
				int wy = int (xyUV.y/TR/4.0f);  
				  
				float2 v1,v2;  
				float2 wxy =float2(wx,wy);  
				if(wx/2*2==wx){  
				    if(wy/2*2==wy){  
				        v1 = wxy;  
				        v2 = wxy+1;  
				    }  
				    else{  
				        v1 = wxy+float2(0,1);  
				        v2 = wxy+float2(1,0);  
				    }     
				}  
				else{  
				    if(wy/2*2 == wy){  
				        v1 = wxy+float2(0,1);  
				        v2 = wxy+float2(1,0);  
				    }  
				    else{  
				        v1 = wxy;  
				        v2 = wxy+1;  
				    }  
				}  
				v1 *= float2(4.0f*1.5f,4.0f*TR);  
				v2 *= float2(4.0f*1.5f,4.0f*TR);  
				  
				float s1 = length(v1.xy-xyUV.xy);  
				float s2 = length(v2.xy-xyUV.xy);  
				fixed4 col = tex2D(_MainTex,v2*_MainTex_TexelSize.xy);  
				if(s1 < s2)    
				    col = tex2D(_MainTex,v1*_MainTex_TexelSize.xy);
				    
				return col;
	        }
			ENDCG
		}		
		//Post Background to output : 2
		Pass {
		
			Stencil {
				Ref 1
				Comp NotEqual
				Pass Keep
				ZFail Keep
			}
				
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc" 
	        
	        struct appdata_t {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;  
            };
	        struct v2f {  
                float4 vertex : SV_POSITION;  
                half2 texcoord : TEXCOORD0;  
            };  
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST; 
	        
	        v2f vert(appdata_t v) {
	        	v2f o;
	        	
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);  
	            return o;
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        	return tex2D(_MainTex, i.texcoord);  
	        }
			ENDCG
		}
		//Post Target to output : 3
		Pass {
		
			Stencil {
				Ref 1
				Comp Equal
				Pass Keep
				ZFail Keep
			}
				
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc" 
	        
	        struct appdata_t {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;  
            };
	        struct v2f {  
                float4 vertex : SV_POSITION;  
                half2 texcoord : TEXCOORD0;  
            };  
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST; 
	        
	        v2f vert(appdata_t v) {
	        	v2f o;
	        	
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);  
	            return o;
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        	return tex2D(_MainTex, i.texcoord);  
	        }
			ENDCG
		}
		//Post output to screen : 4
		Pass {
						
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

	        #include "UnityCG.cginc" 
	        
	        struct appdata_t {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;                    
            };
	        struct v2f {  
                float4 vertex : SV_POSITION;  
                half2 texcoord : TEXCOORD0;  
            };  
	        
	        sampler2D _MainTex;
	        float4 _MainTex_ST; 
	        
	        v2f vert(appdata_t v) {
	        	v2f o;
	        	
	            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);  
	            return o;
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        	return tex2D(_MainTex, i.texcoord);  
	        }
			ENDCG
		}
		// Test Code : 5
		Pass {
		
			Stencil {
				Ref 1
				Comp Equal
				Pass Keep
				ZFail Keep
			}
		
			CGPROGRAM
			#pragma vertex vert
	        #pragma fragment frag

	        
	        struct appdata {
	        	float4 vertex : POSITION;
	        };
	        struct v2f {
	        	float4 pos : POSITION;
	        };
	        
	        fixed4 _Color;
	        
	        v2f vert(appdata v) {
	        	v2f o;
	            o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
	            return o;
	        }
	        
	        fixed4 frag(v2f i) : COLOR {
	        	return fixed4(1,0,0,1);
	        }
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
