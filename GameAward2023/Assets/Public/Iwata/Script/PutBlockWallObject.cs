using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBlockWallObject : MonoBehaviour
{
    //���ׂ�v���n�u
    public GameObject BlockWallObject;
    //���ׂ鐔
    public Vector3Int Size = Vector3Int.one;
    //BreakForce�̐ݒ萔�l
    public float Endure = 10;

    // �������ꂽ�I�u�W�F�N�g��ێ�����z��
    private GameObject[,,] blocks;


    public void Export()
    {
        //�u���b�N�̐�������ێ����邽�߂̕ϐ���������
        blocks = new GameObject[Size.x + 1, Size.y + 1, Size.z + 1];
        //�I�u�W�F�N�g��z�u����Pos��������
        Vector3 pos = this.transform.position;

        //�q�I�u�W�F�N�g�����ɂ���΂��ׂč폜����
        while (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject);
        }

        //�������Ĕz�u���Ă���
        for (int z = 0; z < Size.z; z++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    //����
                    blocks[x, y, z] = Instantiate(BlockWallObject, pos, Quaternion.identity);
                    //�e�̐ݒ�
                    blocks[x, y, z].transform.parent = this.transform;
                    pos.x += BlockWallObject.transform.localScale.x;
                }
                pos.x = this.transform.position.x;
                pos.y += BlockWallObject.transform.localScale.y;
            }
            pos.y = this.transform.position.y;
            pos.z += BlockWallObject.transform.localScale.z;
        }

        //ConnectedBody�̐ݒ�
        FixedJoint[] hoge = new FixedJoint[3];
        for (int z = 0; z < Size.z; z++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    //FixedJoint�̒ǉ�
                    hoge[0] = blocks[x, y, z].AddComponent<FixedJoint>();
                    hoge[1] = blocks[x, y, z].AddComponent<FixedJoint>();
                    hoge[2] = blocks[x, y, z].AddComponent<FixedJoint>();

                    for(int i = 0; i < 3; i++)
                    {
                        hoge[i].connectedBody = null;
                    }

                    //ConnectedBody�̐ݒ�
                    if (blocks[x + 1, y, z] != null)
                        hoge[0].connectedBody = blocks[x + 1, y, z].GetComponent<Rigidbody>();
                    if (blocks[x, y + 1, z] != null)
                        hoge[1].connectedBody = blocks[x, y + 1, z].GetComponent<Rigidbody>();
                    if (blocks[x, y, z + 1] != null)
                        hoge[2].connectedBody = blocks[x, y, z + 1].GetComponent<Rigidbody>();

                    //breakForce�̐ݒ�
                    hoge[0].breakForce = hoge[1].breakForce = hoge[2].breakForce = Endure;
                    hoge[0].breakTorque = hoge[1].breakTorque = hoge[2].breakTorque = Endure;

                    //�s�v��FixedJoint���폜����
                    for(int i = 0; i < 3; i++)
                    {
                        if(hoge[i].connectedBody == null)
                        {
                            DestroyImmediate(hoge[i]);
                        }
                    }
                }
            }
        }
    }
}
