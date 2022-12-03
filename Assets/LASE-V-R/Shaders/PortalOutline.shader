Shader "Custom/PortalOutline"
{
    Properties
    {
        _PortalOutlineColour ("Portal Outline Colour", Color) = (1,1,1,1)
        _MaskID("Mask ID", int) = 1
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
            "Queue"="Geometry+2"
        }
        LOD 200
        
        HLSLINCLUDE
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
		ENDHLSL
        
        Stencil {
            Ref 0
            Comp equal
        }
        
        Pass {
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
            };

            uniform float4 _PortalOutlineColour;

            v2f vert (vertexData v)
            {
                v2f o;
                o.vertex = TransformObjectToHClip(v.vertex.xyz);
                return o;
            }

            float4 frag(v2f i): SV_Target {
                return _PortalOutlineColour;
            }
            
            ENDHLSL
        }
    }
}
