Shader "UI/particle2"
{
    Properties
    {
        _StarTex   ("Star Texture (RGBA, cut-out)", 2D) = "white" {}
        _Tint      ("Base Tint", Color)                 = (1,1,1,1)

        _GlowColor ("Glow Color", Color)                = (1,1,1,1)
        _GlowPow   ("Glow Power", Float)                = 1.8

        _StarCount ("Stars (1-32)", Float)              = 24

        _LifeMin   ("Life Min  (sec)", Float)           = 1.0
        _LifeMax   ("Life Max  (sec)", Float)           = 2.5

        _SpinMax   ("Max Spin (rad/s)", Float)          = 6.0
        _ScaleMin  ("Min Peak Scale", Float)            = 0.5
        _ScaleMax  ("Max Peak Scale", Float)            = 1.3

        _SpeedMin  ("Fall Speed Min (UV/sec)", Float)   = 0.15
        _SpeedMax  ("Fall Speed Max (UV/sec)", Float)   = 0.35

        _SpawnRect ("Spawn Rect XYWH (0-1)", Vector)    = (0,0,1,1)
        _EdgePad   ("Edge Padding (UV)", Float)         = 0.08

        _StencilComp   ("Stencil Comparison", Float) = 8
        _Stencil       ("Stencil ID", Float)         = 0
        _StencilOp     ("Stencil Operation", Float)  = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask  ("Stencil Read Mask",  Float) = 255
        _ColorMask     ("Color Mask", Float) = 15
        _ClipRect      ("Clip Rect", Vector) = (-32767,-32767,32767,32767)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" }

        Stencil { Ref [_Stencil] Comp [_StencilComp] Pass [_StencilOp] ReadMask [_StencilReadMask] WriteMask [_StencilWriteMask] }

        Cull Off  Lighting Off  ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend One One
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex   vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"
            
            struct appdata { float4 v:POSITION; float4 c:COLOR; float2 uv:TEXCOORD0; };
            struct v2f     { float4 pos:SV_POSITION; float2 uv:TEXCOORD0; float4 w:TEXCOORD1; fixed4 col:COLOR; };

            fixed4 _Tint; float4 _ClipRect;

            v2f vert(appdata IN)
            {
                v2f o;
                o.pos   = UnityObjectToClipPos(IN.v);
                o.uv    = IN.uv;
                o.w     = IN.v;
            #ifdef UNITY_HALF_TEXEL_OFFSET
                o.pos.xy += (_ScreenParams.zw-1)*float2(-1,1);
            #endif
                o.col = IN.c * _Tint;
                return o;
            }
            
            sampler2D _StarTex;
            fixed4 _GlowColor;  float _GlowPow;

            int   _StarCount;
            float _LifeMin, _LifeMax;
            float _SpinMax;
            float _ScaleMin, _ScaleMax;
            float _SpeedMin, _SpeedMax;

            float4 _SpawnRect;
            float  _EdgePad;
            
            inline float Hash(float n)   { return frac(sin(n)*43758.5453); }
            inline float2 Hash2(float n) { return frac(sin(float2(n,n+1))*float2(157.3,113.5)); }


            float4 DrawStar(float2 fragUV, int id, float tNow)
            {
                float life = lerp(_LifeMin, _LifeMax, Hash(id*3.11));

                float phaseOffset = Hash(id*5.73)*life;
                float tRel  = tNow + phaseOffset;
                float cycle = floor(tRel / life);
                float age   = frac(tRel / life);

                float seed   = id*91.7 + cycle*37.9;
                
                float2 randPos = Hash2(seed);
                float2 spawnMin = _SpawnRect.xy + _EdgePad;
                float2 spawnMax = _SpawnRect.xy + _SpawnRect.zw - _EdgePad;

                float2 startPos;
                startPos.x = lerp(spawnMin.x, spawnMax.x, randPos.x);
                startPos.y = spawnMax.y;

                float fallSpeed = lerp(_SpeedMin, _SpeedMax, Hash(seed+2.2));

                float2 center = startPos - float2(0, fallSpeed * age * life);

                float grow = (age < 0.5) ? (age*2.0) : (1.0-age)*2.0;
                if(grow < 0.001) return 0;

                float spin = _SpinMax * (Hash(seed+4.1)*2.0 - 1.0);
                float rot  = spin * age * life;

                float  peakScale = lerp(_ScaleMin, _ScaleMax, Hash(seed+6.6));
                float2 local = (fragUV - center) / (peakScale * grow * 0.3);

                float2x2 R = float2x2(cos(rot),-sin(rot),sin(rot),cos(rot));
                local = mul(R, local);

                float2 starUV = local + 0.5;
                if(any(starUV < 0) || any(starUV > 1)) return 0;

                fixed4 tex = tex2D(_StarTex, starUV);
                tex.rgb *= tex.a;
                float glow = pow(tex.a, _GlowPow);

                return fixed4(tex.rgb * grow + _GlowColor.rgb * glow * grow,
                              tex.a * grow);
            }
            
            fixed4 frag(v2f i) : SV_Target
            {
                if(UnityGet2DClipping(i.w.xy,_ClipRect) < 0.5) discard;

                float t = _Time.y;
                fixed4 col = 0;
                [loop]
                for(int k=0; k<_StarCount; k++)
                    col += DrawStar(i.uv, k, t);

                return col * i.col;
            }
            ENDCG
        }
    }
}