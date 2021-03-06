﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using CG_Public;
namespace CG_Manage
{
    public class User_Manage : UserData, interface_User
    {
        private static User_Manage _instance = null;
        private User_Manage() { }
        public static User_Manage CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new User_Manage();
            }
            return _instance;
        }


        private static Dictionary<string, string> getuserinfo = new Dictionary<string, string>();
        /// <summary>
        /// 通过   User_Manage.GetUserInfo[""] 获取用户基本属性 
        /// </summary>
        public static Dictionary<string, string> GetUserInfo
        {
            get
            {
                return getuserinfo;
            }
        }

        //实例化 接口方法
        private string GetKey_hp = SetUserInfoKey.HP.ToString();
        /// <summary>
        /// 获取用户key  血量
        /// </summary>
        public string GetKey_HP
        {
            get
            {
                return GetKey_hp;
            }
        }

        private string GetKey_mp = SetUserInfoKey.MP.ToString();
        /// <summary>
        /// 获取用户key  蓝量
        /// </summary>
        public string GetKey_MP
        {
            get
            {
                return GetKey_mp;
            }
        }
        private string GetKey_attack = SetUserInfoKey.Attack.ToString();
        /// <summary>
        /// 获取用户key  攻击力
        /// </summary>
        public string GetKey_Attack
        {
            get
            {
                return GetKey_attack;
            }
        }
        private string GetKey_magic = SetUserInfoKey.Magic.ToString();//魔法
        /// <summary>
        /// 获取用户key  魔法
        /// </summary>
        public string GetKey_Magic
        {
            get
            {
                return GetKey_magic;
            }
        }

        private string GetKey_role = SetUserInfoKey.Role.ToString();//角色  0 为没有角色
        /// <summary>
        /// 获取用户key  角色  0 为没有角色
        /// </summary>
        public string GetKey_Role
        {
            get
            {
                return GetKey_role;
            }
        }
        private string GetKey_sex = SetUserInfoKey.Sex.ToString();// 1为男    2为女
        /// <summary>
        /// 获取用户key  性别
        /// </summary>
        public string GetKey_Sex
        {
            get
            {
                return GetKey_sex;
            }
        }
        private string GetKey_armor = SetUserInfoKey.Armor.ToString();//护甲
        /// <summary>
        /// 获取用户key  护甲
        /// </summary>
        public string GetKey_Armor
        {
            get
            {
                return GetKey_armor;
            }
        }
        private string GetKey_name = SetUserInfoKey.Name.ToString();
        /// <summary>
        /// 获取用户key  名字
        /// </summary>
        public string GetKey_Name
        {
            get
            {
                return GetKey_name;
            }
        }
        private string GetKey_mac = SetUserInfoKey.MAC.ToString();//魔法防御
        /// <summary>
        /// 获取用户key  魔法防御
        /// </summary>
        public string GetKey_MAC
        {
            get
            {
                return GetKey_mac;
            }
        }
        private string GetKey_model = SetUserInfoKey.Model.ToString();//角色模型
        /// <summary>
        /// 获取用户key  角色模型
        /// </summary>
        public string GetKey_Model
        {
            get
            {
                return GetKey_model;
            }
        }
        private string GetKey_id = SetUserKey.id.ToString();//角色ID
        /// <summary>
        /// 获取用户key  角色ID
        /// </summary>
        public string GetKey_ID
        {
            get
            {
                return GetKey_id;
            }
        }

        private string[] GetModelkeys = {
            SetUserInfoKey.HP.ToString(),
            SetUserInfoKey.MP.ToString(),
            SetUserInfoKey.Attack.ToString(),
            SetUserInfoKey.Magic.ToString(),
            SetUserInfoKey.Armor.ToString(),
            SetUserInfoKey.MAC.ToString()
        };
        /// <summary>
        /// 获取所有keys   
        /// </summary>
        public string[] GetModelKeys {
            get
            {
                return GetModelkeys;
            }
        }
        /*
          注册：
          name:名字
          account：账号
          password：密码
        */
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string GetZhuce(string name, string account, string password)
        {
            string filepath = Application.dataPath + @"/Resources/UserData.xml";
            string on_off = "1";
            Debug.Log(CG_Windows.Format((string)CG_Config.LABEL["ZC"],name, account, password));
            if (account == "")
            {
                Debug.LogError(CG_Config.LABEL["ZHYW"]);
                on_off = "2";
            }
            else if (password == "")
            {
                Debug.LogError(CG_Config.LABEL["MMYW"]);
                on_off = "3";
            }
            else if (name == "")
            {
                Debug.LogError(CG_Config.LABEL["MZYW"]);
                on_off = "4";
            }
            if (on_off == "1")
            {
                if (!File.Exists(filepath))
                {
                    createXml(name, account, password);
                    Debug.Log(CG_Config.LABEL["ZCCG"]);
                }
                else if (File.Exists(filepath))
                {
                    on_off = AddXml(name, account, password);
                    if (on_off == "5")
                    {
                        Debug.LogError(CG_Config.LABEL["ZHCZ"]);
                    }
                }
                else if (on_off == "1")
                {
                    Debug.Log(CG_Config.LABEL["ZCCG"]);
                }
            }
            return on_off;
        }
        /*
         登录：
         account：账号
         password：密码
        */
        /// <summary>
        /// 获取登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public string GetLogin(string account, string password)
        {
            string value = "";
            bool on_off = login(account, password);
            if (on_off)
            {
                getuserinfo = Getinfos(account);
                if (getuserinfo["Role"] !="0")
                {
                    Debug.Log(CG_Config.LABEL["DLCG"]);
                    value = "1";
                }
                else {
                    Debug.Log(CG_Config.LABEL["MYJS"]);
                    value = "2";
                }
            }
            else {
                Debug.LogError(CG_Windows.Format((string)CG_Config.LABEL["DLSB"], account, password));
                Debug.LogError(CG_Config.LABEL["ZHMMYW"]);
                value = "0";
            }
            return value;
        }

        /*
        获取用户所有属性：
        account：用户账户
        */
        /// <summary>
        /// 获取用户所有属性
        /// </summary>
        /// <param name="account">用户账户</param>
        /// <returns></returns>
        public Dictionary<string, string> Getinfos(string account)
        {
            Dictionary<string, string> Object = new Dictionary<string, string>();
            Object = FindAllInfo(account);
            Debug.Log(CG_Windows.Format((string)CG_Config.LABEL["BROKEN"],(string)CG_Config.LABEL["Begin"]));
            foreach (KeyValuePair<string, string> index in Object)
            {
                Debug.Log(CG_Windows.Format((string)CG_Config.LABEL["YHSX"], index.Key, index.Value));
            }
            Debug.Log(CG_Windows.Format((string)CG_Config.LABEL["BROKEN"], (string)CG_Config.LABEL["END"]));
            return Object;
        }

        /*
        V值是否相同
        */
        /// <summary>
        ///  V值是否相同
        /// </summary>
        public bool isSame(string key, string Value)
        {
            bool on_off = FindSame(key,Value);
            return on_off;
        }

        /*
        更改及更新用户属性V值：
        id：用户ID
        key：K值属性
        Value：V值属性
        */
        /// <summary>
        ///  更改及更新用户属性V值：
        /// </summary>
        public void SetInfo(string id, string key, string Value)
        {
            bool on_off = UpdateInfoXml(id, key, Value);
            if (on_off) {
                getuserinfo[key] = Value;
                Debug.Log(CG_Windows.Format((string)CG_Config.LABEL["GXCG"], key, getuserinfo[key]));
                return;
            }
            Debug.LogError(CG_Windows.Format((string)CG_Config.LABEL["GXSB"], id, key));
        }
    }
}
