 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 startPosition;
    private Vector3 oldPosition;
    private bool isTurn = false;

    private int moveCnt = 0;
    private int turnCnt = 0;
    private int spawnCnt = 0;

    private bool isDie = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }

        else if (Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
    }

    private void Init()
    {
        startPosition = transform.position;
        oldPosition = transform.localPosition;

        moveCnt = 0;
        spawnCnt = 0;
        isTurn = false;
        spriteRenderer.flipX = isTurn;
        isDie = false;
    }
    private void CharTurn()
    {
        isTurn =isTurn == true ? false : true;

        spriteRenderer.flipX = isTurn;
    }

    private void CharMove()
    {

        moveCnt++;
        MoveDirection();

        if(isFailTurn())    //사망판정
        {
            CharDie();
            Debug.Log("GameOver");
            return;
        }

        if(moveCnt > 5)
        {
            RespawnStairs();
        }
    }

    private void MoveDirection()
    {
        if (isTurn) //legt
        {
            oldPosition += new Vector3(-0.75f, 0.5f, 0);
        }
        else
        {
            oldPosition += new Vector3(0.75f, 0.5f, 0);
        }

        transform.position = oldPosition;

    }

    private bool isFailTurn()
    {
        bool resurt = false;

        if (GameManager.Instance.isTurn[turnCnt] != isTurn)
        {
            resurt = true;
        }

        turnCnt++;

        if(turnCnt > GameManager.Instance.stairs.Length -1)     //0~19 길이는 20
        {
            turnCnt = 0;
        }

        return resurt;
    }

    private void RespawnStairs()
    {
        GameManager.Instance.SpawnStair(spawnCnt);

        spawnCnt++;

        if(spawnCnt > GameManager.Instance.stairs.Length - 1)
        {
            spawnCnt = 0;
        }
    }
    private void CharDie()
    {
        isDie = true;
    }
}
