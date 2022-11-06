using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    float timeSpawned;

    void Awake() => timeSpawned = Time.time;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if(Time.time - timeSpawned > 2)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("DEATH"))
        {
            Destroy(gameObject);
        }
        else if(col.gameObject.TryGetComponent<Bomb>(out Bomb bomb))
        {
            bomb.Detonate();
            GameObject.FindObjectOfType<BananaController>().UpdateScore(30);
            Destroy(gameObject);
        }
    }
}
