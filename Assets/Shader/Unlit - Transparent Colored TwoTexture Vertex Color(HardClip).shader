Shader "Unlit/Transparent Colored TwoTexture Vertex Color(HardClip)" {
Properties {
 _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
 _Color ("Main Color", Color) = (1,1,1,1)
 _Rect ("Screen Rect", Vector) = (-1,-1,0,0)
 _BackTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
 _ColorB ("Back Color", Color) = (1,1,1,1)
}
SubShader { 
 LOD 200
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "color", Color
   Bind "texcoord", TexCoord0
  }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  ColorMask RGB
  Offset -1, -1
  CGPROGRAM
  #pragma vertex vert
  #pragma fragment frag

  sampler2D _MainTex, _BackTex;
  float4 _MainTex_ST, _Rect, _Color, _ColorB;

  struct appdata_t
  {
    float4 texcoord0 : TEXCOORD0;
    float4 color : COLOR;
    float4 vertex : POSITION;
  };

  struct v2f
  {
    float2 texcoord0 : TEXCOORD0;
    float4 texcoord1 : TEXCOORD1;
    float4 color : COLOR;
    float4 vertex : POSITION;
  };

  v2f vert(appdata_t v)
  {
    v2f o;
    float4 pos = UnityObjectToClipPos(v.vertex);
    float2 rec = pos.xy / pos.w;
    o.vertex = pos;
    o.color = v.color;
    o.texcoord0 = ((v.texcoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
    o.texcoord1 = float4(rec.x - _Rect.x, _Rect.z - rec.x, rec.y - _Rect.y, _Rect.w - rec.y);
    return o;
  }

  float4 frag(v2f i) : SV_TARGET
  {
    
    if (min(i.texcoord1.x, i.texcoord1.y) < 0)
    {
      discard;
    }
    if (min(i.texcoord1.w, i.texcoord1.z) < 0)
    {
      discard;
    }
    float4 col = ((tex2D (_MainTex, i.texcoord0) * _Color) * i.color);
    return lerp ((tex2D (_BackTex, i.texcoord0) * _ColorB), col, col.wwww);
  }
  ENDCG
}
 }
SubShader { 
 LOD 100
 Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
 Pass {
  Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  AlphaTest Greater 0.01
  ColorMask RGB
  ColorMaterial AmbientAndDiffuse
  SetTexture [_MainTex] { combine texture * primary }
 }
}
}