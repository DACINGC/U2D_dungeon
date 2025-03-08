using System.Diagnostics;
using System.IO;
using UnityEngine;
using System;
public class FileDataHandler
{
    private string dataDirPath;
    private string dataFileName;
    private bool encriptCode;
    private string codeword = "yourData";

    public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encriptCode )
    { 
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
        encriptCode = _encriptCode;
    }

    public void Save(GameData _data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_data, true);

            if (encriptCode)
                dataToStore = EncriptDecript(dataToStore);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError("������Ϸ��������" + fullPath + "\n"+ e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }

                }

                if (encriptCode)
                    dataToLoad =  EncriptDecript(dataToLoad);

                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError("������Ϸ��������" + fullPath + "\n" + e);
            }
        }


        return loadData;
    }

    //ɾ���ļ�
    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        if(File.Exists(fullPath))
        { 
            File.Delete(fullPath); 
        }

    }

    //�����ļ�
    public string EncriptDecript(string _data)
    {
        string modifieData = "";
        for (int i = 0; i < _data.Length; i++)
        {
            modifieData += (char)(_data[i] ^ codeword[i % codeword.Length]);
        }
        return modifieData;
    }
}
