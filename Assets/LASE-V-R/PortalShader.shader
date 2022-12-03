Shader "Custom/PortalShader"
{
    Properties
    {
        _InactiveColor ("Inactive Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screen_pos: TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _InactiveColor;

            v2f vert (const appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screen_pos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                const float2 uv = i.screen_pos.xy / i.screen_pos.w;
                const fixed4 portal_col = tex2D(_MainTex, uv);
                return portal_col;
            }
            ENDCG
        }
    }
    Fallback "Standard"
}
