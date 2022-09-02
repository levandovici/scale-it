using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField]
    private AudioSource _source;



    private void Awake()
    {
        DontDestroyOnLoad(_source);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
