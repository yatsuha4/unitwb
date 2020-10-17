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
float _SizeX;
float _SizeY;

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
  float4 c = tex2D(_MainTex, src.uv) * src.color;
  float2 pos = float2(((int)(src.grabPos.x / _SizeX) + 0.5) * _SizeX, 
                      ((int)(src.grabPos.y / _SizeY) + 0.5) * _SizeY);
  float4 dst = 
    tex2Dproj(_GrabTexture, 
              UNITY_PROJ_COORD(float4(pos, src.grabPos.z, src.grabPos.w)));
  dst.rgb *= c.rgb;
  dst.a = c.a;
  return dst;
}
