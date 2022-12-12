// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PortalOutline"
{
    Properties
    {
        _PortalOutlineColour ("Portal Outline Colour", Color) = (1,1,1,1)
        _MaskID("Mask ID", int) = 1
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

		#if UNITY_VERSION >= 560
				UNITY_VERTEX_OUTPUT_STEREO
		#endif
	};

	// Globals --------------------------------------------------------------------------------------------------------------------------------------------------
	UNITY_INSTANCING_BUFFER_START(Props)
		UNITY_DEFINE_INSTANCED_PROP(float4, _PortalOutlineColour)
	UNITY_INSTANCING_BUFFER_END(Props)

	VertexOutput MainVS(VertexInput i)
	{
		VertexOutput o;
		UNITY_SETUP_INSTANCE_ID(i);
		UNITY_INITIALIZE_OUTPUT(VertexOutput, o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

		#if UNITY_VERSION >= 540
			o.vertex = UnityObjectToClipPos(i.vertex);
		#else
			o.vertex = UnityObjectToClipPos(i.vertex);
		#endif

		return o;
	}

	float4 MainPS(VertexOutput i) : SV_Target
	{
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(i);
#if UNITY_SINGLE_PASS_STEREO
	return (1, 0, 0, 1);
#else

		return _PortalOutlineColour;
#endif
	}

	ENDCG

    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry+2"
        }
        LOD 200
        
        Stencil {
            Ref 0
            Comp equal
        }
        
        Pass {
            CGPROGRAM

            #pragma vertex MainVS
            #pragma fragment MainPS
            
            ENDCG
        }
    }
}
