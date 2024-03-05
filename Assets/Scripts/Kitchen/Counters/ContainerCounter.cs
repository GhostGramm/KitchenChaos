using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public Action OnContainerCounterInteract;
    public override void Interact(IKitchenObjectParent objectParent)
    {
        OnContainerCounterInteract?.Invoke();

        GameObject kitchenObjGB = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kObj = kitchenObjGB.GetComponent<KitchenObject>();
        kObj.SetKitchenObjectParent(objectParent);
    }
}
