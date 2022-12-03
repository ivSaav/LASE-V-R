Shader "Custom/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 200
        
        HLSLINCLUDE
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		ENDHLSL
        
        Pass {
            Name "Portal"
            
            Stencil {
                Ref 1
                Pass replace
            }
            
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            struct vertexData
            {
                float4 vertex: POSITION;
            };

            struct v2f
            {
                float4 vertex: SV_POSITION;
                float4 screenPos: TEXCOORD0;
            };

            uniform sampler2D _MainTex;

            v2f vert (vertexData v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            float4 frag(v2f i): SV_Target {
                float2 uv = i.screenPos.xy / i.screenPos.w;
                float4 target = tex2D(_MainTex, uv);
                return target;
            }
            
            ENDHLSL
        }
    }
}
