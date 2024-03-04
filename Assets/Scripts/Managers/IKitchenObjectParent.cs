using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();

    public void ClearKitchenObject();

    public KitchenObject GetKitchenObject();

    public void SetKitchenObject(KitchenObject _kitchenObject);

    public bool IsKitchenObjectAvailable();
}
