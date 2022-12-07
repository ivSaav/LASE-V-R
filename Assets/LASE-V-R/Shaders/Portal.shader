Shader "Custom/Portal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }

		CGINCLUDE

		// Pragmas --------------------------------------------------------------------------------------------------------------------------------------------------
#pragma target 5.0
#if UNITY_VERSION >= 560
#pragma only_renderers d3d11 vulkan glcore
#else
#pragma only_renderers d3d11 glcore
#endif
#pragma exclude_renderers gles
#pragma multi_compile_instancing

// Includes -------------------------------------------------------------------------------------------------------------------------------------------------
#include "UnityCG.cginc"

// Structs --------------------------------------------------------------------------------------------------------------------------------------------------
struct VertexInput
	{
		float4 vertex : POSITION;

#if UNITY_VERSION >= 560
		UNITY_VERTEX_INPUT_INSTANCE_ID
#endif
	};

	struct VertexOutput
	{
		float4 vertex : SV_POSITION;
		float4 screenPos: TEXCOORD0;

#if UNITY_VERSION >= 560
		UNITY_VERTEX_OUTPUT_STEREO
#endif
	};

	// Globals --------------------------------------------------------------------------------------------------------------------------------------------------
	UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(sampler2D, _MainTex)
	UNITY_INSTANCING_BUFFER_END(Props)

	VertexOutput MainVS(VertexInput i) {
		VertexOutput o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(VertexOutput, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

		#if UNITY_VERSION >= 540
			o.vertex = UnityObjectToClipPos(i.vertex);
		#else
			o.vertex = UnityObjectToClipPos(i.vertex);
		#endif

		o.screenPos = ComputeScreenPos(o.vertex);

		return o;
	}

	float4 MainPS(VertexOutput i) : SV_Target {
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(i);
		float2 uv = i.screenPos.xy / i.screenPos.w;
		float4 vTexel = tex2D(UNITY_ACCESS_INSTANCED_PROP(Props, _MainTex), uv).rgba;
		return vTexel;
	}

	ENDCG

    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry"
        }
        LOD 200
        
        Pass {
            Name "Portal"
            
            Stencil {
                Ref 1
                Pass replace
            }

            CGPROGRAM

#pragma vertex MainVS
#pragma fragment MainPS
            
			ENDCG
        }
    }
}
