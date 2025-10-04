using System;
using DG.Tweening;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    [SerializeField] private GameObject _soil;
    [SerializeField] private Vector3 _digPos;
    [SerializeField] private Transform _posDefault;
    [SerializeField] private bool _isDig;
    public void ResetPosition()
    {
        if (_isDig == false)
        {
            transform.position = _posDefault.position;
        }


    }
    private void Start()
    {
        _soil.SetActive(false);
        gameObject.transform.position = _posDefault.position;
    }


    private void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        var beetroot = other.GetComponent<PeeledBeetroot>();
        if (beetroot != null && beetroot.isDigComplete == false)
        {
            _digPos = beetroot.GetDigPos();
            gameObject.transform.position = _digPos;
            StartDigAnimation(beetroot.DigActionInvoke);
        }
    }

    private void StartDigAnimation(Action callback)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_digPos, 0.5f));
        seq.AppendCallback(() => _soil.SetActive(true));
        seq.AppendInterval(0.3f);
        seq.AppendCallback(() => _soil.SetActive(false));
        seq.Append(transform.DORotate(new Vector3(0f, 0f, -90f), 0.5f)).AppendCallback(() => callback?.Invoke()).Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f));
        seq.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 0.5f));
        seq.Append(transform.DOMove(_posDefault.position, 0.5f));
        seq.OnComplete(() =>
        {
            gameObject.GetComponent<Collider2D>().enabled = true;

        });
    }


}
