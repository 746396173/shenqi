﻿using UnityEngine;
using System.Collections;
using CG_Manage;
public class Scene_Gamebegin : Scene_Manage {
    string ClassID = "Scene_Gamebegin";
    User_Manage userdata;
    void Start() {
        userdata = User_Manage.CreateInstance();
        initUI();
    }
    //----------------初始化开始界面
    protected override void initUI()
    {
        AddUI(ClassID, this.gameObject);
        initBtns();
    }

    protected override void initBtns()
    {
        string[] arr = { "bgein", "zc" };
        foreach (var obj in arr)
        {
            Transform button = transform.Find(obj);
            UIEventListener.Get(button.gameObject).onClick = Callback;
        }
    }
    protected override void Callback(GameObject Obj)
    {
        switch (Obj.name)
        {
            case "bgein":
                BtnStart_dl(Obj);
                break;
            case "zc":
                BtnStart_zc(Obj);
                break;
        }
    }
    //登录按钮
    void BtnStart_dl(GameObject obj)
    {
        UILabel zh = GameObject.Find("zh").GetComponent<UILabel>();
        UILabel mm = GameObject.Find("mima").GetComponent<UILabel>();
        string on_off = userdata.GetLogin(zh.text, mm.text);

        switch (on_off)
        {
            case "1":
                GameModel_role role = new GameModel_role();
                LoadLevel("Scene_Game");
                break;
            case "2":
                LoadLevel("Scene_Selectrole");
                break;
            default:
                break;
        }
    }
    //注册按钮  初始化注册界面
    void BtnStart_zc(GameObject obj)
    {
        UI_Manage login = new UI_Login();
    }

}
