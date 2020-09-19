using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// 文件：Controller.cs
/// 功能：表现实体控制器抽象基类
/// </summary>
public abstract class Controller : MonoBehaviour
{
    public Animator ani;
    public CharacterController ctrl;

    protected bool isMove = false;
    protected Vector2 dir = Vector2.zero;
    public Vector2 Dir
    {
        get
        {
            return dir;
        }

        set
        {
            if (value == Vector2.zero)
            {
                isMove = false;
            }
            else
            {
                isMove = true;
            }
            dir = value;
        }
    }
    protected Transform camTrans;

    public virtual void SetBlend(float blend)
    {
        ani.SetFloat("Blend", blend);
    }

    public virtual void SetAction(int act)
    {
        ani.SetInteger("Action", act);
    } 
}