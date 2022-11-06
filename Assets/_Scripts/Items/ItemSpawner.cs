using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bomb;
    [SerializeField] private GameObject coin;

    void Start()
    {
        StartCoroutine(SpawnItem(5));
    }

    IEnumerator SpawnItem(float time)
    {
        if(time < 0.2f)
            time = 0.2f;

        int rand = Random.Range(0,2);
        Vector3 position = new Vector3(Random.Range(-7f, 7f), Random.Range(-2f, 5f),-0.4f);
        Instantiate(rand == 1 ? coin : bomb, position, rand == 1 ? coin.transform.rotation : Random.rotation);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnItem(time * 0.95f));
    }

}
