// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support

Shader "Unlit/UnlitColorTextureAlpha" 
{
    Properties 
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull mode", Float) = 2
    }
    
    SubShader {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100
        Cull [_Cull]
        
        //  ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha 
        Pass {  
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"
            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            
            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                //  UNITY_VERTEX_OUTPUT_STEREO
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            fixed4 _Color;
            fixed4 _BaseColor;

            //  v2f vert (appdata_t v)
            //  {
                //      v2f o;
                //      UNITY_SETUP_INSTANCE_ID(v);
                //      UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                //      o.vertex = UnityObjectToClipPos(v.vertex);
                //      o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                //      UNITY_TRANSFER_FOG(o,o.vertex);
                //      return o;
            //  }

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.texcoord) * _BaseColor;
                col.rgb = col.rgb + (_Color.rgb * _Color.a);
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
    
}