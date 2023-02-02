using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Scene current;

    // Start is called before the first frame update
    void Start()
    {
        current = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (current.name == "SampleScene")
        //    {
        //        SceneManager.LoadScene("Scene2");
        //    }
        //    else if (current.name == "Scene2")
        //    {
        //        SceneManager.LoadScene("SampleScene");
        //    }
        //}
    }
}
