Shader "Custom/2DOutlineShader"
{
    Properties
    {
        _MainTex("Sprite Texture", 2D) = "white" {}
        _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
        _OutlineThickness("Outline Thickness", Float) = 0.03
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _OutlineColor;
            float _OutlineThickness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 uvOffsets[8] = {
                    float2( _OutlineThickness,  0),
                    float2(-_OutlineThickness,  0),
                    float2(0,  _OutlineThickness),
                    float2(0, -_OutlineThickness),
                    float2( _OutlineThickness, _OutlineThickness),
                    float2(-_OutlineThickness, _OutlineThickness),
                    float2( _OutlineThickness, -_OutlineThickness),
                    float2(-_OutlineThickness, -_OutlineThickness)
                };

                float4 texColor = tex2D(_MainTex, i.uv);

                if (texColor.a == 0) // Skip fully transparent pixels
                {
                    for (int j = 0; j < 8; j++)
                    {
                        float4 sampleColor = tex2D(_MainTex, i.uv + uvOffsets[j]);
                        if (sampleColor.a > 0) return _OutlineColor;
                    }
                }

                return texColor;
            }
            ENDCG
        }
    }
}