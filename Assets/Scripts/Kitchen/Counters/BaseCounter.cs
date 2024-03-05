using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] public KitchenObjectSO kitchenObjectSO;
    [SerializeField] public Transform counterTopPoint;

    [HideInInspector]public KitchenObject kitchenObject;

    public virtual void Interact(IKitchenObjectParent objectParent)
    {
        if (kitchenObject == null)
        {
            GameObject kitchenObjGB = Instantiate(kitchenObjectSO.prefab);
            KitchenObject kObj = kitchenObjGB.GetComponent<KitchenObject>();

            kObj.SetKitchenObjectParent(this);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(objectParent);
        }
    }

    public Transform GetKitchenObjectFollowTransform() => counterTopPoint;

    public void ClearKitchenObject() => kitchenObject = null;

    public KitchenObject GetKitchenObject() => kitchenObject;

    public void SetKitchenObject(KitchenObject _kitchenObject) => kitchenObject = _kitchenObject;

    public bool IsKitchenObjectAvailable() => kitchenObject != null;
}
