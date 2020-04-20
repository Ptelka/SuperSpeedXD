Shader "Game/Road"
{
    Properties
    {
        _MainTex ("Color (RGB) Alpha (A)", 2D) = "white" {}
        
        _RoadSizeY ("Road Size Y", float) = 1.0
        _RoadSizeX ("Road Size X", float) = 0.6
        _MiddleStripSize ("Middle Strip Size", float) = 0.01
        _WhiteStripSize ("White Strip Size", float) = 0.04
        _RedStripSize ("Red Strip Size", float) = 0.06
        
        _GrassColorA ("Grass Color A", Color) = (0.1, 0.8, 0, 1)
        _GrassColorB ("Grass Color B", Color) = (0.5, 0.8, 0, 1)
        _FirstStripColor ("First Strip Color", Color) = (0.8, 0.1, 0, 1)
        _SecondStripColor ("Second Strip Color", Color) = (0.8, 0.8, 0.8, 1)
        _MiddleStripColorA ("Middle Strip Color", Color) = (0.8, 0.8, 0.8, 1)
        _MiddleStripColorB ("Middle Strip Color", Color) = (0.2, 0.2, 0.2, 1)
        _RoadColor ("Road Color", Color) = (0.2, 0.2, 0.2, 1)
        
        _Curvature ("Curvature",  Range(-1, 1)) = 0
        _PerspectiveFactor("Perspective Factor", Range(0.5, 1.5)) = 1.02
        _Distance ("Distance", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard alpha

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _RoadSizeY;
        float _RoadSizeX;
        float _MiddleStripSize;
        float _WhiteStripSize;
        float _RedStripSize;
        
        fixed4 _GrassColorA;
        fixed4 _GrassColorB;
        fixed4 _FirstStripColor;
        fixed4 _SecondStripColor;
        fixed4 _MiddleStripColorA;
        fixed4 _MiddleStripColorB;
        fixed4 _RoadColor;
        
        float _Curvature;
        float _PerspectiveFactor;
        float _Distance;
        
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)
        
        float4 sky() {
            return float4(0.01, 0.1, 0.8, 0);
        }
        
        float calc_perspective(float2 position) {
            return position.y / (_RoadSizeY * _PerspectiveFactor);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float2 position = IN.uv_MainTex;
            if (position.y > _RoadSizeY) {
                o.Albedo = sky();
                o.Alpha = 0;
                return;
            }
            
            float perspective = calc_perspective(position);
            float roadWidth =  (1.0 - perspective);
            float width = 1;
            
            float middlePoint = width / 2.f + width * _Curvature * pow(perspective, 3.0);
            
            float middleStripLeft = middlePoint - roadWidth * _MiddleStripSize;
            float roadLeft = middleStripLeft - roadWidth * _RoadSizeX;
            float whiteStripLeft = roadLeft - roadWidth * _WhiteStripSize;
            float redStripLeft = whiteStripLeft - roadWidth * _RedStripSize;
            
            float middleStripRight = middlePoint + roadWidth * _MiddleStripSize;
            float roadRight = middleStripRight + roadWidth * _RoadSizeX;
            float whiteStripRight = roadRight + roadWidth * _WhiteStripSize;
            float redStripRight = whiteStripRight + roadWidth * _RedStripSize;
            
            fixed4 grassColor = sin(20.0 * pow(perspective, 3.0) + _Distance * 0.1) > 0.0 ? _GrassColorA : _GrassColorB;
            fixed4 middleColor = sin(80.0 * pow(perspective, 3.0) + _Distance * 0.1) > 0.0 ? _MiddleStripColorA : _MiddleStripColorB;
            
            if (position.x < redStripLeft && position.x >= 0.0) {
                o.Albedo = grassColor;
            } else if (position.x < whiteStripLeft && position.x >= redStripLeft) {
                o.Albedo = _FirstStripColor;
            } else if (position.x < roadLeft && position.x >= whiteStripLeft) {
                o.Albedo = _SecondStripColor;
            } else if (position.x < middleStripLeft && position.x >= roadLeft) {
                o.Albedo = _RoadColor;
            } else if (position.x < middlePoint && position.x >= middleStripLeft) {
                o.Albedo = middleColor;
            } else if (position.x < 1.0 && position.x >= redStripRight) {
                o.Albedo = grassColor;
            } else if (position.x > whiteStripRight && position.x <= redStripRight) {
                o.Albedo = _FirstStripColor;
            } else if (position.x > roadRight && position.x <= whiteStripRight) {
                o.Albedo = _SecondStripColor;
            } else if (position.x > middleStripRight && position.x <= roadRight) {
                o.Albedo = _RoadColor;
            } else if (position.x > middlePoint && position.x <= middleStripRight) {
                o.Albedo = middleColor;
            }
            
            o.Metallic = 0.0;
            o.Emission = half3(0.11,0.13,0.19);
            o.Alpha = 1;
            o.Albedo = o.Albedo * 1.5;
        }
        
        
        ENDCG
    }
    FallBack "Diffuse"
}

/*
#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable

uniform float time;
uniform vec2 resolution;

void main( void ) {
	if (position.y > resolution.y * roadSizeY) {
		o.Albedo = vec4(0.01, 0.1, 0.8, 1);
		return;
	}
	
	float perspective = position.y / (resolution.y * roadSizeY * 1.01);
	float roadWidth =  resolution.y * (1.0 - perspective);
	float width = resolution.x;
	
	float middlePoint = width / 2.0 + width * curvature * pow(perspective, 3.0);
	
	float middleStripLeft = middlePoint - roadWidth * middleStripSize;
	float roadLeft = middleStripLeft - roadWidth * roadSizeX;
	float whiteStripLeft = roadLeft - roadWidth * whiteStripSize;
	float redStripLeft = whiteStripLeft - roadWidth * redStripSize;
	
	float middleStripRight = middlePoint + roadWidth * middleStripSize;
	float roadRight = middleStripRight + roadWidth * roadSizeX;
	float whiteStripRight = roadRight + roadWidth * whiteStripSize;
	float redStripRight = whiteStripRight + roadWidth * redStripSize;
	
	if (position.x < redStripLeft && position.x >= 0.0) {
		o.Albedo = grassColor;
	} else if (position.x < whiteStripLeft && position.x >= redStripLeft) {
		o.Albedo = redStripColor;
	} else if (position.x < roadLeft && position.x >= whiteStripLeft) {
		o.Albedo = whiteStripColor;
	} else if (position.x < middleStripLeft && position.x >= roadLeft) {
		o.Albedo = roadColor;
	} else if (position.x < middlePoint && position.x >= middleStripLeft) {
		o.Albedo = whiteStripColor;
	} else if (position.x < resolution.x && position.x >= redStripRight) {
		o.Albedo = grassColor;
	} else if (position.x > whiteStripRight && position.x <= redStripRight) {
		o.Albedo = redStripColor;
	} else if (position.x > roadRight && position.x <= whiteStripRight) {
		o.Albedo = whiteStripColor;
	} else if (position.x > middleStripRight && position.x <= roadRight) {
		o.Albedo = roadColor;
	} else if (position.x > middlePoint && position.x <= middleStripRight) {
		o.Albedo = whiteStripColor;
	}
}*/