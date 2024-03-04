Shader "TUI/SpriteVC_2T" {
  Properties {
   _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
   _Color ("Main Color", Color) = (1,1,1,1)
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
  
    struct appdata_t
    {
      float4 texcoord0 : TEXCOORD0;
      float4 color : COLOR;
      float4 vertex :  POSITION;
    };
  
    struct v2f
    {
      float2 texcoord0 : TEXCOORD0;
      float4 color : COLOR;
      float4 vertex :  POSITION;
    };
  
    sampler2D _MainTex, _BackTex;
    float4 _Color, _ColorB, _MainTex_ST;
  
    v2f vert(appdata_t v)
    {
      v2f o;
      o.vertex = UnityObjectToClipPos(v.vertex);
      o.color = v.color;
      o.texcoord0 = ((v.texcoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
      return o;
    }
  
    float4 frag(v2f i) : SV_TARGET
    {
      float4 col = ((tex2D (_MainTex, i.texcoord0) * _Color) * i.color);
      return lerp ((tex2D (_BackTex, i.texcoord0) * _ColorB), col, col.wwww);
    }
    ENDCG
   }
  }
  SubShader { 
   Tags { "QUEUE"="Transparent" }
   Pass {
    Tags { "QUEUE"="Transparent" }
    BindChannels {
     Bind "vertex", Vertex
     Bind "color", Color
     Bind "texcoord", TexCoord
    }
    Color [_Color]
    Cull Off
    Fog { Mode Off }
    Blend SrcAlpha OneMinusSrcAlpha
    Offset 0, -1
    SetTexture [_MainTex] { combine texture * primary }
    SetTexture [_MainTex] { ConstantColor [_Color] combine constant * previous }
    SetTexture [_MainTex] { ConstantColor [_Color] combine constant * previous }
   }
  }
  }