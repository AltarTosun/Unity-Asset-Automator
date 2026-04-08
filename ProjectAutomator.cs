using System.IO;
using UnityEditor;
using UnityEngine;

// This script MUST be placed in a folder named "Editor"
public class ProjectAutomator : AssetPostprocessor
{
    private const string ProcessedMarker = "PROCESSED_BY_TECHART_V1";

    // --------------------------------------------------
    // TEXTURE OPTIMIZATION
    // --------------------------------------------------
    void OnPreprocessTexture()
    {
        TextureImporter importer = (TextureImporter)assetImporter;

        if (AlreadyProcessed(importer))
            return;

        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        // -------------------------------
        // SPRITE TEXTURES
        // -------------------------------
        if (fileName.StartsWith("TS_"))
        {
            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.mipmapEnabled = false;

            importer.alphaSource = TextureImporterAlphaSource.FromInput;
            importer.alphaIsTransparency = true;

            importer.filterMode = FilterMode.Bilinear;
            importer.wrapMode = TextureWrapMode.Clamp;

            TextureImporterSettings textureSettings = new TextureImporterSettings();
            importer.ReadTextureSettings(textureSettings);
            textureSettings.spriteGenerateFallbackPhysicsShape = false;
            importer.SetTextureSettings(textureSettings);

            ApplyTexturePlatformSettings(importer, "Android", 512, TextureImporterFormat.ASTC_6x6);
            ApplyTexturePlatformSettings(importer, "iPhone", 512, TextureImporterFormat.ASTC_6x6);

            MarkProcessed(importer);
            return;
        }

        // -------------------------------
        // GENERIC TEXTURES
        // -------------------------------
        if (fileName.StartsWith("T_"))
        {
            importer.textureType = TextureImporterType.Default;
            importer.mipmapEnabled = false;

            ApplyTexturePlatformSettings(importer, "Android", 512, TextureImporterFormat.ASTC_8x8);
            ApplyTexturePlatformSettings(importer, "iPhone", 512, TextureImporterFormat.ASTC_8x8);

            MarkProcessed(importer);
            return;
        }
    }

    // --------------------------------------------------
    // MODEL OPTIMIZATION
    // --------------------------------------------------
    void OnPreprocessModel()
    {
        ModelImporter importer = (ModelImporter)assetImporter;

        if (AlreadyProcessed(importer))
            return;

        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        // -------------------------------
        // STATIC MESH
        // -------------------------------
        if (fileName.StartsWith("SM_"))
        {
            importer.importBlendShapes = false;
            importer.importVisibility = false;
            importer.importCameras = false;
            importer.importLights = false;
            
            importer.animationType = ModelImporterAnimationType.None;
            
            importer.importAnimation = false;
            
            importer.materialImportMode = ModelImporterMaterialImportMode.None;
            
            MarkProcessed(importer);
            return;
        }

        // -------------------------------
        // RIGGED MESH
        // -------------------------------
        if (fileName.StartsWith("RM_"))
        {
            importer.importVisibility = false;
            importer.importCameras = false;
            importer.importLights = false;
            
            importer.animationCompression = ModelImporterAnimationCompression.Optimal;
            
            importer.materialImportMode = ModelImporterMaterialImportMode.None;

            MarkProcessed(importer);
            return;
        }
    }

    // --------------------------------------------------
    // AUDIO OPTIMIZATION
    // --------------------------------------------------
    void OnPreprocessAudio()
    {
        AudioImporter importer = (AudioImporter)assetImporter;

        if (AlreadyProcessed(importer))
            return;

        string fileName = Path.GetFileNameWithoutExtension(assetPath);

        if (fileName.StartsWith("SFX_"))
        {
            importer.forceToMono = true;

            AudioImporterSampleSettings settings = importer.defaultSampleSettings;
            settings.preloadAudioData = false;
            importer.loadInBackground = true;
            settings.loadType = AudioClipLoadType.CompressedInMemory;
            settings.compressionFormat = AudioCompressionFormat.ADPCM;
            settings.quality = 1f;
            importer.defaultSampleSettings = settings;

            MarkProcessed(importer);
            return;
        }

        if (fileName.StartsWith("Music_"))
        {
            importer.forceToMono = false;

            AudioImporterSampleSettings settings = importer.defaultSampleSettings;
            settings.preloadAudioData = false;
            settings.loadType = AudioClipLoadType.Streaming;
            importer.loadInBackground = true;
            settings.compressionFormat = AudioCompressionFormat.Vorbis;
            settings.quality = 0.7f;
            importer.defaultSampleSettings = settings;

            MarkProcessed(importer);
            return;
        }
    }

    // --------------------------------------------------
    // HELPERS
    // --------------------------------------------------
    private static bool AlreadyProcessed(AssetImporter importer)
    {
        return importer.userData == ProcessedMarker;
    }

    private static void MarkProcessed(AssetImporter importer)
    {
        importer.userData = ProcessedMarker;
    }

    private static void ApplyTexturePlatformSettings(
        TextureImporter importer,
        string platformName,
        int maxTextureSize,
        TextureImporterFormat format)
    {
        TextureImporterPlatformSettings settings = importer.GetPlatformTextureSettings(platformName);
        settings.name = platformName;
        settings.overridden = true;
        settings.maxTextureSize = maxTextureSize;
        settings.format = format;

        importer.SetPlatformTextureSettings(settings);
    }
}