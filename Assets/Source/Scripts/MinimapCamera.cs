using UnityEngine;

namespace Sander.DroneBattle
{
    public class MinimapCamera : MonoBehaviour
    {
        public SpriteRenderer BoundsRenderer;
        public RenderTexture TargetTexture;
        public Camera Cam;

        [Range(0.1f, 5f)]
        public float TextureScale = 1f;

        void Start()
        {
            SetupCamera();
        }

        void SetupCamera()
        {
            Bounds bounds = BoundsRenderer.bounds;

            transform.position = new Vector3(bounds.center.x, bounds.center.y, transform.position.z);
            Cam.orthographic = true;
            Cam.orthographicSize = bounds.size.y / 2f;

            float targetAspect = bounds.size.x / bounds.size.y;
            int texHeight = Mathf.RoundToInt(BoundsRenderer.sprite.texture.height * TextureScale);
            int texWidth = Mathf.RoundToInt(texHeight * targetAspect);

            if (TargetTexture.width != texWidth || TargetTexture.height != texHeight)
            {
                TargetTexture.Release();
                TargetTexture.width = texWidth;
                TargetTexture.height = texHeight;
                TargetTexture.Create();
            }

            Cam.targetTexture = TargetTexture;
        }
    }
}
