Shader "ZombieStreet/Shader2" {
Properties {
 _MainTex ("MainTex", 2D) = "" {}
 _Color ("Color", Color) = (1,1,1,1)
}
SubShader { 
 Tags { "QUEUE"="Transparent+100" }
 Pass {
  Tags { "QUEUE"="Transparent+100" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord
  }
  Color [_Color]
  ZWrite Off
  Cull Off
  Fog { Mode Off }
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { ConstantColor [_Color] combine texture * primary }
 }
}
}