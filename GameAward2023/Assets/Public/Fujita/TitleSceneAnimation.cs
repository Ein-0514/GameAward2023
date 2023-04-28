using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneAnimation : MonoBehaviour
{
    public Animator animator;
    public float animationTime = 2f; //�A�j���[�V�������Đ����鎞��
    private bool isPlaying = false; //�A�j���[�V�������Đ��������肷��t���O

    void Update()
    {
        
    }

    void StartAnimetion()
    {
        if (Input.GetButtonDown("A") && !isPlaying) //�������u�͂��߂���vor�u�Â�����v�������ꂽ��ɕύX����
        {
            animator.Play("AnimationName"); //�A�j���[�V�������Đ�����
            isPlaying = true;
            Invoke("ResetIsPlaying", animationTime); //�A�j���[�V�������I��������t���O�����Z�b�g����
        }
    }

    //�A�j���[�V�������I��������t���O�����Z�b�g����
    void ResetIsPlaying()
    {
        isPlaying = false;
    }
}







