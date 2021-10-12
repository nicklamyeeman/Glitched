using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LoadLevel()
    {
        SceneManager.LoadScene("Test99");
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Hello");
            LoadLevel();
        }

    }
}
