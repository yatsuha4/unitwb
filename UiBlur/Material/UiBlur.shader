Shader "unitwb/UiBlur" {
  Properties {
    [PerRenderData] _MainTex("Sprite Texture", 2D) = "white" {}
    _Size("Size", Float) = 4
  }

  Category {
    SubShader {
      Tags {
        "Queue" = "Transparent"
      }
      Cull Off
      GrabPass {}
      Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment blurX
        #include "UnityCG.cginc"
        #include "UiBlur.cginc"
        ENDCG
      }
      GrabPass {}
      Pass {
        CGPROGRAM
        #pragma vertex vert
        #pragma fragment blurY
        #include "UnityCG.cginc"
        #include "UiBlur.cginc"
        ENDCG
      }
    }
  }
}
