Shader "UI/particle1" // Название шейдера в Unity (выпадающее меню Shader)
{
    Properties
    {
        _StarTex   ("Star Texture (RGBA, cut-out)", 2D) = "white" {} // Текстура звезды
        _Tint      ("Base Tint", Color) = (1,1,1,1) // Базовый цвет (модификатор цвета вершин)

        _GlowColor ("Glow Color", Color) = (1,1,1,1) // Цвет свечения вокруг звезды
        _GlowPow   ("Glow Power", Float) = 1.8 // Степень яркости свечения (экспонента альфа-канала)

        _StarCount ("Stars (1-32)",  Float) = 20 // Количество звёзд (частиц)
        _LifeMin   ("Life Min  (sec)", Float) = 1.0 // Минимальное время жизни звезды
        _LifeMax   ("Life Max  (sec)", Float) = 2.5 // Максимальное время жизни звезды
        _SpinMax   ("Max Spin (rad/s)", Float) = 6.0 // Максимальная скорость вращения звезды
        _ScaleMin  ("Min Peak Scale", Float) = 0.5 // Минимальный масштаб при пике анимации
        _ScaleMax  ("Max Peak Scale", Float) = 1.4 // Максимальный масштаб при пике анимации

        _SpawnRect ("Spawn Rect XYWH (0-1)", Vector) = (0,0,1,1) // Область спавна звёзд в нормализованных UV-координатах
        _EdgePad   ("Edge Padding (UV)",  Float) = 0.08 // Отступ от краёв области спавна

        _StencilComp ("Stencil Comparison", Float) = 8 // Сравнение для Stencil
        _Stencil     ("Stencil ID", Float) = 0 // Значение Stencil
        _StencilOp   ("Stencil Operation", Float) = 0 // Операция Stencil
        _StencilWriteMask ("Stencil Write Mask", Float) = 255 // Маска записи Stencil
        _StencilReadMask  ("Stencil Read Mask",  Float) = 255 // Маска чтения Stencil
        _ColorMask   ("Color Mask", Float) = 15 // Какие каналы цвета отрисовывать
        _ClipRect    ("Clip Rect", Vector) = (-32767,-32767,32767,32767) // Область клиппинга UI
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "CanUseSpriteAtlas"="True" } // UI рендер, прозрачный

        Stencil { Ref [_Stencil] Comp [_StencilComp] Pass [_StencilOp] ReadMask [_StencilReadMask] WriteMask [_StencilWriteMask] } // Настройки stencil буфера
        Cull Off  Lighting Off  ZWrite Off // UI: не отбрасываем полигоны, без освещения, не пишем в Z-буфер
        ZTest [unity_GUIZTestMode] // Z-тест зависит от настроек Canvas
        Blend One One // Аддитивное смешивание — звёзды светятся
        ColorMask [_ColorMask] // Управляем маской цвета (RGBA)

        Pass
        {
            CGPROGRAM
            #pragma vertex vert // Вершинный шейдер
            #pragma fragment frag // Фрагментный шейдер
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata
            {
                float4 v:POSITION;
                float4 c:COLOR;
                float2 uv:TEXCOORD0;
            }; // Входные данные: позиция, цвет, UV
            
            struct v2f
            {
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
                float4 w:TEXCOORD1;
                fixed4 col:COLOR;
            }; // Выходные данные: позиция, UV, цвет

            sampler2D _StarTex;
            fixed4 _Tint, _GlowColor;
            float  _GlowPow;
            int    _StarCount;
            float  _LifeMin, _LifeMax, _SpinMax;
            float  _ScaleMin, _ScaleMax;
            float4 _SpawnRect;
            float  _EdgePad;
            float4 _ClipRect;

            inline float Hash(float n){ return frac(sin(n)*43758.5453);} // Простая хеш-функция на float
            inline float2 Hash2(float n){return frac(sin(float2(n,n+1))*float2(157.3,113.5));} // Векторная хеш-функция

            v2f vert(appdata IN){
                v2f o;
                o.pos=UnityObjectToClipPos(IN.v); 
                o.uv=IN.uv;
                o.w=IN.v;
                o.col=IN.c*_Tint; return o;
            }

            float4 DrawStar(float2 uv,int id,float timeNow){
                float life=lerp(_LifeMin,_LifeMax,Hash(id*4.17)); // Время жизни
                float phaseOffset=Hash(id*9.31)*life; // Смещение начала жизни
                float tRel=timeNow+phaseOffset;
                float cycle=floor(tRel/life);
                float age=frac(tRel/life); // Возраст от 0 до 1

                float seed=id*97.1+cycle*23.57; // Новый seed на цикл
                float2 randPos=Hash2(seed); // Позиция
                float randScale=lerp(_ScaleMin,_ScaleMax,Hash(seed+4.2)); // Масштаб
                float randSpin=_SpinMax*(Hash(seed+7.7)*2.0-1.0); // Вращение

                float grow=(age<0.5)?(age*2.0):(1.0-age)*2.0; // Рост-затухание
                if(grow<0.001) return 0;

                float2 spawnMin=_SpawnRect.xy + _EdgePad;
                float2 spawnMax=_SpawnRect.xy + _SpawnRect.zw - _EdgePad;
                float2 center=lerp(spawnMin,spawnMax,randPos); // Центр звезды

                float2 local=(uv-center)/(randScale*grow*0.3); // Локальные координаты
                float rot=randSpin*age*life;
                float2x2 R=float2x2(cos(rot),-sin(rot),sin(rot),cos(rot)); // Матрица вращения
                local=mul(R,local);

                float2 starUV=local+0.5; // UV в пределах 0-1
                if(any(starUV<0)||any(starUV>1)) return 0;

                fixed4 tex=tex2D(_StarTex,starUV);
                tex.rgb*=tex.a;
                float glow=pow(tex.a,_GlowPow); // Светимость по альфе

                return fixed4(tex.rgb*grow + _GlowColor.rgb*glow*grow, tex.a*grow); // Цвет звезды
            }

            fixed4 frag(v2f i):SV_Target{
                if(UnityGet2DClipping(i.w.xy,_ClipRect)<0.5) discard; // Обрезка по ClipRect
                float t=_Time.y;
                fixed4 col=0;
                [loop]for(int k=0;k<_StarCount;k++) col+=DrawStar(i.uv,k,t); // Отрисовка всех звёзд
                return col*i.col; // Модификация цветом вершины
            }
            ENDCG
        }
    }
}
