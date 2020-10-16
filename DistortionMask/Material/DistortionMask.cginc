struct appdata {
  float4 vertex : POSITION;
  float2 uv : TEXCOORD0;
  float4 color : COLOR;
};

struct v2f {
  float4 vertex : POSITION;
  float2 uv : TEXCOORD0;
  float4 color : COLOR;
  float4 grabPos : TEXCOORD1;
};

sampler2D _MainTex;
float4 _MainTex_ST;
float _Size;

sampler2D _GrabTexture;
float4 _GrabTexture_TexelSize;

v2f vert(appdata src) {
  v2f dst;
  dst.vertex = UnityObjectToClipPos(src.vertex);
  dst.uv = TRANSFORM_TEX(src.uv, _MainTex);
  dst.color = src.color;
  dst.grabPos = ComputeGrabScreenPos(dst.vertex);
  return dst;
}

float4 frag(v2f src) : SV_Target {
  float4 c = tex2D(_MainTex, src.uv);
  float2 pos = float2(c.x - 0.5, c.y - 0.5) * src.color.a * c.a * _Size;
  float4 dst = 
    tex2Dproj(_GrabTexture, 
              UNITY_PROJ_COORD(float4(src.grabPos.x + 
                                      _GrabTexture_TexelSize.x * (pos.x + 0.5), 
                                      src.grabPos.y + 
                                      _GrabTexture_TexelSize.y * (pos.y + 0.5), 
                                      src.grabPos.z, src.grabPos.w)));
  dst.rgb *= src.color.rgb;
  dst.a = 1.0;
  return dst;
}
