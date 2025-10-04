using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]

public abstract class IDraggable : MonoBehaviour
{
    public Vector3 _defaultPosition { set; get; }
    public virtual void OnDragStart() { }
    public virtual void OnDrag(Vector3 position)
    {
        gameObject.transform.position = position;
    }
    public virtual void OnDragEnd(IDropStorage storage)
    {

        storage.OnDrop(this);

    }
    public virtual void OnDragCancel()
    {
        gameObject.transform.DOMove(_defaultPosition, 0.5f);

    }
    public virtual void SetDefaultPos(Vector3 position)
    {
        _defaultPosition = position;
    }
    public virtual Transform GetTransform()
    {
        return gameObject.transform;
    }
    public virtual GameObject GetGameObject()
    {
        return gameObject;
    }
    public virtual void Start()
    {
        _defaultPosition = gameObject.transform.position;
        gameObject.layer = LayerMask.NameToLayer("Draggable");

    }
    public virtual void OnEnable()
    {


    }
    public virtual void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Draggable");

    }


}
