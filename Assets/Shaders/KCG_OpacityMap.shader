Shader "Hidden/KCG_OpacityMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _KCG_Material("Texture", 2D) = "white"{}
        _KCG_Invert("_KCG_Invert", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            Color(0, 0, 0, 0)
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
            Texture2D _KCG_Material;
            float4 _KCG_Invert;
            SamplerState PointClampSampler;

            fixed4 frag(v2f i) : SV_Target
            {
                //float4 color = tex2D(_MainTex, i.uv);
                float4 color = _MainTex.Sample(PointClampSampler, i.uv);
                float ll = length(color);
                float4 material = _KCG_Material.Sample(PointClampSampler, i.uv);
                if (material.z > 0)
                {
                    return float4(i.uv.x, i.uv.y, _KCG_Invert.r > 0 ? 0 : 100000, 1);
                }
                else
                {
                    //return float4(i.uv.x, i.uv.y, 0, 1);
                    if (_KCG_Invert.r > 0)
                        return float4(i.uv.x, i.uv.y, (color.a <= 0 ? 0 : 100000), 1);
                    else
                        return float4(i.uv.x, i.uv.y, (color.a > 0 ? 0 : 100000), 1);
                }
            }
            ENDCG
        }
    }
}
