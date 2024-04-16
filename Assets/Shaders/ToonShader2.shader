// code from Alan Zucconi
// https://www.alanzucconi.com/2015/06/24/physically-based-rendering/

// also code from https://shadowmint.gitbooks.io/unity-material-shaders/content/shaders/surface/multipass_edge.html


Shader "Shaders/Material/EdgeMultiPass" {
    Properties{
      _MainTex("Albedo (RGB)", 2D) = "white" {}
      _RampTex("Ramp", 2D) = "white" {}
      _OutlineSize("Outline Size", float) = 0.01
      _OutlineColor("Outline Color", Color) = (1, 1, 1, 1)
    }
        SubShader{
          Tags { "RenderType" = "Opaque" }

          // Render the normal content as a second pass overlay.
          // This is a normal texture map.
          CGPROGRAM
            #pragma surface surf Toon

            sampler2D _MainTex;

            struct Input {
              float2 uv_MainTex;
            };

            void surf(Input IN, inout SurfaceOutput o) {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
            }

            sampler2D _RampTex;
            fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
            {
                half NdotL = dot(s.Normal, lightDir);
                fixed4 c;

                NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));

                c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten * 1;
                
                c.a = s.Alpha;
                return c;
            }
          ENDCG

              // Cull rendered pixels on this surface
              Cull Front

              // Render scaled background geometry
              CGPROGRAM
                #pragma surface surf Standard vertex:vert

                float4 _OutlineColor;
                float _OutlineSize;

                // Linearly expand using surface normal
                void vert(inout appdata_full v) {
                  v.vertex.xyz += v.normal * _OutlineSize;
                }

                struct Input {
                  float2 uv_MainTex;
                };

                void surf(Input IN, inout SurfaceOutputStandard o) {
                  o.Albedo = _OutlineColor.rgb;
                }
              ENDCG
      }
          FallBack "Diffuse"
}