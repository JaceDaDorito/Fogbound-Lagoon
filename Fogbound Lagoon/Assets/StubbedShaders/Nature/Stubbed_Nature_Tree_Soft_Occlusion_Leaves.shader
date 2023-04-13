//////////////////////////////////////////
//
// NOTE: This is *not* a valid shader file
//
///////////////////////////////////////////
Shader "StubbedNature/Tree Soft Occlusion Leaves" {
Properties {
_Color ("Main Color", Color) = (1,1,1,1)
_MainTex ("Main Texture", 2D) = "white" { }
_Cutoff ("Alpha cutoff", Range(0.25, 0.9)) = 0.5
_BaseLight ("Base Light", Range(0, 1)) = 0.35
_AO ("Amb. Occlusion", Range(0, 10)) = 2.4
_Occlusion ("Dir Occlusion", Range(0, 20)) = 7.5
_TreeInstanceColor ("TreeInstanceColor", Vector) = (1,1,1,1)
_TreeInstanceScale ("TreeInstanceScale", Vector) = (1,1,1,1)
_SquashAmount ("Squash", Float) = 1
}
Fallback "Transparent/Cutout/VertexLit"
}