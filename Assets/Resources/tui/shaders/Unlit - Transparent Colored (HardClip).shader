// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Transparent Colored (HardClip)" {
  Properties {
   _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
   _Rect ("Screen Rect", Vector) = (-1,-1,0,0)
  }
  SubShader { 
   LOD 200
   Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
   Pass {
    Tags { "QUEUE"="Transparent" "IGNOREPROJECTOR"="True" "RenderType"="Transparent" }
    ZWrite Off
    Cull Off
    Fog { Mode Off }
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask RGB
    Offset -1, -1
    CGPROGRAM
    #pragma vertex vert
    #pragma fragment frag
  
    sampler2D _MainTex;
    float4 _MainTex_ST, _Rect;
  
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
      return tex2D(_MainTex, i.texcoord0) * i.color;
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