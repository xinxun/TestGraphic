Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _brightness("brightness",float) = 1
        _saturation("saturation",float) = 1
        _contrast("contrast",float) =1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            ZTest Always 
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _brightness;
            half _saturation;
            half _contrast;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                //亮度
                fixed4 renderTex = tex2D(_MainTex, i.uv);
                fixed3 finalColor = renderTex.rgb * _brightness;

                //饱和度
                fixed Luminance = 0.215 * renderTex.r + 0.7154 * renderTex.g + 0.0721 *renderTex.b;
                fixed3 LuminanceColor = fixed3 (Luminance,Luminance,Luminance);
                finalColor = lerp (LuminanceColor,finalColor,_saturation);

                //对比度
                fixed3 avgColor = fixed3 (0.5,0.5,0.5);
                finalColor = lerp (avgColor,finalColor,_contrast);


                return fixed4 (finalColor,renderTex.a);
            }
            ENDCG
        }
    }
}