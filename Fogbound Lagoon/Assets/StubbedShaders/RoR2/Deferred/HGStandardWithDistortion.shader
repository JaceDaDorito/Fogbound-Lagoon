Shader "StubbedRoR2/Base/Shaders/HGStandardWithDistortion" {
	Properties {
		_BumpMap ("Bump Texture", 2D) = "bump" {}
		_MaskTex ("Mask Texture", 2D) = "white" {}
		_Magnitude ("Magnitude", Range(0, 10)) = 0.05
		[HDR] _DistortionColor ("Distortion Color", Vector) = (1,1,1,1)
		[HDR] _OpacityColor ("Opacity Color", Vector) = (1,1,1,1)
		_Opacity ("Opacity", Range(0, 1)) = 0
		_NearFadeZeroDistance ("Near-Fade Zero Distance", Float) = 0
		_NearFadeOneDistance ("Near-Fade One Distance", Float) = 5
		_FarFadeOneDistance ("Far-Fade One Distance", Float) = 200000
		_FarFadeZeroDistance ("Far-Fade Zero Distance", Float) = 250000
		[Toggle(DISTANCEMODULATION)] _DistanceModulationOn ("Apply distance modulation", Float) = 0
		_DistanceModulationMagnitude ("Distance Modulation Magnitude", Range(0, 1)) = 0.5
		_InvFade ("Soft Factor", Range(0, 2)) = 0.1
		[MaterialEnum(Off,0,Front,1,Back,2)] _Cull ("Cull", Float) = 2
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}