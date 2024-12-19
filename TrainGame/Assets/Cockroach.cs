using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    Bounds bounds;
    void Start()
    {
        bounds.center = transform.parent.position;
        bounds.size = transform.parent.GetComponent<BoxCollider>().size;
        StartCoroutine(roachBrain());
    }

    private void Update()
    {
        if (!bounds.Contains(transform.position))
        {
            transform.position = bounds.max * Random.Range(0f,1f);
        }
    }

    IEnumerator roachBrain()
    {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.3f, 3f));
            yield return moveRandom();
        }
    }

    IEnumerator moveRandom()
    {
        float duration = Random.Range(0.5f, 2);
        int move = Random.Range(0, 3);
        float elapsed = 0;

        switch (move)
        {
            case 0:
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward * Time.deltaTime;
                    yield return null;
                }
                break;
            case 1:
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward * Time.deltaTime;
                    transform.Rotate(0, 200 * Time.deltaTime, 0);
                    yield return null;
                }
                break;
            case 2:
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward  * Time.deltaTime;
                    transform.Rotate(0, -200 * Time.deltaTime, 0);
                    yield return null;
                }
                break;
            case 3:
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    yield return null;
                }
                break;

        }
    }
}
