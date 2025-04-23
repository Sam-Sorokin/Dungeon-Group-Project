// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Explosion"
{
	Properties
	{
		_Explosion_Tex("Explosion_Tex", 2D) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			half2 vertexToFrag4;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Explosion_Tex;
		SamplerState sampler_Explosion_Tex;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			half2 appendResult5 = (half2(v.texcoord.x , v.texcoord.y));
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles3 = 8.0 * 8.0;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset3 = 1.0f / 8.0;
			float fbrowsoffset3 = 1.0f / 8.0;
			// Speed of animation
			float fbspeed3 = _Time[ 1 ] * 0.0;
			// UV Tiling (col and row offset)
			float2 fbtiling3 = float2(fbcolsoffset3, fbrowsoffset3);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex3 = round( fmod( fbspeed3 + ( v.texcoord.z * 63.0 ), fbtotaltiles3) );
			fbcurrenttileindex3 += ( fbcurrenttileindex3 < 0) ? fbtotaltiles3 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox3 = round ( fmod ( fbcurrenttileindex3, 8.0 ) );
			// Multiply Offset X by coloffset
			float fboffsetx3 = fblinearindextox3 * fbcolsoffset3;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy3 = round( fmod( ( fbcurrenttileindex3 - fblinearindextox3 ) / 8.0, 8.0 ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy3 = (int)(8.0-1) - fblinearindextoy3;
			// Multiply Offset Y by rowoffset
			float fboffsety3 = fblinearindextoy3 * fbrowsoffset3;
			// UV Offset
			float2 fboffset3 = float2(fboffsetx3, fboffsety3);
			// Flipbook UV
			half2 fbuv3 = appendResult5 * fbtiling3 + fboffset3;
			// *** END Flipbook UV Animation vars ***
			o.vertexToFrag4 = fbuv3;
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			half4 tex2DNode1 = tex2D( _Explosion_Tex, i.vertexToFrag4 );
			o.Emission = tex2DNode1.rgb;
			o.Alpha = ( tex2DNode1.a * i.vertexColor.a );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
0;621.3334;1058.333;356.3333;2359.817;496.5352;2.991243;True;False
Node;AmplifyShaderEditor.TexCoordVertexDataNode;2;-1660.766,75.30044;Inherit;False;0;4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;5;-1331.3,85.94727;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1385.87,271.1931;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;63;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;3;-1074.219,80.71321;Inherit;False;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;8;False;2;FLOAT;8;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.VertexToFragmentNode;4;-799.975,81.63918;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexColorNode;8;-408.0673,311.0546;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-519.6595,55.46386;Inherit;True;Property;_Explosion_Tex;Explosion_Tex;1;0;Create;True;0;0;False;0;False;-1;95e28f80a9cb13346ba155bc68c52e41;d71ea9af935ef8f419c8b06f5f55c4cd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-12.63855,259.7644;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;369.5911,38.25359;Half;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Explosion;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;True;Transparent;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;5;0;2;1
WireConnection;5;1;2;2
WireConnection;6;0;2;3
WireConnection;3;0;5;0
WireConnection;3;4;6;0
WireConnection;4;0;3;0
WireConnection;1;1;4;0
WireConnection;7;0;1;4
WireConnection;7;1;8;4
WireConnection;0;2;1;0
WireConnection;0;9;7;0
ASEEND*/
//CHKSM=E4B67DAF55F121885406F514AB7167D1C36C41B6