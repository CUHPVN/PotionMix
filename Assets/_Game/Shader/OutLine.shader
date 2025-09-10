Shader "Custom/SpriteOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineSize ("Outline Size", Range(0,10)) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.texcoord;
                float alpha = tex2D(_MainTex, uv).a;

                // kiểm tra pixel lân cận để tạo outline
                float outline = 0;
                float2 offset = _MainTex_TexelSize.xy * _OutlineSize;
                outline += tex2D(_MainTex, uv + float2(offset.x, 0)).a;
                outline += tex2D(_MainTex, uv - float2(offset.x, 0)).a;
                outline += tex2D(_MainTex, uv + float2(0, offset.y)).a;
                outline += tex2D(_MainTex, uv - float2(0, offset.y)).a;

                fixed4 col = tex2D(_MainTex, uv);

                if (alpha == 0 && outline > 0)
                {
                    return _OutlineColor;
                }

                return col;
            }
            ENDCG
        }
    }
}
