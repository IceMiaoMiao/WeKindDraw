// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Edu/Wave" 
{
    
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Magnitude("Magnitude",Range(0,10)) = 1
        _Frequency("Frequency",Range(0,10)) = 1
        _WaveLen("WaveLen",Range(0,100)) = 10
        _Speed("Speed",Range(-10,10)) = 10
    }
    SubShader {
        Tags 
        { 
            "RenderType"="Opaque" 
            "Queue" = "Transparent" 
            "IgnoreProjector" = "True"
            "DisableBatching" = "True"
        }
        Pass
        {
            Tags{"LightMode" = "ForwardBase"}
            ZWrite off
            Cull off
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Magnitude;
            half _Frequency;
            half _WaveLen;
            half _Speed;
            struct a2v
            {
                float4 vertex:POSITION;
                float4 texcoord:TEXCOORD0;
            };
            struct v2f
            {
                float4 pos:SV_POSITION;
                float2 uv:TEXCOORD0;
            };
            v2f vert(a2v v)
            {
                v2f o;
                float4 offset;
                offset.yzw = float3(0.0,0.0,0.0);
                // _Time.y 、 _Frequency决定频率
                // _WaveLen 决定波长
                // _Magnitude 决定振幅
                offset.x = sin(_Frequency * _Time.y + v.vertex.x * _WaveLen + v.vertex.y * _WaveLen + v.vertex.z * _WaveLen)*_Magnitude; 
                //对模型顶点进行横向偏移
                o.pos = UnityObjectToClipPos(v.vertex + offset);
                o.uv = TRANSFORM_TEX(v.texcoord,_MainTex);
                //潜意识中的错误写法
                //o.uv += float2(_Time.y * _Speed,0.0);
                //实际上y方向在目标场景中是水平方向
                //让纹理在该方向上进行“流动”
                o.uv += float2(0.0,_Time.y * _Speed);
                return o;
            }
            fixed4 frag(v2f i):SV_Target
            {
                return fixed4(tex2D(_MainTex,i.uv) * _Color);
            }
            ENDCG
        }
    }
    FallBack "Transparent/VertexLit"
}


