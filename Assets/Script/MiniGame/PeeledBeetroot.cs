using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PeeledBeetroot : MonoBehaviour
{
    [SerializeField] private List<GameObject> _soil;
    [SerializeField] private Transform _digPos;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator _animator;
    [SerializeField] public bool isDigComplete;

    private void Start()
    {
        _animator = _sprite.GetComponent<Animator>();
        isDigComplete = false;




    }
    public void DigActionInvoke()
    {
        RemoveSoil();
    }
    public Vector3 GetDigPos()
    {
        return _digPos.position;
    }
    private void BounceEffect()
    {
        _animator.Play("Bounce", 0, 0f);



    }
    private void RemoveSoil()
    {
        for (int i = 0; i < _soil.Count; i++)
        {
            if (_soil[i].activeSelf == true)
            {
                _soil[i].SetActive(false);
                BounceEffect();
                if (i == _soil.Count - 1)
                {
                    isDigComplete = true;
                    gameObject.layer = LayerMask.NameToLayer("Interactable");
                }
                return;
            }

        }
    }




}
