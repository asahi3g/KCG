//imports UnityEngine

namespace PixelEffect
{
    [UnityEngine.ExecuteInEditMode]
    [UnityEngine.RequireComponent(typeof(UnityEngine.Camera))]
    public class PixelEffect : UnityEngine.MonoBehaviour
    {
        [UnityEngine.Header("Pixels size")]
        [UnityEngine.Range(1.0f, 20f)]
        public float pixelWidth = 0.05f;
        [UnityEngine.Range(1.0f, 20f)]
        public float pixelHeight = 0.05f;

        private UnityEngine.Material pixelMaterial = null;

        void SetMaterial()
        {
            pixelMaterial = new UnityEngine.Material(UnityEngine.Shader.Find("Hidden/PixelShader"));
        }

        void OnEnable()
        {
            SetMaterial();
        }

        void OnDisable()
        {
            pixelMaterial = null;
        }

        void OnRenderImage(UnityEngine.RenderTexture source, UnityEngine.RenderTexture destination)
        {

            if (source == null)
            {
                return;
            }
            if (pixelMaterial == null)
            {
                UnityEngine.Graphics.Blit(source, destination);
                return;
            }

            pixelMaterial.SetFloat("_PixelWidth", pixelWidth);
            pixelMaterial.SetFloat("_PixelHeight", pixelHeight);
            pixelMaterial.SetFloat("_ScreenWidth", source.width);
            pixelMaterial.SetFloat("_ScreenHeight", source.height);

            UnityEngine.Graphics.Blit(source, destination, pixelMaterial);
        }
    }

}
