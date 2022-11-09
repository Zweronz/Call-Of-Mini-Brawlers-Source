Shader "TUI/SpriteVC_2T" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
 _Color ("Main Color", Color) = (1,1,1,1)
 _BackTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _ColorB ("Back Color", Color) = (1,1,1,1)
}
	//DummyShaderTextExporter
	
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard fullforwardshadows
#pragma target 3.0
		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
		}
		ENDCG
	}
}