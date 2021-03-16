using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    public string sceneName;
    private Animator fadeSystem;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(LoadNextScene());
        }
    }

    public IEnumerator LoadNextScene()
    {
        LoadAndSaveData.instance.SaveDate();
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
