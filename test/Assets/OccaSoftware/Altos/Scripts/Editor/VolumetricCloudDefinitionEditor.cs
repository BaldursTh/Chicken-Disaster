﻿using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace OccaSoftware.Altos.Editor
{

    [CustomEditor(typeof(VolumetricCloudsDefinitionScriptableObject))]
    [CanEditMultipleObjects]
    public class VolumetricCloudDefinitionEditor : UnityEditor.Editor
    {
        private class Params
        {
            public class Editor
            {
                public static SerializedProperty pageSelection;
                public static SerializedProperty lowAltitudeModelingState;
                public static SerializedProperty lowAltitudeLightingState;
                public static SerializedProperty lowAltitudeWeatherState;
                public static SerializedProperty lowAltitudeBaseState;
                public static SerializedProperty lowAltitudeDetail1State;
                public static SerializedProperty lowAltitudeDetail2State;
                public static SerializedProperty lowAltitudeCurlState;
            }

            public class Basic
            {
                public static SerializedProperty stepCount;
                public static SerializedProperty blueNoise;
                public static SerializedProperty sunColor;
                public static SerializedProperty ambientExposure;
                public static SerializedProperty HGEccentricityForward;
                public static SerializedProperty HGEccentricityBackward;
                public static SerializedProperty HGBlend;
                public static SerializedProperty HGStrength;

                public static SerializedProperty celestialBodySelection;
                public static SerializedProperty planetRadius;
                public static SerializedProperty cloudLayerHeight;
                public static SerializedProperty cloudLayerThickness;
                public static SerializedProperty cloudFadeDistance;
                public static SerializedProperty fogPower;

                public static SerializedProperty renderScaleSelection;
                public static SerializedProperty renderScale;
                public static SerializedProperty renderInSceneView;
                public static SerializedProperty taaEnabled;
                public static SerializedProperty taaBlendFactor;
                public static SerializedProperty depthCullOptions;
                public static SerializedProperty subpixelJitterEnabled;
            }

            public class LowAlt
            {
                public static SerializedProperty extinctionCoefficient;
                public static SerializedProperty cloudiness;
                public static SerializedProperty heightDensityInfluence;
                public static SerializedProperty cloudinessDensityInfluence;
                public static SerializedProperty distantCoverageDepth;
                public static SerializedProperty distantCoverageAmount;


                public static SerializedProperty maxLightingDistance;
                public static SerializedProperty multipleScatteringAmpGain;
                public static SerializedProperty multipleScatteringDensityGain;
                public static SerializedProperty multipleScatteringOctaves;

                public class Weather
                {
                    public static SerializedProperty weathermapTexture;
                    public static SerializedProperty weathermapVelocity;
                    public static SerializedProperty weathermapScale;
                    public static SerializedProperty weathermapValueRange;
                    public static SerializedProperty weathermapType;
                }

                public class Base
                {
                    public static SerializedProperty baseTextureID;
                    public static SerializedProperty baseTextureQuality;
                    public static SerializedProperty baseTextureScale;
                    public static SerializedProperty baseTextureTimescale;
                    public static SerializedProperty baseCurlAdjustment;

                    public static SerializedProperty baseFalloffSelection;
                    public static SerializedProperty baseTextureRInfluence;
                    public static SerializedProperty baseTextureGInfluence;
                    public static SerializedProperty baseTextureBInfluence;
                    public static SerializedProperty baseTextureAInfluence;
                }

                public class Detail1
                {
                    public static SerializedProperty detail1TextureID;
                    public static SerializedProperty detail1TextureQuality;
                    public static SerializedProperty detail1TextureInfluence;
                    public static SerializedProperty detail1TextureScale;
                    public static SerializedProperty detail1TextureTimescale;

                    public static SerializedProperty detail1FalloffSelection;
                    public static SerializedProperty detail1TextureRInfluence;
                    public static SerializedProperty detail1TextureGInfluence;
                    public static SerializedProperty detail1TextureBInfluence;
                    public static SerializedProperty detail1TextureAInfluence;

                    public static SerializedProperty detail1TextureHeightRemap;
                }

                public class Detail2
                {
                    public static SerializedProperty detail2TextureID;
                    public static SerializedProperty detail2TextureQuality;
                    public static SerializedProperty detail2TextureInfluence;
                    public static SerializedProperty detail2TextureScale;
                    public static SerializedProperty detail2TextureTimescale;

                    public static SerializedProperty detail2FalloffSelection;
                    public static SerializedProperty detail2TextureRInfluence;
                    public static SerializedProperty detail2TextureGInfluence;
                    public static SerializedProperty detail2TextureBInfluence;
                    public static SerializedProperty detail2TextureAInfluence;

                    public static SerializedProperty detail2TextureHeightRemap;
                }

                public class Curl
                {
                    public static SerializedProperty curlTexture;
                    public static SerializedProperty curlTextureInfluence;
                    public static SerializedProperty curlTextureScale;
                    public static SerializedProperty curlTextureTimescale;
                }
            }

            public class HighAlt
            {
                public static SerializedProperty highAltExtinctionCoefficient;
                public static SerializedProperty highAltCloudiness;


                public static SerializedProperty highAltTex1;
                public static SerializedProperty highAltScale1;
                public static SerializedProperty highAltTimescale1;
                public static SerializedProperty highAltStrength1;


                public static SerializedProperty highAltTex2;
                public static SerializedProperty highAltScale2;
                public static SerializedProperty highAltTimescale2;
                public static SerializedProperty highAltStrength2;


                public static SerializedProperty highAltTex3;
                public static SerializedProperty highAltScale3;
                public static SerializedProperty highAltTimescale3;
                public static SerializedProperty highAltStrength3;
            }
        }

        private void SetupSerializedPropertyParams<T>()
        {
            foreach (FieldInfo field in typeof(T).GetFields())
            {
                field.SetValue(null, serializedObject.FindProperty(field.Name));
            }
        }

        private void OnEnable()
        {
            SetupSerializedPropertyParams<Params.Editor>();

            SetupSerializedPropertyParams<Params.Basic>();

            SetupSerializedPropertyParams<Params.LowAlt>();
            SetupSerializedPropertyParams<Params.LowAlt.Weather>();
            SetupSerializedPropertyParams<Params.LowAlt.Base>();
            SetupSerializedPropertyParams<Params.LowAlt.Detail1>();
            SetupSerializedPropertyParams<Params.LowAlt.Detail2>();
            SetupSerializedPropertyParams<Params.LowAlt.Curl>();

            SetupSerializedPropertyParams<Params.HighAlt>();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Draw();
            serializedObject.ApplyModifiedProperties();
        }

        private void Draw()
        {
            DrawCloudDefinitionHeader();

            switch (Params.Editor.pageSelection.enumValueIndex)
            {
                case 0: DrawVolumetricBasicSetup(); break;
                case 1: HandleLowAltitudeDrawing(); break;
                case 2: HandleHighAltitudeDrawing(); break;
                default: break;
            }
        }


        void DrawCloudDefinitionHeader()
        {
            if (GUILayout.Button("Basic Configuration"))
            {
                Params.Editor.pageSelection.enumValueIndex = 0;
            }

            if (GUILayout.Button("Low Altitude Configuration"))
            {
                Params.Editor.pageSelection.enumValueIndex = 1;
            }

            if (GUILayout.Button("High Altitude Configuration"))
            {
                Params.Editor.pageSelection.enumValueIndex = 2;
            }
        }

        #region Draw Basic Setup
        void DrawVolumetricBasicSetup()
        {
            #region Basic Setup
            EditorHelpers.HandleIndentedGroup("Rendering", DrawBasicRendering);
            EditorHelpers.HandleIndentedGroup("Lighting", DrawBasicLighting);
            EditorHelpers.HandleIndentedGroup("Atmosphere", DrawBasicAtmosphere);
            #endregion
        }

        void DrawBasicRendering()
        {
            EditorGUILayout.IntSlider(Params.Basic.stepCount, 1, 128);
            EditorGUILayout.Slider(Params.Basic.blueNoise, 0f, 1f);

            EditorGUILayout.PropertyField(Params.Basic.renderScaleSelection);
            if(GetEnum<RenderScaleSelection>(Params.Basic.renderScaleSelection) == RenderScaleSelection.Custom)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Slider(Params.Basic.renderScale, 0.1f, 1f);
                EditorGUI.indentLevel--;
            }
                
            EditorGUILayout.PropertyField(Params.Basic.renderInSceneView);
            EditorGUILayout.PropertyField(Params.Basic.taaEnabled);
            EditorGUILayout.Slider(Params.Basic.taaBlendFactor, 0f, 1f);
            EditorGUILayout.PropertyField(Params.Basic.subpixelJitterEnabled);
            EditorGUILayout.PropertyField(Params.Basic.depthCullOptions);
        }

        void DrawBasicLighting()
        {
            Params.Basic.sunColor.colorValue = EditorGUILayout.ColorField(new GUIContent("Sun Color"), Params.Basic.sunColor.colorValue, false, false, true);
            EditorGUILayout.Slider(Params.Basic.ambientExposure, 0f, 2f);
            EditorGUILayout.Slider(Params.Basic.HGStrength, 0f, 1f);
            EditorGUILayout.Slider(Params.Basic.HGEccentricityForward, 0f, 0.99f);
            EditorGUILayout.Slider(Params.Basic.HGEccentricityBackward, -0.99f, 0f);
        }

        void DrawBasicAtmosphere()
        {
            EditorGUILayout.PropertyField(Params.Basic.celestialBodySelection, new GUIContent("Planet Radius"));
            if (GetEnum<CelestialBodySelection>(Params.Basic.celestialBodySelection) == CelestialBodySelection.Custom)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(Params.Basic.planetRadius, new GUIContent("Planet Radius (km)"));
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Slider(Params.Basic.cloudLayerHeight, 0.3f, 4.0f, new GUIContent("Cloud Layer Altitude (km)"));
            EditorGUILayout.Slider(Params.Basic.cloudLayerThickness, 0.1f, 4.0f, new GUIContent("Cloud Layer Thickness (km)"));
            EditorGUILayout.Slider(Params.Basic.cloudFadeDistance, 5f, 35f, new GUIContent("Cloud Fade Distance (km)"));
            EditorGUILayout.Slider(Params.Basic.fogPower, 0f, 6f);
        }
        #endregion

        #region Low Altitude
        void HandleLowAltitudeDrawing()
        {
            Params.Editor.lowAltitudeModelingState.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeModelingState.boolValue, "Modeling", DrawLowAltitudeModeling);
            Params.Editor.lowAltitudeLightingState.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeLightingState.boolValue, "Lighting", DrawLowAltitudeLighting);
            Params.Editor.lowAltitudeWeatherState.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeWeatherState.boolValue, "Weather", DrawLowAltitudeWeather);
            Params.Editor.lowAltitudeBaseState.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeBaseState.boolValue, "Base Texture", DrawLowAltitudeBase);

            if (GetEnum<TextureIdentifier>(Params.LowAlt.Base.baseTextureID) != TextureIdentifier.None)
            {
                Params.Editor.lowAltitudeDetail1State.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeDetail1State.boolValue, "Detail Texture 1", DrawLowAltitudeDetail1);
                Params.Editor.lowAltitudeDetail2State.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeDetail2State.boolValue, "Detail Texture 2", DrawLowAltitudeDetail2);
                Params.Editor.lowAltitudeCurlState.boolValue = EditorHelpers.HandleFoldOutGroup(Params.Editor.lowAltitudeCurlState.boolValue, "Curl Texture", DrawLowAltitudeCurl);
            }
        }

        void DrawLowAltitudeModeling()
        {
            #region Modeling
            EditorGUILayout.PropertyField(Params.LowAlt.extinctionCoefficient);
            EditorGUILayout.Slider(Params.LowAlt.cloudiness, 0f, 1f);

            EditorGUILayout.Slider(Params.LowAlt.cloudinessDensityInfluence, 0f, 1f);
            EditorGUILayout.Slider(Params.LowAlt.heightDensityInfluence, 0f, 1f);
            #endregion

            #region Distant Coverage Configuration
            // Distant Coverage Configuration
            EditorGUILayout.LabelField("Distant Coverage", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.Slider(Params.LowAlt.distantCoverageDepth, 5f, 35f, new GUIContent("Start Distance"));
            EditorGUILayout.Slider(Params.LowAlt.distantCoverageAmount, 0f, 1f, new GUIContent("Cloudiness"));
            EditorGUI.indentLevel--;
            #endregion
        }

        void DrawLowAltitudeLighting()
        {
            EditorGUILayout.PropertyField(Params.LowAlt.maxLightingDistance);
            EditorGUILayout.LabelField("Multiple Scattering", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.IntSlider(Params.LowAlt.multipleScatteringOctaves, 1, 4, new GUIContent("Octaves"));
            if (Params.LowAlt.multipleScatteringOctaves.intValue > 1)
            {
                EditorGUILayout.Slider(Params.LowAlt.multipleScatteringAmpGain, 0f, 1f, new GUIContent("Amp Gain"));
                EditorGUILayout.Slider(Params.LowAlt.multipleScatteringDensityGain, 0f, 1f, new GUIContent("Density Gain"));
            }
            EditorGUI.indentLevel--;
        }

        void DrawLowAltitudeWeather()
        {
            EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapType);
            if(GetEnum<WeathermapType>(Params.LowAlt.Weather.weathermapType) == WeathermapType.Texture)
            {
                EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapTexture);
                EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapVelocity);
            }
            else
            {
                EditorGUILayout.LabelField("Type: Perlin, Octaves: 1");
                EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapVelocity);
                EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapScale);
                EditorGUILayout.PropertyField(Params.LowAlt.Weather.weathermapValueRange);
            }
            
        }

        #region Draw Base
        void DrawLowAltitudeBase()
        {
            EditorGUILayout.PropertyField(Params.LowAlt.Base.baseTextureID, new GUIContent("Texture"));

            if (GetEnum<TextureIdentifier>(Params.LowAlt.Base.baseTextureID) != TextureIdentifier.None)
            {

                EditorGUILayout.PropertyField(Params.LowAlt.Base.baseTextureQuality, new GUIContent("Quality"));
                EditorGUILayout.Space(5);

                EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(Params.LowAlt.Base.baseTextureScale, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(Params.LowAlt.Base.baseTextureTimescale, new GUIContent("Timescale"));
                EditorGUILayout.Space(5);

                EditorGUILayout.LabelField("Texture Channel Strength", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(Params.LowAlt.Base.baseFalloffSelection, new GUIContent("Falloff"));
                if(GetEnum<FalloffSelection>(Params.LowAlt.Base.baseFalloffSelection) == FalloffSelection.Custom)
                {
                    EditorGUILayout.Slider(Params.LowAlt.Base.baseTextureRInfluence, 0f, 1f, new GUIContent("R"));
                    EditorGUILayout.Slider(Params.LowAlt.Base.baseTextureGInfluence, 0f, 1f, new GUIContent("G"));
                    EditorGUILayout.Slider(Params.LowAlt.Base.baseTextureBInfluence, 0f, 1f, new GUIContent("B"));
                    EditorGUILayout.Slider(Params.LowAlt.Base.baseTextureAInfluence, 0f, 1f, new GUIContent("A"));
                }

                EditorGUILayout.Space(5);
                EditorGUILayout.LabelField("Curl Influence", EditorStyles.boldLabel);
                EditorGUILayout.Slider(Params.LowAlt.Base.baseCurlAdjustment, 0f, 1f);
            }
        }
        #endregion


        #region Draw Detail
        private void DrawLowAltitudeDetail1()
        {
            DetailData detailData = new DetailData
            {
                texture = Params.LowAlt.Detail1.detail1TextureID,
                quality = Params.LowAlt.Detail1.detail1TextureQuality,
                influence = Params.LowAlt.Detail1.detail1TextureInfluence,
                heightRemap = Params.LowAlt.Detail1.detail1TextureHeightRemap,
                scale = Params.LowAlt.Detail1.detail1TextureScale,
                timescale = Params.LowAlt.Detail1.detail1TextureTimescale,
                falloffSelection = Params.LowAlt.Detail1.detail1FalloffSelection,
                r = Params.LowAlt.Detail1.detail1TextureRInfluence,
                g = Params.LowAlt.Detail1.detail1TextureGInfluence,
                b = Params.LowAlt.Detail1.detail1TextureBInfluence,
                a = Params.LowAlt.Detail1.detail1TextureAInfluence
            };

            DrawDetail(detailData);
        }

        private void DrawLowAltitudeDetail2()
        {
            DetailData detailData = new DetailData
            {
                texture = Params.LowAlt.Detail2.detail2TextureID,
                quality = Params.LowAlt.Detail2.detail2TextureQuality,
                influence = Params.LowAlt.Detail2.detail2TextureInfluence,
                heightRemap = Params.LowAlt.Detail2.detail2TextureHeightRemap,
                scale = Params.LowAlt.Detail2.detail2TextureScale,
                timescale = Params.LowAlt.Detail2.detail2TextureTimescale,
                falloffSelection = Params.LowAlt.Detail2.detail2FalloffSelection,
                r = Params.LowAlt.Detail2.detail2TextureRInfluence,
                g = Params.LowAlt.Detail2.detail2TextureGInfluence,
                b = Params.LowAlt.Detail2.detail2TextureBInfluence,
                a = Params.LowAlt.Detail2.detail2TextureAInfluence
            };

            DrawDetail(detailData);
        }

        private void DrawDetail(DetailData detailData)
        {
            EditorGUILayout.PropertyField(detailData.texture, new GUIContent("Texture"));

            if (GetEnum<TextureIdentifier>(detailData.texture) != TextureIdentifier.None)
            {
                EditorGUILayout.PropertyField(detailData.quality, new GUIContent("Quality"));
                EditorGUILayout.Space(5);

                EditorGUILayout.LabelField("Strength", EditorStyles.boldLabel);
                EditorGUILayout.Slider(detailData.influence, 0f, 1f, new GUIContent("Strength"));
                EditorGUILayout.PropertyField(detailData.heightRemap, new GUIContent("Height Remap"));
                EditorGUILayout.Space(5);

                EditorGUILayout.LabelField("Scale", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(detailData.scale, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(detailData.timescale, new GUIContent("Timescale"));
                EditorGUILayout.Space(5);

                EditorGUILayout.LabelField("Texture Channel Strength", EditorStyles.boldLabel);
                EditorGUILayout.PropertyField(detailData.falloffSelection, new GUIContent("Falloff"));
                if(GetEnum<FalloffSelection>(detailData.falloffSelection) == FalloffSelection.Custom)
                {
                    EditorGUILayout.Slider(detailData.r, 0f, 1f, new GUIContent("R"));
                    EditorGUILayout.Slider(detailData.g, 0f, 1f, new GUIContent("G"));
                    EditorGUILayout.Slider(detailData.b, 0f, 1f, new GUIContent("B"));
                    EditorGUILayout.Slider(detailData.a, 0f, 1f, new GUIContent("A"));
                }
            }
        }

        private struct DetailData
        {
            public SerializedProperty texture;
            public SerializedProperty quality;
            public SerializedProperty influence;
            public SerializedProperty heightRemap;
            public SerializedProperty scale;
            public SerializedProperty timescale;
            public SerializedProperty falloffSelection;
            public SerializedProperty r;
            public SerializedProperty g;
            public SerializedProperty b;
            public SerializedProperty a;
        }
        #endregion


        #region Draw Curl
        private void DrawLowAltitudeCurl()
        {
            EditorGUILayout.PropertyField(Params.LowAlt.Curl.curlTexture);
            if (Params.LowAlt.Curl.curlTexture.objectReferenceValue != null)
            {
                EditorGUILayout.PropertyField(Params.LowAlt.Curl.curlTextureInfluence);
                EditorGUILayout.PropertyField(Params.LowAlt.Curl.curlTextureScale);
                EditorGUILayout.PropertyField(Params.LowAlt.Curl.curlTextureTimescale);
            }
        }
        #endregion
        #endregion

        void HandleHighAltitudeDrawing()
        {
            bool tex1State = Params.HighAlt.highAltTex1.objectReferenceValue != null ? true : false;
            bool tex2State = Params.HighAlt.highAltTex2.objectReferenceValue != null ? true : false;
            bool tex3State = Params.HighAlt.highAltTex3.objectReferenceValue != null ? true : false;
            bool aggTexState = tex1State || tex2State || tex3State ? true : false;
            
            if (aggTexState)
            {
                EditorGUILayout.LabelField("Basic Configuration", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(Params.HighAlt.highAltExtinctionCoefficient, new GUIContent("Extinction Coefficient"));
                EditorGUILayout.Slider(Params.HighAlt.highAltCloudiness, 0f, 1f, new GUIContent("Cloudiness"));
                EditorGUI.indentLevel--;
            }


            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("Texture Strength", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.Slider(Params.HighAlt.highAltStrength1, 0f, 1f, new GUIContent("Texture 1"));
            EditorGUILayout.Slider(Params.HighAlt.highAltStrength2, 0f, 1f, new GUIContent("Texture 2"));
            EditorGUILayout.Slider(Params.HighAlt.highAltStrength3, 0f, 1f, new GUIContent("Texture 3"));
            EditorGUI.indentLevel--;

            if (aggTexState)
            {
                EditorGUILayout.Space(10f);
                EditorGUILayout.LabelField("Textures", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
            }

            EditorGUILayout.LabelField("Texture 1", EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(Params.HighAlt.highAltTex1, new GUIContent("Texture"));
            if (tex1State)
            {
                EditorGUILayout.PropertyField(Params.HighAlt.highAltScale1, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(Params.HighAlt.highAltTimescale1, new GUIContent("Timescale"));
            }

            EditorGUI.indentLevel--;


            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("Texture 2", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(Params.HighAlt.highAltTex2, new GUIContent("Texture"));
            if (tex2State)
            {
                EditorGUILayout.PropertyField(Params.HighAlt.highAltScale2, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(Params.HighAlt.highAltTimescale2, new GUIContent("Timescale"));
            }
            EditorGUI.indentLevel--;


            EditorGUILayout.Space(10f);
            EditorGUILayout.LabelField("Texture 3", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(Params.HighAlt.highAltTex3, new GUIContent("Texture"));
            if (tex3State)
            {
                EditorGUILayout.PropertyField(Params.HighAlt.highAltScale3, new GUIContent("Scale"));
                EditorGUILayout.PropertyField(Params.HighAlt.highAltTimescale3, new GUIContent("Timescale"));
            }
            EditorGUI.indentLevel--;

            if (aggTexState) EditorGUI.indentLevel--;
        }

        T GetEnum<T>(SerializedProperty property)
        {
            return (T)Enum.ToObject(typeof(T), property.enumValueIndex);
        }
    }

    static class EditorHelpers
    {
        public static bool HandleFoldOutGroup(bool state, string header, Action controls)
        {
            state = EditorGUILayout.BeginFoldoutHeaderGroup(state, header);

            if (state)
            {
                EditorGUI.indentLevel++;
                controls();
                EditorGUI.indentLevel--;
                EditorGUILayout.Space(10);
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return state;
        }

        public static void HandleIndentedGroup(string header, Action controls)
        {
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            controls();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space(10);
        }
    }
}