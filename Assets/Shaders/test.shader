Shader "Custom/test"
{
    Properties
    {
        _MainTex   ("Main Texture", 2D) = "white" {}
        _Cutoff    ("Wipe Progress", Range(0,1)) = 0.0
        _PixelSize ("Pixel Size", Float) = 2.0
        _GlowColor ("Glow Color", Color) = (1, 0.8, 0.2, 1)
        _GlowPower ("Glow Power", Float) = 1.0
        _GlowThreshold ("Glow Threshold", Range(0,1)) = 0.8
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Cutoff;
            float _PixelSize;
            fixed4 _GlowColor;
            float _GlowPower;
            float _GlowThreshold;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };

            float hash21(float2 p)
            {
                p = frac(p * float2(123.34, 456.21));
                p += dot(p, p + 45.32);
                return frac(p.x * p.y);
            }

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.screenPos = o.vertex;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenBlocks = _ScreenParams.xy / _PixelSize;
                float2 blockCoord = floor(i.uv * screenBlocks);
                float noise = hash21(blockCoord);

                if (noise < _Cutoff)
                    discard;

                fixed4 col = tex2D(_MainTex, i.uv);

                if (col.a > 0.01)
                {
                    float brightness = dot(col.rgb, float3(0.299, 0.587, 0.114));
                    float glowStrength = saturate((brightness - _GlowThreshold) / (1.0 - _GlowThreshold));

                    float2 pos = i.uv * 20.0;
                    float glowWave = sin(_Time.y * 1.5 + pos.x * 0.5 + pos.y * 0.3);
                    float dynamicGlow = 0.5 + 0.5 * glowWave;

                    col.rgb += _GlowColor.rgb * glowStrength * dynamicGlow * _GlowPower;
                }

                return col;
            }
            ENDCG
        }
    }
    Fallback Off
}
