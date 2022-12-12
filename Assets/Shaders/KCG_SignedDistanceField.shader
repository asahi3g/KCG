Shader "Hidden/KCG_SignedDistanceField"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"
        _KCG_SDF_Params("_KCG_SDF_Params", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

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
                o.uv = v.uv;
                return o;
            }

            Texture2D _MainTex;
            SamplerState PointClampSampler;

            float4 _KCG_SDF_Params;

            fixed4 frag(v2f i) : SV_Target
            {
                float Beta = _KCG_SDF_Params.x;
                float2 Offset = _KCG_SDF_Params.yz;
                float2 vTexCoord = i.uv;

                float3 this_pixel = _MainTex.Sample(PointClampSampler, i.uv).rgb;
                float3 east_pixel = _MainTex.Sample(PointClampSampler, vTexCoord + Offset).rgb;
                float3 west_pixel = _MainTex.Sample(PointClampSampler, vTexCoord - Offset).rgb;

                float A = this_pixel.b;
                float e = Beta + east_pixel.b;
                float w = Beta + west_pixel.b;
                float B = min(min(A, e), w);

                // If there is no change, discard the pixel.
                // Convergence can be detected using GL_ANY_SAMPLES_PASSED.
                if (A == B) {
                    return float4(this_pixel, 1);
                    discard;
                }

                float3 DistanceMap = float3(west_pixel.rg, B);

                if (A <= e && e <= w) DistanceMap.rg = this_pixel.rg;
                if (A <= w && w <= e) DistanceMap.rg = this_pixel.rg;
                if (e <= A && A <= w) DistanceMap.rg = east_pixel.rg;
                if (e <= w && w <= A) DistanceMap.rg = east_pixel.rg;

                return float4(DistanceMap.rgb, 1);
            }

            ENDCG
        }
    }
}
