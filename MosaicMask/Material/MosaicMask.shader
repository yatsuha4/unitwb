Shader "unitwb/MosaicMask" {
  Properties {
    [PerRenderData] _MainTex("Sprite Texture", 2D) = "white" {}
    _SizeX("SizeX", Float) = 0.01
    _SizeY("SizeY", Float) = 0.01
  }

  Category {
    SubShader {
      Tags {
        "Queue" = "Transparent"
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
        #include "MosaicMask.cginc"
        ENDCG
      }
    }
  }
}
