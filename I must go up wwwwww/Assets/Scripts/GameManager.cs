using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager  : MonoBehaviour
{

    public static GameManager Instance;

    [Header("°è´Ü")]
    [Space(10)]
    public GameObject[] stairs;
    public bool[] isTurn;
    private enum State {Start,Left,Right};
    private State state;
    private Vector3 oldPosition;
    
    void Start()
    {
        Instance = this;

        Init();

        InitStairs();
    }

    private void Init()
    {
        state = State.Start;
        oldPosition = Vector3.zero;

        isTurn = new bool[stairs.Length];

        for (int i = 0; i < stairs.Length; i++)
        {
            stairs[i].transform.position = Vector3.zero;
            isTurn[i] = false;
        }
    }

    private void InitStairs()
    {
        for(int i = 0; i < stairs.Length; i++)
        {
            switch (state)
            {
                case State.Start:
                    stairs[i].transform.position = new Vector3(0.75f,-0.1f,0);
                    state = State.Right;
                    break;

                case State.Left:
                    stairs[i].transform.position = oldPosition + new Vector3(-0.75f, 0.5f, 0);
                    isTurn[i] = true;
                    break;

                case State.Right:
                    stairs[i].transform.position = oldPosition + new Vector3(0.75f, 0.5f, 0);
                    isTurn[i] = false;
                    break;

            }

            oldPosition = stairs[i].transform.position;

            if(i != 0)
            {
                int rand = Random.Range(0,5);
                if(rand < 2 && i < stairs.Length - 1)
                {
                    state = state == State.Left ? State.Right : State.Left;
                }
            }
        }
    }

    public void SpawnStair(int cnt)
    {
        int rand = Random.Range(0, 5);
        if (rand < 2 )
        {
            state = state == State.Left ? State.Right : State.Left;
        }
        switch (state)
        {
          

            case State.Left:
                stairs[cnt].transform.position = oldPosition + new Vector3(-0.75f, 0.5f, 0);
                isTurn[cnt] = true;
                break;

            case State.Right:
                stairs[cnt].transform.position = oldPosition + new Vector3(0.75f, 0.5f, 0);
                isTurn[cnt] = false;
                break;

        }

        oldPosition = stairs[cnt].transform.position;
    }

}
