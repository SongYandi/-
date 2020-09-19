using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 文件：PlayerController.cs
/// 功能：角色控制器
/// </summary>
public class PlayerController : Controller 
{
    public Vector3 camOffset;
    

    private float targetBlend;
    private float currentBlend;

    public void Init()
    {
        camTrans = Camera.main.transform;
        camOffset = transform.position - camTrans.position;
    }
    private void Update()
    {
        //#region Input Test
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        //Vector2 _idr = new Vector2(h, v).normalized;
        //if (_idr != Vector2.zero)
        //{
        //    Dir = _idr;
        //    SetBlend(Constants.BlendRun);
        //}
        //else
        //{
        //    SetBlend(Constants.BlendIdle);
        //    Dir = Vector2.zero;
        //}
        //#endregion
        if (currentBlend!=targetBlend)
        {
            UpdateMixBlend();
        }
        if (isMove)
        {
            //控制方向
            SetDir();
            //角色产生移动
            SetMove();
            //相机
            SetCam();
        }
    }

    private void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1))+ camTrans.eulerAngles.y;
        Vector3 eulerAngles = new Vector3(0, angle, 0);
        transform.localEulerAngles = eulerAngles;
    }
    private void SetMove()
    {
        ctrl.Move(transform.forward*Time.deltaTime*Constants.PlayerMoveSpeed);
    }
    private void SetCam()
    {
        if (camTrans!=null)
        {
            camTrans.position = transform.position - camOffset;
        }
    }
    
    private void UpdateMixBlend()
    {
        if (Mathf.Abs(currentBlend-targetBlend)<Constants.AccelerSpeed*Time.deltaTime)
        {
            currentBlend = targetBlend;
        }
        else if (currentBlend>targetBlend)
        {
            currentBlend -= Constants.AccelerSpeed * Time.deltaTime;
        }
        else
        {
            currentBlend += Constants.AccelerSpeed * Time.deltaTime;
        }
        ani.SetFloat("Blend", currentBlend);
    }

    public override void SetBlend(float blend)
    {
        targetBlend = blend;
    }

}
