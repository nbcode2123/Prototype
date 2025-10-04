using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class SkinRemove : IDraggable
{
    public override GameObject GetGameObject()
    {
        return gameObject;
    }

    public override Transform GetTransform()
    {
        return transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TrashCan"))
        {
            gameObject.transform.position = other.gameObject.transform.position;

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
