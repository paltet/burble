Shader "Shader/BaseShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4    _MainTex_ST;

            float4 frag(v2f_img i) : COLOR
            {
                float4 tex = tex2D(_MainTex, i.uv);

                // Constants represent human eye sensitivity to each colour.
                float lum = tex.r * 0.3 + tex.g * 0.59 + tex.b * 0.11;
                float4 result = float4(lum, lum, lum, tex.a);

                return result;
            }
            ENDCG
        }
    }
}
