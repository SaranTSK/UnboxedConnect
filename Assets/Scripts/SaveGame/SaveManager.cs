using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unboxed.SaveGame;

namespace Unboxed.SaveGame
{
    public static class SaveManager
    {
        public static void SavePlayerData(SaveData saveData)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/player.data";
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Position = 0;

            SaveData data = new SaveData(saveData);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SaveData LoadPlayerData()
        {
            string path = Application.persistentDataPath + "/player.data";
            File.Delete(path);

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                stream.Position = 0;

                SaveData data = formatter.Deserialize(stream) as SaveData;
                stream.Close();

                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
    }
}


