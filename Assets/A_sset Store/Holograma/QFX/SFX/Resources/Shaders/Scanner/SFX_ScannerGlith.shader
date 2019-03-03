// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "QFX/SFX/Scanner/Scanner Glith"
{
	Properties
	{
		[HDR]_TintColor("Tint Color", Color) = (1,1,1,1)
		_MainTex("Main Tex", 2D) = "white" {}
		_TexturePow("Texture Pow", Range( 0.2 , 10)) = 5.316663
		_PatternTexture("Pattern Texture", 2D) = "white" {}
		[HDR]_DepthColor("Depth Color", Color) = (1,1,1,1)
		_DepthDistance("Depth Distance", Float) = 0.25
		_DepthFadeExp("Depth Fade Exp", Range( 0.2 , 10)) = 5.316663
		_Glitch("Glitch", Range( 0.1 , 1)) = 1
		_AppearProgress("Appear Progress", Range( 0 , 1)) = 1
		_MaskSpeed("Mask Speed", Vector) = (0,0.5,0,0)
		_MaskMap("Mask Map", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#include "UnityCG.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
			};

			uniform float _Glitch;
			uniform float4 _TintColor;
			uniform sampler2D _PatternTexture;
			uniform float4 _PatternTexture_ST;
			uniform sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float _TexturePow;
			uniform sampler2D _CameraDepthTexture;
			uniform float _DepthDistance;
			uniform float _DepthFadeExp;
			uniform float4 _DepthColor;
			uniform sampler2D _MaskMap;
			uniform float2 _MaskSpeed;
			uniform float4 _MaskMap_ST;
			uniform float _AppearProgress;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord2 = screenPos;
				
				o.ase_texcoord = v.vertex;
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float temp_output_10_0_g1 = sin( ( ( _Time.y * 100.0 ) + i.ase_texcoord.xyz.y ) );
				float2 uv_PatternTexture = i.ase_texcoord1.xy * _PatternTexture_ST.xy + _PatternTexture_ST.zw;
				float2 uv_MainTex = i.ase_texcoord1.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 temp_cast_0 = (_TexturePow).xxxx;
				float4 screenPos = i.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth476 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD(screenPos))));
				float distanceDepth476 = abs( ( screenDepth476 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _DepthDistance ) );
				float clampResult479 = clamp( ( 1.0 - distanceDepth476 ) , 0.0 , 1.0 );
				float4 temp_output_482_0 = ( pow( clampResult479 , _DepthFadeExp ) * _DepthColor );
				float2 uv_MaskMap = i.ase_texcoord1.xy * _MaskMap_ST.xy + _MaskMap_ST.zw;
				float2 panner580 = ( _Time.y * _MaskSpeed + uv_MaskMap);
				float2 uv588 = i.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float smoothstepResult592 = smoothstep( 0.0 , saturate( _AppearProgress ) , ( 0.5 * uv588.y ));
				
				
				finalColor = ( ( saturate( ( step( temp_output_10_0_g1 , _Glitch ) + step( temp_output_10_0_g1 , ( _Glitch * 0.5 ) ) ) ) * ( ( ( ( _TintColor * tex2D( _PatternTexture, uv_PatternTexture ) ) + ( _TintColor * saturate( pow( tex2D( _MainTex, uv_MainTex ) , temp_cast_0 ) ) ) ) + temp_output_482_0 ) * tex2D( _MaskMap, panner580 ).r ) ) * saturate( ( 1.0 - smoothstepResult592 ) ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=15401
98;206;1680;487;-3384.713;-748.3142;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;475;1021.198,541.3701;Float;False;Property;_DepthDistance;Depth Distance;7;0;Create;True;0;0;False;0;0.25;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;476;1232.361,545.9961;Float;False;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;496;1284.958,405.7684;Float;False;Property;_TexturePow;Texture Pow;2;0;Create;True;0;0;False;0;5.316663;1.95;0.2;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;473;1266.317,209.0419;Float;True;Property;_MainTex;Main Tex;1;0;Create;True;0;0;False;0;None;529239097d02f9f42b0ddd436c6fcbb0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;477;1427.36,544.9961;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;495;1572.227,306.5018;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;479;1589.215,545.645;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;497;1719.277,306.5934;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;534;2403.921,267.639;Float;True;Property;_PatternTexture;Pattern Texture;3;0;Create;True;0;0;False;0;None;529239097d02f9f42b0ddd436c6fcbb0;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;478;1466.831,701.1641;Float;False;Property;_DepthFadeExp;Depth Fade Exp;8;0;Create;True;0;0;False;0;5.316663;5.316663;0.2;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;472;1604.804,-23.96548;Float;False;Property;_TintColor;Tint Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;10,1.862068,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;483;1783.806,765.9655;Float;False;Property;_DepthColor;Depth Color;6;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;599;4460.036,1069.409;Float;False;190;119;changes in the script;1;598;;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;574;2561.581,1217.696;Float;False;Property;_MaskSpeed;Mask Speed;12;0;Create;True;0;0;False;0;0,0.5;0,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;555;4187.672,1107.404;Float;False;Property;_AppearProgress;Appear Progress;11;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;588;4143.523,954.7515;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;590;4143.518,846.2626;Float;False;Constant;_Float0;Float 0;13;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;474;1852.804,112.0345;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;571;2529.034,1069.912;Float;False;0;554;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;538;2726.231,190.3834;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TimeNode;573;2566.023,1356.241;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;480;1787.035,593.1641;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;537;2808.185,579.2836;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;598;4479.299,1111.433;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;589;4428.744,849.7624;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;580;2865.053,1160.175;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;482;1999.196,587.7283;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;481;2860.053,784.8444;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;554;3126.799,1027.494;Float;True;Property;_MaskMap;Mask Map;13;0;Create;True;0;0;False;0;None;22440178d4b71bb4f9b12ef5ffd25697;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;592;4657.967,849.7623;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;566;3665.914,796.6874;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;595;4855.7,848.0126;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;553;3738.785,377.6218;Float;False;Simple Glitch;9;;1;bb16914625242fb46ba2e0385c26d46a;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;581;3967.107,579.2476;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;597;5032.433,846.2629;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;489;1586.017,976.8276;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;594;5098.653,585.3767;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;491;1490.35,1112.853;Float;False;Property;_FresnelPower;Fresnel Power;5;0;Create;True;0;0;False;0;2.000714;1;0.1;6;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;498;1944.348,1319.718;Float;False;Property;_FrenselColor;Frensel Color;4;1;[HDR];Create;True;0;0;False;0;1,1,1,1;2,1.117241,0,1;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;510;2169.867,945.7825;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;492;1928.388,1093.565;Float;True;2;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;490;1739.634,976.9796;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;488;1242.017,884.8275;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;484;817.6531,926.2236;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;485;816.057,1081.286;Float;False;Constant;_Vector1;Vector 1;3;0;Create;True;0;0;False;0;0,0,-1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMaxOpNode;494;2441.038,822.8612;Float;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldNormalVector;487;1232.618,1038.227;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;499;2190.073,1204.793;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwitchByFaceNode;486;1008.27,1038.144;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;470;5365.555,582.2463;Float;False;True;2;Float;ASEMaterialInspector;0;1;QFX/SFX/Scanner/Scanner Glith;0770190933193b94aaa3065e307002fa;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;True;-1;False;-1;-1;False;-1;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;2;RenderType=Transparent;Queue=Transparent;True;2;0;False;False;False;False;False;False;False;False;False;False;0;;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;476;0;475;0
WireConnection;477;0;476;0
WireConnection;495;0;473;0
WireConnection;495;1;496;0
WireConnection;479;0;477;0
WireConnection;497;0;495;0
WireConnection;474;0;472;0
WireConnection;474;1;497;0
WireConnection;538;0;472;0
WireConnection;538;1;534;0
WireConnection;480;0;479;0
WireConnection;480;1;478;0
WireConnection;537;0;538;0
WireConnection;537;1;474;0
WireConnection;598;0;555;0
WireConnection;589;0;590;0
WireConnection;589;1;588;2
WireConnection;580;0;571;0
WireConnection;580;2;574;0
WireConnection;580;1;573;2
WireConnection;482;0;480;0
WireConnection;482;1;483;0
WireConnection;481;0;537;0
WireConnection;481;1;482;0
WireConnection;554;1;580;0
WireConnection;592;0;589;0
WireConnection;592;2;598;0
WireConnection;566;0;481;0
WireConnection;566;1;554;1
WireConnection;595;0;592;0
WireConnection;581;0;553;0
WireConnection;581;1;566;0
WireConnection;597;0;595;0
WireConnection;489;0;488;0
WireConnection;489;1;487;0
WireConnection;594;0;581;0
WireConnection;594;1;597;0
WireConnection;510;0;492;0
WireConnection;492;0;490;0
WireConnection;492;1;491;0
WireConnection;490;0;489;0
WireConnection;494;0;482;0
WireConnection;494;1;499;0
WireConnection;487;0;486;0
WireConnection;499;0;510;0
WireConnection;499;1;498;0
WireConnection;486;0;484;0
WireConnection;486;1;485;0
WireConnection;470;0;594;0
ASEEND*/
//CHKSM=D4F0278003ECEA049476F5C1288649DF95F3A9A1