using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private float resetThreshold = 1000;
    GameObject player;
    public static GameManager gameManagerInstance;
    public bool gameActive = true;
    public Text gameOverText;
    public Button restartButton;

    private void Awake()
    {
        gameManagerInstance = this;
    }// Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.transform.position.z) > resetThreshold)
        {
            transform.position = new Vector3(0, 0, player.transform.position.z);
            ResetWorldPosition();
        }
    }

    //Parents all objects exepect player under this one, moves back to world 0 and unparents objects back
    private void ResetWorldPosition()
    {
        GameObject[] objectsInScene = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var objectInScene in objectsInScene)
        {
            if (objectInScene.gameObject.CompareTag("Player")) continue;

            objectInScene.gameObject.transform.SetParent(transform);
        }

        // Reset player position
        transform.position = Vector3.zero;
        player.transform.position = new Vector3(player.transform.localPosition.x, player.transform.localPosition.y, 0);

        foreach (var objectInScene in objectsInScene)
        {
            if (objectInScene.gameObject.CompareTag("Player")) continue;
            objectInScene.gameObject.transform.SetParent(null);
        }
    }

    public void gameOver()
    {
        gameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        StartCoroutine(Camera.main.GetComponent<CameraScript>().CameraShake());
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
