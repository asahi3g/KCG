Shader "Unlit/CircleSegmentDraw"
{
    Properties
    {
         _Color("Color", Color) = (1,1,1,1)
         _Angle("Angle", Range(0,360)) = 120.0
    }
        SubShader
    {
        Tags 
        { 
            "RenderType" = "Opaque" 
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha // Traditional transparency

        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
         
            #include "UnityCG.cginc"

            float _Angle;
            float4 _Color;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv - 0.5;;
                return o;
            }

            // Todo antialiasing.

            fixed4 frag (v2f i) : SV_Target
            {
                float theta = degrees(atan2(i.uv.y, i.uv.x));
                float dist = length(i.uv);
                bool isInCircle = dist <= 0.5;
                float angle = abs(theta) - _Angle / 2.0f;
                bool isInSector = angle <= 0.0;

                if (isInSector && isInCircle)
                {
                    return fixed4(_Color.rgb, _Color.a);

                }

                 return fixed4(0.0f, 0.0f, 0.0f, 0.0f);
            }
            ENDCG
        }
    }
}
