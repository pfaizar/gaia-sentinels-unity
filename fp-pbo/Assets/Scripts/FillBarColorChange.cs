using UnityEngine;
using UnityEngine.UI;

namespace M_FillBarColorChange
{
    public class FillBarColorChange : MonoBehaviour
    {
        public Image ForegroundImage;
        public Color FlashForegroundColorFull;
        public Image BackgroundImage;
        public Color DefaultForegroundColor;
        public Color DefaultBackgroundColor;

        public Color FlashBackgroundColorEmpty;
        public float EmptyValue = 0f;
        public float FullValue = 1f;

        public float ColorChangeSharpness = 5f;

        float m_PreviousValue;

        public void Initialize(float fullValueRatio, float emptyValueRatio)
        {
            FullValue = fullValueRatio;
            EmptyValue = emptyValueRatio;

            m_PreviousValue = fullValueRatio;
        }

        public void UpdateVisual(float currentRatio)
        {
            if (currentRatio == FullValue && currentRatio != m_PreviousValue)
            {
                ForegroundImage.color = FlashForegroundColorFull;
            }
            else if (currentRatio < EmptyValue)
            {
                BackgroundImage.color = FlashBackgroundColorEmpty;
            }
            else
            {
                ForegroundImage.color = Color.Lerp(ForegroundImage.color, DefaultForegroundColor,
                    Time.deltaTime * ColorChangeSharpness);
                BackgroundImage.color = Color.Lerp(BackgroundImage.color, DefaultBackgroundColor,
                    Time.deltaTime * ColorChangeSharpness);
            }

            m_PreviousValue = currentRatio;
        }
    }
}