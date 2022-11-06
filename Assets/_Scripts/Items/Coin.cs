using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    void Start()
    {
        Vector3 startScale = transform.localScale;
        transform.localScale = Vector3.one * 0.001f;
        transform.DOScale(startScale, 0.2f).SetEase(Ease.OutSine);
    }

    void Update()
    {
        if(transform.localScale.magnitude <= 0)
            Destroy(gameObject);

        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);

        if(transform.position.y < -6)
            Destroy(gameObject);
    }

    public void Remove()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.OutSine);
    }
}
