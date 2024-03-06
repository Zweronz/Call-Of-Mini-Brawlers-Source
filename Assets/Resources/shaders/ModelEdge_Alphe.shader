Shader "Triniti/Model/ModelEdge_Alphe" {
  Properties {
   _MainTex ("Texture (RGB)", 2D) = "white" {}
   _Color ("Color", Color) = (1,1,1,1)
   _AtmoColor ("Atmosphere Color", Color) = (0.5,0.5,1,1)
  }
  SubShader { 
   Tags { "QUEUE"="Transparent" }
   Pass {
    Name "PLANETBASE"
    Tags { "LIGHTMODE"="Always" "QUEUE"="Transparent" }
    BindChannels {
     Bind "vertex", Vertex
     Bind "normal", Normal
     Bind "texcoord", TexCoord0
    }
    Blend SrcAlpha OneMinusSrcAlpha
      CGPROGRAM
          #pragma vertex vert
          #pragma fragment frag
  
          struct v2f {
              float2 texcoord0 : TEXCOORD0;
              float2 texcoord1 : TEXCOORD1;
              float2 texcoord2 : TEXCOORD2;
              float2 texcoord3 : TEXCOORD3;
              float4 vertex : POSITION;
          };
  
          struct appdata_t {
              float2 texcoord0 : TEXCOORD0;
              float2 texcoord1 : TEXCOORD1;
              float2 texcoord2 : TEXCOORD2;
              float2 texcoord3 : TEXCOORD3;
              float3 normal : NORMAL;
              float4 vertex : POSITION;
          };
  
          sampler2D _MainTex;
          float4 _Color, _AtmoColor, _MainTex_ST;
  
          v2f vert(appdata_t v) {
              v2f o;
              float3 tmpvar_1;
              float4 tmpvar_2;
              tmpvar_2.w = 0.000000;
              tmpvar_2.xyz = normalize(v.normal);
              float3 tmpvar_3;
              tmpvar_3 = normalize(UnityObjectToClipPos(v.vertex).xyz);
              float tmpvar_4;
              tmpvar_4 = clamp ((((tmpvar_3.x * tmpvar_3.x) + (tmpvar_3.y * tmpvar_3.y)) - ((tmpvar_3.z * tmpvar_3.z) * 0.500000)), 0.000000, 1.00000);
              o.vertex = UnityObjectToClipPos(v.vertex);
              o.texcoord0 = tmpvar_3;
              o.texcoord1 = tmpvar_1;
              o.texcoord2 = ((v.texcoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
              o.texcoord3 = (tmpvar_4 * tmpvar_4);
              return o;
          }
  
          float4 frag(v2f i) : SV_TARGET {
              float4 tmpvar_1;
              tmpvar_1 = tex2D(_MainTex, i.texcoord2);
              return lerp((tmpvar_1 * _Color), _AtmoColor, float4(i.texcoord3.xyxy));
          }
  
      ENDCG
   }
  }
  }