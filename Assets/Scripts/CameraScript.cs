using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;
    public static CameraScript cameraInstance;
    public float duration = 2f;
    public float shakeMagnitude = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
    }

    public IEnumerator CameraShake()
    {
        Vector3 originalPos = transform.position;
        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPos.x + x, originalPos.y+y, originalPos.z);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        transform.position = originalPos;

    }
}
