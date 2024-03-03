using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSelectVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject selectedVisual;

    private void Start()
    {
        Player.Instance.OnSetSelectedCounter += ToggleSelectedState;
    }

    private void ToggleSelectedState(ClearCounter _counter)
    {
        if(clearCounter != _counter)
            Hide();
        else
            Show();
    }

    private void Show() => selectedVisual.SetActive(true);
    private void Hide() => selectedVisual.SetActive(false);
}
