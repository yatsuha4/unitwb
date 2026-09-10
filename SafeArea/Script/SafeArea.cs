using UnityEngine;
using UnityEngine.UI;

namespace unitwb
{
    /**
     * <summary>セーフエリア</summary>
     */
    public class SafeArea: MonoBehaviour
    {
        private Canvas canvas;
        private float? canvasScale;

        /**
         */
        void Awake()
        {
            this.canvas = GetComponentInParent<Canvas>();
            UpdateLayout();
        }

        /**
         */
        void Update()
        {
            UpdateLayout();
        }

        /**
         */
        private void UpdateLayout()
        {
            var canvasScale = Screen.height /
                this.canvas.GetComponent<RectTransform>().sizeDelta.y;
            if(!this.canvasScale.HasValue || this.canvasScale.Value != canvasScale)
            {
                this.canvasScale = canvasScale;
                var resolution = new Vector2(Screen.width, Screen.height);
                var safeArea = Screen.safeArea;
                var transform = GetComponent<RectTransform>();
                transform.offsetMin = safeArea.min / this.canvasScale.Value;
                transform.offsetMax = (safeArea.max - resolution) / this.canvasScale.Value;
            }
        }
    }
}
