Shader "Unlit/AlphaUnlit"
{
    Properties
    {
        _MainTex ("Top texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Transparency ("Transparency", Range(0.0, 1)) = 1
        _CutoutThresh("Cutout Threshold", Range(0.0, 1.0)) = 0.2
    }
    SubShader
    {
        Tags { "Queue"="Transparent"  "RenderType"="Transparent" }
        LOD 100
        
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 mainTexCoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float4 _Color;
            float _Transparency;
            float _CutoutThresh;
          

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.mainTexCoord = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.mainTexCoord) * _Color;
                col.a = col.a * _Transparency;
                
                if (col.a < _CutoutThresh)
                    col.a = 0; 
                                          
                return col;
            }
            ENDCG
        }
    }
}
