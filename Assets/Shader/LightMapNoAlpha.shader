Shader "DesertSlug3D/LightMapNoAlpha" {
Properties {
 _MainTex ("base", 2D) = "white" {}
 _Lightmap ("light map", 2D) = "white" {}
 _Fogmap ("fog map", 2D) = "black" {}
}
SubShader { 
 Tags { "QUEUE"="Geometry" "RenderType"="Opaque" }
 Pass {
  Tags { "QUEUE"="Geometry" "RenderType"="Opaque" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
  }
  SetTexture [_MainTex] { combine texture, texture alpha }
  SetTexture [_Lightmap] { combine previous * texture }
  SetTexture [_Fogmap] { combine previous + texture }
 }
}
}