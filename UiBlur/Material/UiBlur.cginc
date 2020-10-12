struct appdata {
  half4 vertex : POSITION;
  half2 uv : TEXCOORD0;
  half4 color : COLOR;
};

struct v2f {
  half4 vertex : POSITION;
  half2 uv : TEXCOORD0;
  half4 uvgrab : TEXCOORD1;
  half4 color : COLOR;
};

static const int WEIGHT_COUNT = 9;
static const half WEIGHTS[WEIGHT_COUNT] = {
  0.05, 0.09, 0.12, 0.15, 0.18, 0.15, 0.12, 0.09, 0.05
};

half _Size;
sampler2D _MainTex;
float4 _MainTex_ST;
sampler2D _GrabTexture;
half4 _GrabTexture_TexelSize;

v2f vert(appdata v) {
  v2f o;
  o.vertex = UnityObjectToClipPos(v.vertex);
  o.uv = TRANSFORM_TEX(v.uv, _MainTex);
  o.uvgrab = ComputeGrabScreenPos(o.vertex);
  o.color = v.color;
  return o;
}

half4 blur(v2f i, half a, half w, half kx, half ky) {
  return tex2Dproj(_GrabTexture, 
                   UNITY_PROJ_COORD(half4(i.uvgrab.x + 
                                          _GrabTexture_TexelSize.x * kx * _Size * a, 
                                          i.uvgrab.y + 
                                          _GrabTexture_TexelSize.y * ky * _Size * a, 
                                          i.uvgrab.z, i.uvgrab.w))) * w;
}

half4 blurX(v2f src) : SV_Target {
  half4 dst = 0;
  half4 c = tex2D(_MainTex, src.uv);
  for(int i = 0; i < WEIGHT_COUNT; i++) {
    dst += blur(src, src.color.a * c.a, WEIGHTS[i], i - (WEIGHT_COUNT / 2), 0.0);
  }
  dst.a = 1.0;
  return dst;
}

half4 blurY(v2f src) : SV_Target {
  half4 dst = 0;
  half4 c = tex2D(_MainTex, src.uv);
  for(int i = 0; i < WEIGHT_COUNT; i++) {
    dst += blur(src, src.color.a * c.a, WEIGHTS[i], 0.0, i - (WEIGHT_COUNT / 2));
  }
  dst.rgb *= c.rgb * src.color.rgb;
  dst.a = c.a;
  return dst;
}

half4 test(v2f src) : SV_Target {
  return tex2D(_MainTex, src.uv);
}
