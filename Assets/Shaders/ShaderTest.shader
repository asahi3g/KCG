Shader "Unlit/ShaderTest"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _KCG_Material ("Material", Vector) = (0, 0, 0, 0)
        _KCG_LightCount ("Light Count", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition0 ("Light Position 0", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition1 ("Light Position 1", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition2 ("Light Position 2", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition3 ("Light Position 3", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition4 ("Light Position 4", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition5 ("Light Position 5", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition6 ("Light Position 6", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition7 ("Light Position 7", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition8 ("Light Position 8", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition9 ("Light Position 9", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition10 ("Light Position 10", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition11 ("Light Position 11", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition12 ("Light Position 12", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition13 ("Light Position 13", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition14 ("Light Position 14", Vector) = (0, 0, 0, 0)
        //_KCG_LightPosition15 ("Light Position 15", Vector) = (0, 0, 0, 0)


        _KCG_LightColor0 ("Light Color 0", Vector) = (0, 0, 0, 0)
        _KCG_LightColor1 ("Light Color 1", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor2 ("Light Color 2", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor3 ("Light Color 3", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor4 ("Light Color 4", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor5 ("Light Color 5", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor6 ("Light Color 6", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor7 ("Light Color 7", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor8 ("Light Color 8", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor9 ("Light Color 9", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor10 ("Light Color 10", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor11 ("Light Color 11", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor12 ("Light Color 12", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor13 ("Light Color 13", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor14 ("Light Color 14", Vector) = (0, 0, 0, 0)
        //_KCG_LightColor15 ("Light Color 15", Vector) = (0, 0, 0, 0)

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
                float4 color: COLOR0;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 color: COLOR0;
                float2 uv : TEXCOORD1;
                float2 pos: TEXCOORD2;
                UNITY_FOG_COORDS(3)
           };

            Texture2D _MainTex;
            SamplerState PointClampSampler;
            float4 _MainTex_ST;

            float4 _KCG_LightCount;
            float4 _KCG_Material;

            float4 _KCG_LightPosition0;
            float4 _KCG_LightPosition1;
            float4 _KCG_LightPosition2;
            float4 _KCG_LightPosition3;
            float4 _KCG_LightPosition4;
            float4 _KCG_LightPosition5;
            float4 _KCG_LightPosition6;
            float4 _KCG_LightPosition7;
            float4 _KCG_LightPosition8;
            float4 _KCG_LightPosition9;
            float4 _KCG_LightPosition10;
            float4 _KCG_LightPosition11;
            float4 _KCG_LightPosition12;
            float4 _KCG_LightPosition13;
            float4 _KCG_LightPosition14;
            float4 _KCG_LightPosition15;

            float4 _KCG_LightColor0;
            float4 _KCG_LightColor1;
            float4 _KCG_LightColor2;
            float4 _KCG_LightColor3;
            float4 _KCG_LightColor4;
            float4 _KCG_LightColor5;
            float4 _KCG_LightColor6;
            float4 _KCG_LightColor7;
            float4 _KCG_LightColor8;
            float4 _KCG_LightColor9;
            float4 _KCG_LightColor10;
            float4 _KCG_LightColor11;
            float4 _KCG_LightColor12;
            float4 _KCG_LightColor13;
            float4 _KCG_LightColor14;
            float4 _KCG_LightColor15;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,o.vertex);
                o.pos = v.vertex;
                o.color = v.color;
                return o;
            }

            float4 AddLight(float4 albedo, float2 pixelPosition, float4 lightPosition, float4 lightColor)
            {
                float dis = 1 - clamp(length(pixelPosition - lightPosition.xy) * 0.13, 0, 1);
                return float4(lightColor.rgb * dis * albedo.rgb, 0);
            }

            struct FragmentOutput
            {
                half4 color : SV_Target0;
                half4 material : SV_Target1;
            };

            FragmentOutput frag(v2f i) : SV_Target
            {
                FragmentOutput o;
                //return float4(_KCG_LightColor0.rgb, 1);
                float2 pixelWorld = mul(unity_ObjectToWorld, float4(i.pos.xy, 0, 1)).xy;
                //return float4(pixelWorld, 0, 1);

                float2 lightWorld = _KCG_LightPosition0.xy;
                //return float4(lightWorld, 0, 1);
                float distance = length(pixelWorld - lightWorld.xy) * 0.01;
                //return float4(distance.xxx * _KCG_LightColor0.rgb, 1);

                float4 color = i.color;
                float islight = 0;
                if (color.a >= 0.22 && color.a <= 0.23)
                {
                    islight = 1.0f;
                    //color *= 30;
                }
                else
                {
                    color = float4(1, 1, 1, 1);
                }

                float4 texColor = texColor = _MainTex.Sample(PointClampSampler, i.uv);
                o.color = float4(texColor.rgb, texColor.a);// texColor;
                o.material = float4(_KCG_Material.x, _KCG_Material.y, islight, 0);
                return o;

                float4 albedo = texColor;// *i.color;
                float4 light = float4(albedo.rgb, albedo.a);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition0 , _KCG_LightColor0 * 2);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition1 , _KCG_LightColor1);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition2 , _KCG_LightColor2);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition3 , _KCG_LightColor3);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition4 , _KCG_LightColor4);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition5 , _KCG_LightColor5);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition6 , _KCG_LightColor6);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition7 , _KCG_LightColor7);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition8 , _KCG_LightColor8);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition9 , _KCG_LightColor9);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition10, _KCG_LightColor10);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition11, _KCG_LightColor11);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition12, _KCG_LightColor12);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition13, _KCG_LightColor13);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition14, _KCG_LightColor14);
                //light += AddLight(albedo, pixelWorld, _KCG_LightPosition15, _KCG_LightColor15);






            }
            ENDCG
        }
    }
}
