Shader "DesertSlug3D/LightMap" {
Properties {
 _MainTex ("base", 2D) = "white" {}
 _Lightmap ("light map", 2D) = "white" {}
 _Fogmap ("fog map", 2D) = "black" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent" "RenderType"="Opaque" }
 Pass {
  Tags { "QUEUE"="Transparent" "RenderType"="Opaque" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
  }
  Blend SrcAlpha OneMinusSrcAlpha
  SetTexture [_MainTex] { combine texture, texture alpha }
  SetTexture [_Lightmap] { combine previous * texture }
  SetTexture [_Fogmap] { combine previous + texture }
 }
}
}