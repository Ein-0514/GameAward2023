using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jettsize : MonoBehaviour
{
     ParticleSystem particleSystem;
     float lifeTime = 1.0f;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var main = particleSystem.main;
        main.startLifetime = lifeTime;//�W�F�b�g�������ĂȂ��Ȃ鑬��
    }
    public float LifeTime
    {
        set { lifeTime = value; }
    }
}
