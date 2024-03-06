Shader "Triniti/Scene/COL_LM_DO" {
Properties {
 _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
 _MainTex ("MainTex(RGB)", 2D) = "" {}
 _LightMap ("Lightmap (RGB)", 2D) = "white" {}
}
SubShader { 
 Tags { "QUEUE"="Geometry" }
 Pass {
  Tags { "QUEUE"="Geometry" }
  BindChannels {
   Bind "vertex", Vertex
   Bind "texcoord", TexCoord0
   Bind "texcoord1", TexCoord1
  }
  Color [_Color]
  Fog { Mode Off }
  SetTexture [_MainTex] { combine texture * primary double }
  SetTexture [_LightMap] { combine texture * previous }
 }
}
}