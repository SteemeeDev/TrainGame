using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cockroach : MonoBehaviour
{
    Bounds bounds;
    void Start()
    {
        bounds = transform.parent.GetComponent<BoxCollider>().bounds;
        StartCoroutine(roachBrain());
    }

    private void Update()
    {
        if (!bounds.Contains(transform.position))
        {
            transform.position = new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
            Debug.Log(transform.position);
        }
    }

    IEnumerator roachBrain()
    {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0f, 3f));
            yield return moveRandom();
        }
    }

    IEnumerator moveRandom()
    {
        float duration = Random.Range(0.2f, 2);
        int move = Random.Range(0, 3);
        float elapsed = 0;

        switch (move)
        {
            case 0:
                duration += 1;
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward * Time.deltaTime;
                    yield return null;
                }
                break;
            case 1:
                duration /= 2f;
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward * Time.deltaTime * 0.5f;
                    transform.Rotate(0, 300 * Time.deltaTime, 0);
                    yield return null;
                }
                break;
            case 2:
                duration /= 2f;
                while (elapsed < duration)
                {
                    elapsed += Time.deltaTime;
                    transform.position += transform.forward  * Time.deltaTime * 0.5f;
                    transform.Rotate(0, -300 * Time.deltaTime, 0);
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
