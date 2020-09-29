Shader "Game/Road"
{
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard alpha
        #pragma target 3.0
        #include "Assets/Scripts/RoadCommon.h"

        struct Input
        {
            float2 uv_MainTex;
        };

        sampler2D uv_MainTex;
        
        float2 road_size;

        float markings_size;
        float4 markings_color;

        float4 layers[10];
        float layers_sizes[10];
        int layers_count;

        float curvature;
        float distance;

        float4 background[2];

        bool in_range(float val, float a, float b)
        {
            return val > a && val < b;
        }

        bool in_layer(float position, float sum, float size, float offset)
        {
            return in_range(position, sum + offset, sum + offset + size) ||
                   in_range(position, (-sum + offset) - size, -sum + offset);
        }

        void draw_markings(inout SurfaceOutputStandard o, float position, float offset, float perspective)
        {
            if (in_layer(position, 0, markings_size * (1.0 - perspective), offset) && MOVE_ILLUSION_FUNCTION(80.0, perspective, distance) < 0.f)
            {
                o.Albedo = markings_color;
            }
        }

        float4 draw_background(float perspective)
        {
            return MOVE_ILLUSION_FUNCTION(20.0, perspective, distance) < 0.f ? background[0] : background[1];
        }

        float4 draw_layers(float position, float offset, float width, float perspective)
        {
            float sum = 0.f;
            for (int i = 0; i < layers_count; ++i)
            {
                float size = layers_sizes[i] * width;
                if (in_layer(position, sum, size, offset))
                {
                    return layers[i];
                }
                
                sum += size;
            }

            return draw_background(perspective);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 position = IN.uv_MainTex;
            if (position.y > road_size.y) {
                o.Albedo = float4(0.01, 0.1, 0.8, 0);
                o.Alpha = 0;
                return;
            }
            
            float perspective = PERSPECTIVE(position.y, road_size.y);
            float curve = CURVE(curvature, perspective);

            o.Albedo = draw_layers(position.x - 0.5f, curve, 1.0 - perspective, perspective);
            draw_markings(o, position.x - 0.5f, curve, perspective);
            
            o.Metallic = 0.0;
            o.Emission = half3(0.11,0.13,0.19);
            o.Alpha = 1;
            o.Albedo = o.Albedo * 1.5;
        }
        
        
        ENDCG
    }
    FallBack "Diffuse"
}