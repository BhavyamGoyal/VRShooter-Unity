using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace HCFramework.SaveSystem
{
    public class BinarySave : ISave
    {
        private Dictionary<string, object> saveData = new Dictionary<string, object>();

        GameData gameData = new GameData();


        public BinarySave(Action<Action> callback)
        {
            LoadBinaryData();
            callback(CreateBinaryData);
        }

        //~BinarySave()
        //{
        //    DebugMsg("Destructor Called");
        //    CreateBinaryData();
        //}

        public T LoadData<T>(string key)
        {
            DebugMsg(key + ":" + (T)saveData[key]);
            return (T)saveData[key];
        }

        public bool HasKey<T>(string key)
        {
            if (saveData.ContainsKey(key))
            {
                DebugMsg(key + ": Key Found");
                return true;
            }

            DebugMsg(key + ": No Key Found");
            return false;
        }

        public void SaveData<T>(string key, T value)
        {
            if (saveData.ContainsKey(key))
            {
                saveData[key] = value;
            }
            else
            {
                saveData.Add(key, value);
            }
        }

        private void CreateBinaryData()
        {
            FileStream file = File.Create(Application.persistentDataPath + "/gamedata.data");

            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                gameData.saveData = saveData;
                bf.Serialize(file, gameData);
                DebugMsg("GameData Saved");
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
        }

        private void LoadBinaryData()
        {
            if (File.Exists(Application.persistentDataPath + "/gamedata.data"))
            {
                FileStream file = null;
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    file = File.Open(Application.persistentDataPath + "/gamedata.data", FileMode.Open);
                    gameData = (GameData)bf.Deserialize(file);
                    saveData = gameData.saveData;
                    DebugMsg("GameData Loaded");
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    if (file != null)
                    {
                        file.Close();
                    }
                }
            }
            else
            {
                DebugMsg("No GameData Loaded");
            }
        }


        private void DebugMsg(string msg)
        {
            Debug.Log("<color=green>[BinarySave] </color>" + msg);
        }

    }


    [System.Serializable]
    class GameData
    {
        public Dictionary<string, object> saveData = new Dictionary<string, object>();

        public GameData()
        {
            saveData = new Dictionary<string, object>();
        }
    }
}
