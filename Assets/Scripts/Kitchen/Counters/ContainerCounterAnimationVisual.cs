using UnityEngine;

public class ContainerCounterAnimationVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator _animator;

    public const string OPEN_ANIMATION_TRIGGER = "CounterOpenTG";

    public void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnContainerCounterInteract += ContainerCounterInteractEvent;
    }

    public void ContainerCounterInteractEvent()
    {
        _animator.SetTrigger(OPEN_ANIMATION_TRIGGER);
    }
}