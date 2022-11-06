using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float moveSpeed;

    [SerializeField] private GameObject explosion;
    [SerializeField] private ShakePreset explosionShake;


    void Start()
    {
        Vector3 endScale = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(endScale, 0.2f).SetEase(Ease.OutSine);
    }

    void Update()
    {
        transform.Rotate(Vector3.one * Time.deltaTime * rotationSpeed);
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.World);

        if(transform.position.y < -6)
            Destroy(gameObject);

    }


    public void Detonate()
    {
        Instantiate(explosion, transform.position, Random.rotation);
        Shaker.ShakeAll(explosionShake);
        Destroy(gameObject);
    }
}
