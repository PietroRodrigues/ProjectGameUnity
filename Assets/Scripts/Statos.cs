using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statos
{
    int _life;
    float _speed;

    public Statos(int life, float speed)
    {
        _life = life;
        _speed = speed;    

    }

    public int Life { get => _life; set => _life = value; }
    public float Speed { get => _speed; set => _speed = value; }
}
