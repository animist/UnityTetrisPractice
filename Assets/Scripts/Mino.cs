using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mino : MonoBehaviour
{
    public static float Speed = 1.0f;

    float LastMoveDown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpdateField();

        if(!IsInGrid())
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.GameOver);
            Debug.Log("GameOver");
            Mino.Speed = 1.0f;

            Invoke("OpenGameOverScene", .1f);
        }

        InvokeRepeating("IncreaseSpeed", 2.0f, 2.0f);
    }

    void OpenGameOverScene()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("GameOver");
    }

    void IncreaseSpeed()
    {
        Mino.Speed -= 0.001f;
        if (Mino.Speed < 0.05f)
        {
            Mino.Speed = 0.1f;
        }
        Debug.Log("Speed : " + Mino.Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            transform.position += new Vector3(-1, 0, 0);
            //Debug.Log(transform.position);

            if (!IsInGrid())
            {
                //Debug.Log("not in grid");
                transform.position += new Vector3(1, 0, 0);
            } else {
                UpdateField();
            }
        }
        if (Input.GetKeyDown("l"))
        {
            transform.position += new Vector3(1, 0, 0);
            // Debug.Log(transform.position);

            if (!IsInGrid())
            {
                // Debug.Log("not in grid");
                transform.position += new Vector3(-1, 0, 0);
            } else {
                UpdateField();
            }
        }
        if (Input.GetKeyDown("j") || Time.time - LastMoveDown >= Mino.Speed)
        {
            transform.position += new Vector3(0, -1, 0);
            // Debug.Log(transform.position);

            if (!IsInGrid())
            {
                //Debug.Log("not in grid");
                transform.position += new Vector3(0, 1, 0);
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Strike);

                Field.DeleteAllFullRows();

                enabled = false;

                FindObjectOfType<MinoSpawner>().SpawnMino();
            } else {
                UpdateField();
            }
            
            LastMoveDown = Time.time;
        }
        if (Input.GetKeyDown("k"))
        {
            transform.Rotate(0, 0, 90);
            //Debug.Log(transform.position);

            if (!IsInGrid())
            {
                //Debug.Log("not in grid");
                transform.Rotate(0, 0, -90);
            } else {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Rotate);
                UpdateField();
            }
        }
        if (Input.GetKeyDown("i"))
        {
            transform.Rotate(0, 0, -90);
            //Debug.Log(transform.position);

            if (!IsInGrid())
            {
                //Debug.Log("not in grid");
                transform.Rotate(0, 0, 90);
            } else {
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.Rotate);
                UpdateField();
            }
        }
    }

    public bool IsInGrid()
    {
        int ChildCount = 0;

        foreach(Transform ChildBlock in transform)
        {
            Vector2 vect = RoundVector(ChildBlock.position);
            ChildCount++;

            Debug.Log("check!! : " + ChildCount + " : " + ChildBlock.position);

            if(!IsInBorder(ChildBlock.position))
            {
                return false;
            }

            if ((int)vect.x >= 0 && (int)vect.y < 20)
            {
                Debug.Log(Field.field[(int)vect.x, (int)vect.y] != null);
                if(Field.field[(int)vect.x, (int)vect.y] != null)
                    Debug.Log(Field.field[(int)vect.x, (int)vect.y].parent != transform);
            }
            if ((int)vect.x >= 0 && (int)vect.y < 20 &&
                Field.field[(int)vect.x, (int)vect.y] != null &&
                Field.field[(int)vect.x, (int)vect.y].parent != transform)
                {
                    return false;
                }
        }
        return true;
    }

    public Vector2 RoundVector(Vector2 vect)
    {
        //Debug.Log("Rounding : " + vect.x + " -> " + Mathf.Round(vect.x) + " / " + vect.y + " -> " + Mathf.Round(vect.y));
        return new Vector2(Mathf.Round(vect.x), Mathf.Round(vect.y));
    }

    public static bool IsInBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x <= 9 &&
                (int)pos.y >= 0);
    }

    public void UpdateField()
    {
        for (int y = 0; y < 20; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                if (Field.field[x, y] != null &&
                    Field.field[x, y].parent == transform) // What means this??
                    {
                        Field.field[x, y] = null;
                    }
            }
        }

        foreach(Transform ChildBlock in transform)
        {
            Vector2 vect = RoundVector(ChildBlock.position);

            //Debug.Log("Cube At : " + (int)vect.x + " / " + (int)vect.y);
            if ((int)vect.x < 0 || (int)vect.y > 19) continue;
            Field.field[(int)vect.x, (int)vect.y] = ChildBlock;
        }

        Field.PrintArray();
    }
}
