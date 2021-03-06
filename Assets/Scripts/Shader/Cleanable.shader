﻿Shader "Unlit/Cleanable"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _MaskTex ("Mask Texture (A)", 2D) = "white" {}
        _MaskColor ("Mask Color", Color) = (1,1,1,1)
        _Color ("Tint", Color) = (1,1,1,1)
        _CutoutThresh("Cutout Threshold", Range(0.0, 1.0)) = 0.2
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader
    {
        Tags
        { 
            "Queue"="Transparent" 
            "IgnoreProjector"="True" 
            "RenderType"="Transparent" 
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        
        LOD 100
        
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha
     

        Pass
        {
        CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            
            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord  : TEXCOORD0;
            };
            
            fixed4 _Color;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _Color ;
                #ifdef PIXELSNAP_ON
                OUT.vertex = UnityPixelSnap (OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            sampler2D _MaskTex;
            float4 _MaskTex_ST;
            fixed4 _MaskColor;
            
            float _CutoutThresh;
            
            float _AlphaSplitEnabled;
            float _ShowHeightPercent;

            fixed4 SampleSpriteTexture (float2 uv)
            {
                fixed4 color = tex2D (_MainTex, uv);

#if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                if (_AlphaSplitEnabled)
                    color.a = tex2D (_AlphaTex, uv).r;
#endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

                return color;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 mainCol = SampleSpriteTexture (IN.texcoord) * IN.color;
                float w = tex2D(_MaskTex, IN.texcoord).a;
                
                fixed4 c = lerp(_MaskColor, mainCol, mainCol.a);

                if (w < _CutoutThresh) {
                    c.rgb = 0;
                    c.a = 0;
                } else {
                    if (w < c.a) {
                        c.rgb *= w;
                        c.a = w;
                    } else {
                        c.rgb *= c.a;
                    }
                }
                
                return c;
            }
        ENDCG
        }
    }
}
