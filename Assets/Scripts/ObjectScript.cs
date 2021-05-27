using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnBecameInvisible()
    {
        gameObject.transform.root.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (gameObject.CompareTag("collectable"))
        {
            player.GetComponent<PlayerController>().collectItem();
            gameObject.transform.root.gameObject.SetActive(false);
        }
    }
}
