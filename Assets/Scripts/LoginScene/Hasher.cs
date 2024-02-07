using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Hasher : MonoBehaviour
{
    byte[] tmpSource;
    byte[] tmpHash;

    public string HashData(string sourceData)
    {
        tmpSource = ASCIIEncoding.ASCII.GetBytes(sourceData);
        tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        return ByteArrayToString(tmpHash);
    }

    public string ByteArrayToString(byte[] arrInput)
    {
        int i;
        StringBuilder sOutput = new StringBuilder(arrInput.Length);
        for (i = 0; i < arrInput.Length; i++)
        {
            sOutput.Append(arrInput[i].ToString("X2"));
        }
        return sOutput.ToString();
    }
}
