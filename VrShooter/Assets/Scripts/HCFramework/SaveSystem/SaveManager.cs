using System;
using UnityEngine;
using HCFramework.SaveSystem;

namespace HCFramework
{
    public class SaveManager : Singleton<SaveManager>
    {
        public static SaveManager instance;

        private ISave binarySave, playerPrefSave;
        private Action createBinaryCallback;

        public SaveManager()
        {
            binarySave = new BinarySave(CreateBinaryCallback);
            playerPrefSave = new PlayerPrefSave();
        }

        public void SaveData<T>(string key, T value, SaveType saveType)
        {
            if (saveType == SaveType.Binary)
            {
                binarySave.SaveData<T>(key, value);
            }
            else if (saveType == SaveType.PlayerPref)
            {
                playerPrefSave.SaveData<T>(key, value);
            }
        }

        public T LoadData<T>(string key, SaveType saveType)
        {
            System.Object value = null;

            if (saveType == SaveType.Binary)
            {
                if (binarySave.HasKey<T>(key))
                {
                    value = binarySave.LoadData<T>(key);
                }
                else
                {
                    return default(T);
                }
            }
            else if (saveType == SaveType.PlayerPref)
            {
                if (playerPrefSave.HasKey<T>(key))
                {
                    value = playerPrefSave.LoadData<T>(key);
                }
                else
                {
                    return default(T);
                }
            }

            return (T)Convert.ChangeType(value, typeof(T));
        }


        public void CreateBinaryData()
        {
            createBinaryCallback(); 
        }

        private void CreateBinaryCallback(Action createBinaryData)
        {
            createBinaryCallback = createBinaryData;
        }
    }

    public enum SaveType { PlayerPref, Binary }
}