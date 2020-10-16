Shader "unitwb/DistortionMask" {
  Properties {
    [PerRenderData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Size("Size", Float) = 64
  }

  Category {
    SubShader {
      Tags {
        "Queue" = "Transparent"
        "RenderType" = "Transparent"
      }
      Cull Off
      ZWrite Off
      Blend SrcAlpha OneMinusSrcAlpha
      GrabPass {}
      Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        #include "DistortionMask.cginc"
        ENDCG
      }
    }
  }
}
