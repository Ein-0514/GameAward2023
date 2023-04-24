using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Playing : MonoBehaviour
{
    [SerializeField] GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(gm.GameStatus == GameManager.eGameStatus.E_GAME_STATUS_PLAY)
        {
            // �q�I�u�W�F�N�g����Parent�N���X���p�������X�N���v�g���擾����
            JankBase[] scripts = GetComponentsInChildren<JankBase>();

            // �擾�����X�N���v�g��work�֐������s����
            foreach (JankBase script in scripts)
            {
                script.work();
            }
        }

    }
}
