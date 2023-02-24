using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour
{
    [SerializeField] TMP_Text MessageContent;
    [SerializeField] Image MessageBorder;

    float maxWidth = 400f;

    [SerializeField] TMP_Text textComponent;
    [SerializeField] ContentSizeFitter contentSizeFitter;
    [SerializeField] RectTransform TextrectTransform;
    [SerializeField] RectTransform ImagerectTransform;
    [SerializeField] RectTransform MainrectTransform;

    private void Update()
    {
            if (textComponent.preferredWidth > maxWidth)
            {
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                TextrectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
                SetUpBackgroundBorder(maxWidth, TextrectTransform.rect.height);
            }
            else
            {
                contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                SetUpBackgroundBorder(TextrectTransform.rect.width, TextrectTransform.rect.height);
            }    
    }

    


    public void SetUpBackgroundBorder(float X, float Y)
    {
        ImagerectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, X);
        ImagerectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Y);
        MainrectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Y + 60f);
    }


}
