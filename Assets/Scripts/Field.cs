using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public static Transform[,] field = new Transform[10, 20];

    public static void PrintArray()
    {
        string arrayOutput = "";

        int iMax = field.GetLength(0) - 1;
        int jMax = field.GetLength(1) - 1;

        for(int j = jMax; j >= 0; j--)
        {
            for(int i = 0; i <= iMax; i++)
            {
                if(field[i, j] == null)
                {
                    arrayOutput += "N ";
                } else {
                    arrayOutput += "X ";
                }
            }
            arrayOutput += "\n";
        }

        var DebugView = GameObject.Find("DebugView").GetComponent<Text>();
        DebugView.text = arrayOutput;
    }

    public static bool DeleteAllFullRows()
    {
        for(int row = 0; row < 20; row++)
        {
            if(IsRowFull(row))
            {
                DeleteRow(row);

                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Delete);

                DeleteAllFullRows();
            }
        }
        return false;
    }

    public static bool IsRowFull(int row)
    {
        for (int col = 0; col < 10; col++)
        {
            if (field[col, row] == null)
            {
                return false;
            }
        }
        return true;
    }

    public static void DeleteRow(int row)
    {
        for (int col = 0; col < 10; col++)
        {
            Destroy(field[col, row].gameObject);
            field[col, row] = null;
        }

        row++;

        for (int j = row; j < 20; j++)
        {
            for (int i = 0; i < 10; i++)
            {
                if(field[i, j] != null)
                {
                    field[i, j - 1] = field[i, j];
                    field[i, j] = null;
                    field[i, j - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
    }
}
