using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        Debug.Log("�Ԃ�����" + collision.transform.name);

		if (collision.transform.name.Contains("Core_Child"))
        {
            Debug.Log("�S�[������");
            SceneManager.LoadScene("GameScene_v2.0");
        }

	}
}