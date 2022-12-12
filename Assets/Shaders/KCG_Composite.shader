// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/KCG_Composite"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _KCG_Material("Texture", 2D) = "black" {}
        _KCG_DistanceField0("Texture", 2D) = "white" {}
        _KCG_DistanceField1("Texture", 2D) = "white" {}
        

        _KCG_BottomLeft("BottomLeft", Vector) = (0, 0, 0, 0)
        _KCG_BottomRight("BottomRight", Vector) = (0, 0, 0, 0)
        _KCG_TopLeft("TopLeft", Vector) = (0, 0, 0, 0)
        _KCG_TopRight("TopRight", Vector) = (0, 0, 0, 0)

        _KCG_Viewport("Viewport", Vector) = (0, 0, 0, 0)

        _KCG_SDF_Scale("SDF_Scale", Vector) = (0, 0, 0, 0)

        _KCG_LightCount("Light Count", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition0("Light Position 0", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition1("Light Position 1", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition2 ("Light Position 2", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition3 ("Light Position 3", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition4 ("Light Position 4", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition5 ("Light Position 5", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition6 ("Light Position 6", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition7 ("Light Position 7", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition8 ("Light Position 8", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition9 ("Light Position 9", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition10 ("Light Position 10", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition11 ("Light Position 11", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition12 ("Light Position 12", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition13 ("Light Position 13", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition14 ("Light Position 14", Vector) = (0, 0, 0, 0)
        _KCG_LightPosition15 ("Light Position 15", Vector) = (0, 0, 0, 0)


        _KCG_LightColor0("Light Color 0", Vector) = (0, 0, 0, 0)
        _KCG_LightColor1("Light Color 1", Vector) = (0, 0, 0, 0)
        _KCG_LightColor2 ("Light Color 2", Vector) = (0, 0, 0, 0)
        _KCG_LightColor3 ("Light Color 3", Vector) = (0, 0, 0, 0)
        _KCG_LightColor4 ("Light Color 4", Vector) = (0, 0, 0, 0)
        _KCG_LightColor5 ("Light Color 5", Vector) = (0, 0, 0, 0)
        _KCG_LightColor6 ("Light Color 6", Vector) = (0, 0, 0, 0)
        _KCG_LightColor7 ("Light Color 7", Vector) = (0, 0, 0, 0)
        _KCG_LightColor8 ("Light Color 8", Vector) = (0, 0, 0, 0)
        _KCG_LightColor9 ("Light Color 9", Vector) = (0, 0, 0, 0)
        _KCG_LightColor10 ("Light Color 10", Vector) = (0, 0, 0, 0)
        _KCG_LightColor11 ("Light Color 11", Vector) = (0, 0, 0, 0)
        _KCG_LightColor12 ("Light Color 12", Vector) = (0, 0, 0, 0)
        _KCG_LightColor13 ("Light Color 13", Vector) = (0, 0, 0, 0)
        _KCG_LightColor14 ("Light Color 14", Vector) = (0, 0, 0, 0)
        _KCG_LightColor15 ("Light Color 15", Vector) = (0, 0, 0, 0)
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
                    float4 screen: TEXCOORD1;
                };





                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                Texture2D _MainTex;
                Texture2D _KCG_Material;
                Texture2D _KCG_DistanceField0;
                Texture2D _KCG_DistanceField1;
                SamplerState PointClampSampler;


                float4 _KCG_BottomLeft;
                float4 _KCG_BottomRight;
                float4 _KCG_TopLeft;
                float4 _KCG_TopRight;

                float4 _KCG_Viewport;

                float4 _KCG_SDF_Scale;

                float4x4 _KCG_ViewProjection;
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

                float sampleSDF(float2 uv)
                {
                    float4 sp0 = _KCG_DistanceField0.Sample(PointClampSampler, uv);
                    float4 sp1 = _KCG_DistanceField1.Sample(PointClampSampler, uv);
                    float d = sp0.b - (sp0.b > 0 ? 0 : sp1.b);
                    return d;
                }

                float2 snap(float2 uv)
                {
                    return float2(floor(uv.x * _KCG_Viewport.x) / _KCG_Viewport.x, floor(uv.y * _KCG_Viewport.y) / _KCG_Viewport.y);
                }

                float addSoftShadows(float2 uv, float2 direction, float4 material, float screenDistance, float worldDistance, float4 lightColor)
                {
                    float light = 9999;
                    float step = 0;
                    float stepV = 0;
                    float scale = 20;
                    for (int i = 0; i < 32; i++)
                    {
                        float2 pos = uv + direction * step / _KCG_Viewport.xy;

                        const float dd = sampleSDF(pos);
                        const float sqd = sqrt(dd);
                        if (dd <= 0)
                        {
                            //if (material.x > 0)
                                light = 0;
                            //else
                            //    light = min(light, scale * sqd / step);
                            break;
                        }
                        if (step >= screenDistance)
                        {
                            light = min(light, scale * sqd / step);
                            break;
                        }
                        else
                        {
                            light = min(light, sqd);
                        }
                        step += sqd;
                    }

                    return light;
                }


                float addHardShadows(float2 uv, float2 direction, float4 material, float screenDistance, float worldDistance, float4 lightColor)
                {
                    float light = 0;
                    float step = 0;
                    float stepV = 0;
                    if (material.x > 0)
                        return 0.1;
                    for (int i = 0; i < 32; i++)
                    {
                        float2 pos = uv + direction * step / _KCG_Viewport.xy;

                        const float dd = sampleSDF(pos);
                        const float sqd = sqrt(dd);
                        if (dd <= 0)
                        {
                            break;
                        }
                        if (step >= screenDistance)
                        {
                            light = pow(saturate(1 - worldDistance * 0.01), 4);
                            //break;
                        }
                        step += sqd;
                    }

                    return light;
                }


                float addNoShadows(float2 uv, float2 direction, float4 material, float screenDistance, float worldDistance, float4 lightColor)
                {
                    float light = 0;
                    float step = 0;
                    float stepV = 0;
                    if (material.x > 0)
                        return 0.1;
                    for (int i = 0; i < 32; i++)
                    {
                        float2 pos = uv + direction * step / _KCG_Viewport.xy;

                        const float dd = sampleSDF(pos);
                        const float sqd = sqrt(dd);
                        if (dd <= 0)
                        {
                            break;
                        }
                        if (step >= screenDistance)
                        {
                            light = pow(saturate(1 - worldDistance * 0.01), 4);
                            //break;
                        }
                        step += sqd;
                    }

                    return 0.6;//light;
                }

                float4 addLight(float2 uv, float4 lightPosition, float4 lightColor, float4 material)
                {
                    float2 position = uv * _KCG_Viewport.xy;
                    //position.x = (int)position.x;
                    //position.y = (int)position.y;

                    float2 lightUV = ComputeScreenPos(mul(_KCG_ViewProjection, lightPosition)) * _KCG_Viewport.xy;
                    //lightUV.x = (int)lightUV.x;
                    //lightUV.y = (int)lightUV.y;

                    float2 distance = lightUV - position;
                    float screenDistance = length(distance);

                    float2 worldToScreen = _KCG_Viewport.xy / _KCG_BottomRight.xy;
                    float worldDistance = length(distance / worldToScreen);
                    
                    float2 direction = distance / screenDistance;
                    //return float4(direction.xy, 0, 1);

                    //float light = addNoShadows(uv, direction, material, screenDistance, worldDistance, lightColor);
                    float light = addHardShadows(uv, direction, material, screenDistance, worldDistance, lightColor);
                    //float light = addSoftShadows(uv, direction, material, screenDistance, worldDistance, lightColor);

                    return float4(3 * (pow(1 - saturate(worldDistance * 0.01), 16))* lightColor.rgb, light);
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    const float scale = _KCG_SDF_Scale.z;

                    float width = _KCG_Viewport.x;
                    float height = _KCG_Viewport.y;
                    
                    float vwidth = _KCG_BottomRight.x;
                    float vheight = _KCG_BottomRight.y;

                    float2 uv = i.uv;

                    float d = sampleSDF(uv);
                    float4 baseColor = _MainTex.Sample(PointClampSampler, uv);
                    baseColor.rgb *= baseColor.a;

                    float4 material = _KCG_Material.Sample(PointClampSampler, uv);

                    const float borderWidth = 10;// scale;
                    const float gray = 0.4;
                    const float4 backgroundColor = float4(gray, gray, gray, 1);
                    const float4 fillColor = float4(0.4, 0.4, 0.4, 1);
                    const float t = saturate(1 - sqrt(abs(d)) / borderWidth);


                    float4 outputColor = 0;

                    float4 lightColor0 = float4(0.5, 0.3, 0.3, 1);
                    float4 lightColor1 = float4(0.3, 0.5, 0.3, 1);
                    float4 lightColor2 = float4(0.3, 0.3, 0.5, 1);
                    float4 lightColor3 = float4(0.5, 0.3, 0.5, 1);

                    float4 light = float4(0, 0, 0, 0);
                    float4 light0 = addLight(uv, _KCG_LightPosition0, lightColor0, material);
                    float4 light1 = addLight(uv, _KCG_LightPosition1, lightColor1, material);
                    float4 light2 = addLight(uv, _KCG_LightPosition2, lightColor2, material);
                    float4 light3 = addLight(uv, _KCG_LightPosition3, lightColor3, material);
                    
                    
                    float3 lightI = light0.rgb * light0.a + light1.rgb * light1.a + light2.rgb * light2.a + light3.rgb * light3.a;
                    float3 lightColor = light0.rgb + light1.rgb + light2.rgb + light3.rgb;// normalize(light.rgb);
                    light = light0.a + light1.a + light2.a + light3.a;
                    float4 borderColor = float4(normalize(lightColor) * light.a, 1);

                    const float borderPow = 18;
                    const float minLight = 0.8;
                    const float borderScale = 10;

                    if (uv.x > 0.5)
                        return float4(d.xxx / 1000, 1);
                    if (d > borderWidth) // background
                    {
                        if (material.z > 0)
                        {
                            //outputColor = float4(baseColor.xxx  * lightColor, 1);// lerp(baseColor, borderColor, t);
                            outputColor.rgb += pow(baseColor.rrr * light.a, 1) * lightColor * 6;
                            return outputColor;
                        }
                        else
                        {
                            outputColor = float4(backgroundColor.rgb * lightI, backgroundColor.a);
                            return outputColor;
                        }
                    }
                    else
                    {
                        if (material.x > 0)
                        {
                            if (d > 0)
                            {
                                outputColor = lerp(baseColor * light.a, borderColor, t);
                                outputColor.rgb += pow(t, borderPow) * lightI * borderScale;
                                return outputColor;
                            }
                            else
                            {
                                outputColor = float4(baseColor.rgb * lightI * 20, baseColor.a);
                                return outputColor;
                            }
                        }
                        else
                        {
                            if (abs(d) <= borderWidth)
                            {
                                outputColor = lerp(baseColor * pow(light.a, 1), pow(borderColor, 9), t);
                                outputColor.rgb += pow(t, borderPow) * borderColor * borderScale * pow(light.a, 4);
                                return outputColor;
                            }
                            else
                            {
                                const float colorScale = scale * 0.0001;
                                outputColor = float4(baseColor.rgb * fillColor * saturate(1 - pow(abs(d) * colorScale, 0.5)) * borderColor * 2, baseColor.a);
                                return outputColor;
                            }
                        }
                    }

                    return outputColor;
                }
                ENDCG
            }
        }
}
