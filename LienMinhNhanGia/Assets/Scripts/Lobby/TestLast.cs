using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLast : MonoBehaviour
{
    public Scrollbar verticalScrollbar;

    public RectTransform TextUI;

    public Transform content;
    public GameObject MessageItem;

    private void Start()
    {
        // Add a listener to the scrollbar's onValueChanged event
        verticalScrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);

    }

    private void OnScrollbarValueChanged(float value)
    {
        // Check if the scrollbar is at the bottom
        if (value == 1f)
        {
            verticalScrollbar.value = value;
            Debug.Log("Reached the bottom of the scroll view");
        }
        else
        {
            verticalScrollbar.value = value;
            Debug.Log("Reached the top of the scroll view");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(MessageItem, content);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //OnScrollbarValueChanged(1.0f);
        }
    }
}
