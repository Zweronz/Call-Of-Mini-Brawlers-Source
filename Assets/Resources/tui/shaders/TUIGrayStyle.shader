Shader "Triniti/TUI/TUIGrayStyle" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" {}
 _Color ("Main Color", Color) = (1,1,1,1)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" }
  Blend SrcAlpha OneMinusSrcAlpha
  CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag

  struct appdata_t
  {
    float2 texcoord0 : TEXCOORD0;
    float4 vertex : POSITION;
  };

  struct v2f
  {
    float2 texcoord0 : TEXCOORD0;
    float4 vertex : POSITION;
  };

  sampler2D _MainTex;
  float4 _Color, _MainTex_ST;

  v2f vert(appdata_t v)
  {
    v2f o;
    o.vertex = UnityObjectToClipPos(v.vertex);
    o.texcoord0 = v.texcoord0.xy;
    return o;
  }

  float4 frag(v2f i) : SV_TARGET
  {
    float4 c = tex2D (_MainTex, i.texcoord0) * _Color;
    return float4((((c.x * 0.100000) + (c.y * 0.600000)) + (c.z * 0.300000)).xxx, c.a);
  }
  ENDCG
 }
}
}