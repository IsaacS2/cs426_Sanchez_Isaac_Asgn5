// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// code from Alan Zucconi
// https://www.alanzucconi.com/2015/06/24/physically-based-rendering/

Shader "Example/Toon Shading" {
    Properties{
        _MainTex("Texture", 2D) = "white" {}
        _RampTex("Ramp", 2D) = "white" {}
    }

    CGINCLUDE
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct appdata members normal)
    #pragma exclude_renderers d3d11
    #include "UnityCG.cginc"

    struct appdata {
        float4 vertex : POSITION;
        float3 normal;
    };

    struct v2f {
        float4 pos : POSITION;
        float4 color : COLOR;
    };

    uniform float _Outline;
    uniform float4 _OutlineColor;

    v2f vert(appdata v) {
        v2f o;
        o.pos = UnityObjectToClipPos(v.vertex);

        float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);
        float2 offset = TransformViewToProjection(norm.xy);

        o.pos.xy += offset * o.pos.z * _Outline;
        o.color = _OutlineColor;
        return o;
    }
    ENDCG

        SubShader{
            Tags { "RenderType" = "Opaque" }
            CGPROGRAM
            #pragma surface surf Toon
            #pragma surface surf Lambert vertex:vert

            struct Input {
                float2 uv_MainTex;
            };
            
            sampler2D _MainTex;
            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            }
            sampler2D _RampTex;
            fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
            {
                half NdotL = dot(s.Normal, lightDir);
                NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));
                
                fixed4 c;
                c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten;
                c.a = s.Alpha;
                return c;
            }
            ENDCG
    }
        Fallback "Diffuse"
}