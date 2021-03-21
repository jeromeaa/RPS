using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int enemyAmount=30;
    public int boxAmount = 15;
    public int specialBoxAmount = 5;
    public GameObject player;
    public List<GameObject> rpslist;
    public GameObject box;
    public GameObject specialBox;
    public int addEnemyEvery = 10;
    public int addEnemyAmount = 5;

    List<GameObject> spawnedEnemies = new List<GameObject>();
    List<GameObject> spawnedBox = new List<GameObject>();
    List<GameObject> spawnedSpBox = new List<GameObject>();
    float MinX=-58;
    float MaxX=57;
    float MinY=-34;
    float MaxY=32;

    bool check=false;
    int scr=0;
    void Start()
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            SpawnEnemy();
        }      
        for(int i=0; i < boxAmount; i++)
        {
            SpawnBox(box , spawnedBox);
        }
        for(int i=0; i< specialBoxAmount; i++)
        {
            SpawnBox(specialBox, spawnedSpBox);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E))
        //    SpawnEnemy();
        //if (Input.GetKeyDown(KeyCode.Q))
        //    SpawnBox();

        CheckEnemy();
        CheckBox(box, spawnedBox,boxAmount);
        CheckBox(specialBox, spawnedSpBox, specialBoxAmount);

        if (check)
        {
            enemyAmount += addEnemyAmount;
            check = false;
        }
    }

    private void SpawnEnemy()
    {
        int i = Random.Range(0, 3);
        bool checkplace = false;
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(MinX,MaxX), Random.Range(MinY,MaxY));
            if (pos.x > player.transform.position.x + 20 || pos.x < player.transform.position.x - 20 || pos.y > player.transform.position.y + 10 || pos.y < player.transform.position.y - 10)
                checkplace = true;
        } 
        while (checkplace == false);
        GameObject newObj=(Instantiate(rpslist[i], pos,Quaternion.identity)) as GameObject;
        spawnedEnemies.Add(newObj);
    }
    private void SpawnBox(GameObject bx , List<GameObject> bxlist)
    {
        bool checkplace = false;
        Vector2 pos;
        do
        {
            pos = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
            if (pos.x > player.transform.position.x + 20 || pos.x < player.transform.position.x - 20 || pos.y > player.transform.position.y + 10 || pos.y < player.transform.position.y - 10)
            {
                if(Physics2D.OverlapCircleAll(pos,10f, LayerMask.GetMask("Box")).Length==0)
                    checkplace = true;
            }
        }
        while (checkplace == false);
        GameObject newbox=Instantiate(bx, pos, Quaternion.identity);
        bxlist.Add(newbox);
    }


    private void CheckEnemy()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            if (spawnedEnemies[i] == null)
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
        while (spawnedEnemies.Count < enemyAmount)
            SpawnEnemy();
    }
    private void CheckBox(GameObject bx, List<GameObject> bxlist, int bxamnt)
    {
        for (int i = 0; i < bxlist.Count; i++)
        {
            if (bxlist[i] == null)
            {
                bxlist.RemoveAt(i);
            }
        }
        while (bxlist.Count < bxamnt)
            SpawnBox(bx,bxlist);
    }

    public void ScoreEnemyAdder()
    {
        scr++;
        if (scr == addEnemyEvery)
        {
            check = true;
            scr = 0;
        }
    }
}
