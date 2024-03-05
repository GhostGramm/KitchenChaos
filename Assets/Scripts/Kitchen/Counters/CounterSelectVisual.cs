using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSelectVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] selectedVisual;

    private void Start()
    {
        Player.Instance.OnSetSelectedCounter += ToggleSelectedState;
    }

    private void ToggleSelectedState(BaseCounter _counter)
    {
        if (baseCounter != _counter)
            Hide();
        else
            Show();
    }

    private void Show()
    {
        foreach (GameObject item in selectedVisual)
        {
            item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject item in selectedVisual)
        {
            item.SetActive(false);
        }
    }
}
