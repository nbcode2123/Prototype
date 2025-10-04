using DG.Tweening;
using UnityEngine;

public class CutPieceAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        float y = gameObject.transform.position.y;
        transform.DOMoveY(y + 0.1f, 0.1f)
         .SetLoops(2, LoopType.Yoyo);
        //  .SetEase(Ease.OutBack);
    }
}
