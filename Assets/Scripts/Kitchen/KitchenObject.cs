using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    public KitchenObjectSO kitchenObject;
    private IKitchenObjectParent kitchenObjectParent;

    public void GetKitchenObject()
    {
        Debug.Log(kitchenObject.objectName);
    }

    public void SetKitchenObjectParent(IKitchenObjectParent _kitchenObjectParent)
    {
        if(this.kitchenObjectParent != null && this.kitchenObjectParent.IsKitchenObjectAvailable()) 
            this.kitchenObjectParent.ClearKitchenObject();

        Debug.Log(_kitchenObjectParent);
        this.kitchenObjectParent = _kitchenObjectParent;
        this.kitchenObjectParent.SetKitchenObject(this);
        transform.parent = this.kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetClearCounter() => kitchenObjectParent;

}