using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class GasStove : MonoBehaviour, IDropStorage
{
    public ClayPot _clayPot;

    public GameObject _pivotSurface;
    public bool _isOn;
    public GameObject _fire;



    public void EmptySlot()
    {
    }
    public void TurnOnGas()
    {
        _isOn = !_isOn;
        if (_clayPot != null)
        {
            _clayPot.isGasTurnOn = _isOn;
        }
        if (_isOn == true)
        {
            _fire.SetActive(true);

        }
        else
        {
            _fire.SetActive(false);
        }

    }

    public void OnDrop(IDraggable draggable)
    {
        if (draggable is ClayPot clayPot)
        {
            if (_clayPot == null)
            {
                ObserverManager.Notify(ObserverEvent.ChangeDropStorage.ToString(), clayPot);

                _clayPot = clayPot;
                _clayPot.transform.DOMove(_pivotSurface.transform.position, 0.2f);
                _clayPot.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
                clayPot.SetDefaultPos(_pivotSurface.transform.position);
                clayPot.isInGasStove = true;
                if (clayPot.isHaveWater == true)
                {
                    clayPot.gameObject.layer = LayerMask.NameToLayer("DropStorage");
                }
                gameObject.layer = LayerMask.NameToLayer("Default");
            }

        }
        else { draggable.OnDragCancel(); }
    }

    public void Start()
    {
        _clayPot.gameObject.transform.position = _pivotSurface.transform.position;
        _clayPot.SetDefaultPos(_pivotSurface.transform.position);
        ObserverManager.AddListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);
    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);

    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);

    }
    public void CheckChangeDrag(ClayPot clayPot)
    {
        if (_clayPot != null && _clayPot == clayPot)
        {
            _clayPot = null;
            gameObject.layer = LayerMask.NameToLayer("DropStorage");


        }

    }
}
