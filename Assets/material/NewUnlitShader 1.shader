Shader "Custom/FadeFromCenterSmooth"
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
                // ��ȡ������ɫ
                fixed4 col = tex2D(_MainTex, i.uv);

                // ��ȡ���������Բ�ĵľ��루����Sprite��UV�Ǳ�׼����(0,0)��(1,1)��
                float2 center = float2(0.5, 0.5);
                float dist = distance(i.uv, center);

                // ʹ��ƽ������������͸���ȣ����Բ�ֵ��
                float alpha = smoothstep(_FadeRadius - 0.01, _FadeRadius + 0.01, dist);

                // Ӧ��͸����
                col.a *= alpha;

                return col;
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}
