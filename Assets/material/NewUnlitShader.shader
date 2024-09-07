Shader "Custom/FadeFromCenterBinary"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FadeRadius ("Fade Radius", Range(0, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 200

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _FadeRadius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 获取纹理颜色
                fixed4 col = tex2D(_MainTex, i.uv);

                // 获取像素相对于圆心的距离（假设Sprite的UV是标准化的(0,0)到(1,1)）
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // 如果距离大于FadeRadius，像素透明，否则不透明
                if (dist > _FadeRadius)
                {
                    col.a = 1.0; // 不透明
                }
                else
                {
                    col.a = 0.0; // 完全透明
                }

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}
