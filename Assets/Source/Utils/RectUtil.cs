using UnityEngine;

namespace Utils
{
    public static class RectUtil
    {
        public static void ScaleToHeight(this RectTransform rect, float? adjustToHeight)
        {
            if (adjustToHeight != null)
            {
                var scaleXY = (float) adjustToHeight / rect.rect.height;
                var scale = rect.localScale;
                scale.x = scaleXY;
                scale.y = scaleXY;
                rect.localScale = scale;
            }
        }
    }
}