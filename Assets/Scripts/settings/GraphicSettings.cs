using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GraphicsSetting : MonoBehaviour
{
    public int i=0;

    public RenderPipelineAsset lowRenderer;
    public RenderPipelineAsset highRenderer;
        public RenderPipelineAsset extremeRenderer;
    void Start()
    {
         
        if (GameSettings.Instance != null)
        {
            int saved =GameSettings.Instance.graphics;
            if (saved == 0) SetLow();
            else if (saved == 1) SetMedium();
            else if (saved == 2) SetExtreme();
            else SetLow();
        }
        else
        {
            
            int saved=i;
            if (saved == 0) SetLow();
            else if (saved == 1) SetMedium();
            else if (saved == 2) SetExtreme();
            else SetLow();
        }
        
    }

    public void SetLow()
    {
    
        GraphicsSettings.defaultRenderPipeline = lowRenderer;
        QualitySettings.renderPipeline = lowRenderer;
        QualitySettings.SetQualityLevel(0, true);
        QualitySettings.globalTextureMipmapLimit =3;
        QualitySettings.shadowDistance = 0f;
        QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;
        QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low;
        QualitySettings.antiAliasing = 0;
        Application.targetFrameRate = 90;

    }

    public void SetMedium()
    {
        GraphicsSettings.defaultRenderPipeline = highRenderer;
        QualitySettings.renderPipeline = highRenderer;
        QualitySettings.SetQualityLevel(2, true);
        QualitySettings.globalTextureMipmapLimit = 1;
        QualitySettings.shadows = UnityEngine.ShadowQuality.HardOnly;
        QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low;
        QualitySettings.antiAliasing = 0;
        Application.targetFrameRate = 90;

    }

    public void SetExtreme()
    {
         GraphicsSettings.defaultRenderPipeline = extremeRenderer;
        QualitySettings.renderPipeline = extremeRenderer;
        QualitySettings.SetQualityLevel(4, true);
        QualitySettings.globalTextureMipmapLimit = 0;
        QualitySettings.shadows = UnityEngine.ShadowQuality.All;
         QualitySettings.shadowResolution = UnityEngine.ShadowResolution.Low;
        
        Application.targetFrameRate = 90;

    }
}