using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoSpawner : MonoBehaviour
{
    public GameObject[] Minos;
    public GameObject[] NextMinos;
    GameObject upNextMino = null;
    int MinoIndex = 0;
    int NextMinoIndex = 0;

    public void SpawnMino()
    {
        int MinoIndex = NextMinoIndex;
        Instantiate(Minos[MinoIndex], transform.position, Quaternion.identity);
        NextMinoIndex = Random.Range(0, Minos.Length);
        Vector3 NextMinoPos = new Vector3(16, 17, 0);

        if (upNextMino != null)
            Destroy(upNextMino);

        upNextMino = Instantiate(NextMinos[NextMinoIndex], NextMinoPos, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnMino();
    }
}
