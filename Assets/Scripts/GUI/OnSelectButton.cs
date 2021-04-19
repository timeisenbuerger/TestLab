using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnSelectButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Button[] buttons;

    public void OnSelect(BaseEventData eventData)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (eventData.selectedObject == buttons[i].gameObject)
            {
                buttons[i].onClick.Invoke();
                break;
            }
        }
    }
}