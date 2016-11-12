﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using System;

public  class UserData : MonoBehaviour
{
    enum SetUserKey
    {
        id,
        name,
        account,
        password
    }

    enum SetUserInfoKey
    {
        HP,
        MP,
        Attack,
        Magic
    }
    //-------------SetUserKey
    public int id = 1;//id
    public string name = "乔彬";//name
    public string account = "adsdasasd";//账号
    public string password = "asd151154";//密码

    //-------------SetUserInfoKey
    public string HP = "24151";//HP
    public string MP = "156454";//MP
    public string Attack = "44545";//攻击
    public string Magic = "151515";//魔法

    public virtual void  createXml(string name, string account, string password)
    {
            string filepath = Application.dataPath + @"/Resources/UserData.xml";
            if (!File.Exists(filepath)){
                XmlDocument xmlDoc = new XmlDocument();
                XmlElement root = xmlDoc.CreateElement("transforms");
                XmlElement elmNew = xmlDoc.CreateElement("rotation");
                elmNew.SetAttribute(SetUserKey.id.ToString(), id.ToString());
                elmNew.SetAttribute(SetUserKey.name.ToString(), name.ToString());
                elmNew.SetAttribute(SetUserKey.account.ToString(), account.ToString());
                elmNew.SetAttribute(SetUserKey.password.ToString(), password.ToString());

                XmlElement rotation_HP = xmlDoc.CreateElement(SetUserInfoKey.HP.ToString());
                rotation_HP.InnerText = HP.ToString();
                XmlElement rotation_MP = xmlDoc.CreateElement(SetUserInfoKey.MP.ToString());
                rotation_MP.InnerText = MP.ToString();
                XmlElement rotation_ATTACK = xmlDoc.CreateElement(SetUserInfoKey.Attack.ToString());
                rotation_ATTACK.InnerText = Attack.ToString();
                XmlElement rotation_MAGIC = xmlDoc.CreateElement(SetUserInfoKey.Magic.ToString());
                rotation_MAGIC.InnerText = Magic.ToString();

                elmNew.AppendChild(rotation_HP);
                elmNew.AppendChild(rotation_MP);
                elmNew.AppendChild(rotation_MAGIC);
                elmNew.AppendChild(rotation_ATTACK);
                root.AppendChild(elmNew);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(filepath);
                Debug.Log("createXml OK!");
            }
    }
    public virtual string AddXml(string name, string account, string password)
    {
        string on_off = "1";
        int Maxid = GetMaxID();
        string filepath = Application.dataPath + @"/Resources/UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);

            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("account") == account)
                {
                    on_off = "5";
                    break;
                }
            }
            if (on_off=="1")
            {
                XmlNode root = xmlDoc.SelectSingleNode("transforms");
                XmlElement elmNew = xmlDoc.CreateElement("rotation");
                elmNew.SetAttribute(SetUserKey.id.ToString(), (Maxid + 1).ToString());
                elmNew.SetAttribute(SetUserKey.name.ToString(), name.ToString());
                elmNew.SetAttribute(SetUserKey.account.ToString(), account.ToString());
                elmNew.SetAttribute(SetUserKey.password.ToString(), password.ToString());

                XmlElement rotation_HP = xmlDoc.CreateElement(SetUserInfoKey.HP.ToString());
                rotation_HP.InnerText = HP.ToString();
                XmlElement rotation_MP = xmlDoc.CreateElement(SetUserInfoKey.MP.ToString());
                rotation_MP.InnerText = MP.ToString();
                XmlElement rotation_ATTACK = xmlDoc.CreateElement(SetUserInfoKey.Attack.ToString());
                rotation_ATTACK.InnerText = Attack.ToString();
                XmlElement rotation_MAGIC = xmlDoc.CreateElement(SetUserInfoKey.Magic.ToString());
                rotation_MAGIC.InnerText = Magic.ToString();

                elmNew.AppendChild(rotation_HP);
                elmNew.AppendChild(rotation_MP);
                elmNew.AppendChild(rotation_MAGIC);
                elmNew.AppendChild(rotation_ATTACK);
                root.AppendChild(elmNew);
                xmlDoc.AppendChild(root);
                xmlDoc.Save(filepath);
                Debug.Log("AddXml OK!");
            }
        }
        return on_off;
    }

    public void UpdateInfoXml(string id, string key, int Value)
    {
        string filepath = Application.dataPath + @"/Resources/UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;

            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("id") == id)
                {
                    //xe.SetAttribute("id", "1000");
                    //xe.SetAttribute("name", "momo");
                    foreach (XmlElement x1 in xe.ChildNodes)
                    {
                        if (x1.Name == key)
                        {
                            x1.InnerText = Value.ToString();
                        }

                    }
                    break;
                }
            }
            xmlDoc.Save(filepath);
            Debug.Log("UpdateXml OK!");
        }

    }

    public void deleteXml(string id)
    {
        string filepath = Application.dataPath + @"/Resources/UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("id") == id)
                {
                    xe.RemoveAll();
                }
            }
            xmlDoc.Save(filepath);
            Debug.Log("deleteXml OK!");
        }

    }

    public string FindXml(string key, string Type)
    {
        string Value = null;
        string id = this.id.ToString();
        string filepath = Application.dataPath + @"/Resources/UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;

            foreach (XmlElement xe in nodeList)
            {
                if (xe.GetAttribute("id") == id)
                {
                    if (Type == "user")
                    {
                        Debug.Log(key + "：" + xe.GetAttribute(key));
                        Value = xe.GetAttribute(key);
                    }
                    else if (Type == "info")
                    {
                        foreach (XmlElement x1 in xe.ChildNodes)
                        {
                            if (x1.Name == key)
                            {
                                Debug.Log(key + "：" + x1.InnerText);
                                Value = x1.InnerText;
                            }
                        }
                    }

                    break;
                }
            }

        }
        return Value;
    }
    public int GetMaxID() {
        int id = 1;
        int count = 0;
        string filepath = Application.dataPath + @"/Resources/UserData.xml";
        if (File.Exists(filepath))
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList nodeList = xmlDoc.SelectSingleNode("transforms").ChildNodes;
            foreach (XmlElement xe in nodeList)
            {
                if (count == nodeList.Count - 1)
                {
                    id = int.Parse(xe.GetAttribute("id"));
                    break;
                }
                count++;
            }
        }
        return id;
    }
}
