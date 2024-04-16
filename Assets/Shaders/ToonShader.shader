// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// code from Alan Zucconi
// https://www.alanzucconi.com/2015/06/24/physically-based-rendering/

Shader "Example/Toon Shading" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _RampTex("Ramp", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 0)
        _OutlineWidth ("Outline Width", Range(0.0, 5.0)) = 2.5
    }
        SubShader{
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Toon
            
            struct Input {
                float2 uv_MainTex;
            };
            
            sampler2D _MainTex;
            float4 _OutlineColor;
            float4 _OutlineWidth;
            
            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            }
            sampler2D _RampTex;
            fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
            {
                half NdotL = dot(s.Normal, lightDir);
                fixed4 c;
                
                if (NdotL == 0) {
                    c.rgb = s.Albedo * _OutlineColor;
                }
                else {
                    NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));

                    fixed4 c;
                    c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten * 1;
                }
                c.a = s.Alpha;
                return c;
            }
            ENDCG
    }
        Fallback "Diffuse"
}