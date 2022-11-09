Shader "Triniti/Extra/LightHaloSingle" {
Properties {
 _Color ("Main Color", Color) = (0,0,0,0)
 _Halo ("Halo (RGBA)", 2D) = "black" {}
}
SubShader { 
 Tags { "QUEUE"="Transparent+1" }
 Pass {
  Tags { "QUEUE"="Transparent+1" }
  Color [_Color]
  Fog { Mode Off }
  Blend DstColor One
  SetTexture [_Halo] { combine texture * primary }
  SetTexture [_Halo] { combine texture * previous double }
 }
}
}