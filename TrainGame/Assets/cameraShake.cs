using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    Vector3 originalPos;

    public bool shake = true;
    public float minimumWaitTime = 0.2f;
    [SerializeField, Range(0f,1f)] float smallShakeChance = 0.15f;
    [SerializeField, Range(0f, 1f)] float bigShakeChance = 0.5f;

    private void Awake()
    {
        originalPos = transform.localPosition;
        StartCoroutine(PeriodicShake());
    }
    IEnumerator shakeCamera(float duration, float magnitude)
    {
        float elapsed = 0;
        float t = 0;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            t = Mathf.Clamp01(duration / elapsed);
            transform.localPosition = new Vector3(Random.Range(-magnitude, magnitude), Random.Range(originalPos.y, originalPos.y + magnitude), originalPos.z);
            yield return null; 
        }

        transform.localPosition = originalPos;
    }

    IEnumerator PeriodicShake()
    {
        float waitTime = 0;
        while (shake)
        {
            waitTime = 0;
            if (Random.Range(0f, 1f) <= smallShakeChance)
            {
                Debug.Log("Small shake");
                StartCoroutine(shakeCamera(0.3f, 0.02f));
                waitTime = 3;
            }

            if (Random.Range(0f, 1f) <= bigShakeChance)
            {
                Debug.Log("Big shake!");
                StartCoroutine(shakeCamera(0.1f, 0.07f));
                waitTime = 5;
            }
            yield return new WaitForSeconds(minimumWaitTime + waitTime);
        }
        while (!shake)
        {
            yield return null;
        }

    }
}
