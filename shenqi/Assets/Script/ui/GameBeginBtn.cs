﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using System;
using usermanage;
public class GameBeginBtn : MonoBehaviour {
    // Use this for initialization
    UserManage userdata;

    void Start () {
        userdata = new UserManage();
        initStartBtns();
    }
    //----------------初始化开始界面
    void initStartBtns() {
        Transform  begin = transform.Find("bgein");
        Transform  zc = transform.Find("zc");

        UIEventListener.Get(begin.gameObject).onClick = BtnStart_dl;
        UIEventListener.Get(zc.gameObject).onClick = BtnStart_zc;
    }
    //登录按钮
    void BtnStart_dl(GameObject obj) {
        UILabel zh = GameObject.Find("zh").GetComponent<UILabel>();
        UILabel mm = GameObject.Find("mima").GetComponent<UILabel>(); 
        string on_off = userdata.GetLogin(zh.text, mm.text);
        switch (on_off) {
            case "1":
                GameModel_role role = new GameModel_role();
                Games.LoadLevel("Game");
                break;
            case "2":
                Games.LoadLevel("Selectrole");
                break;
            default:
                break;
        }
    }
    //注册按钮
    void BtnStart_zc(GameObject obj){
        initZClayer();
        Debug.Log("注册界面");//待定
    }

    //----------------初始化注册界面
    void initZClayer() {
        GameObject obj23 = (GameObject)Resources.Load("Prefab/zhuce");
        Vector2 size = new Vector2(1f, 1f);
        GameObject obj2 = Instantiate(obj23);
        int count3 = obj2.transform.GetChildCount();
        for (int i = 1; i < count3; i++)
        {
            obj2.transform.GetChild(i).transform.localScale = obj2.transform.localScale;
        }
        initZCBtns();
    }
    //初始化注册按钮
    void initZCBtns()
    {
        GameObject btn = GameObject.Find("obj/Button");
        if (btn)
        {
            UIEventListener.Get(btn).onClick = BtnZC_zc;
        }
    }
    //注册
    void BtnZC_zc(GameObject obj)
    {
        UILabel name = GameObject.Find("obj/shurukuang/namobj/name").GetComponent<UILabel>();
        UILabel zh = GameObject.Find("obj/shurukuang/zh").GetComponent<UILabel>();
        UILabel mm = GameObject.Find("obj/shurukuang/mima").GetComponent<UILabel>();
        string state = userdata.GetZhuce(name.text,zh.text,mm.text);
        switch (state) {
            case "1":
                GameObject getzclayer = obj.transform.parent.gameObject;
                if (getzclayer) {
                    Destroy(getzclayer);
                }
                break;
            case "2":

                break;
            case "3":

                break;
            case "4":

                break;
            case "5":

                break;
        }
    }
}