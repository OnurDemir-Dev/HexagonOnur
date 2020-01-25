using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChosenScript : MonoBehaviour
{
    GameMode gamemd;
    GameObject[] selectedobejects;
    Hexagon_Script[] hexascripts;
    void SetChosenStats()
    {
        this.name = "Chosen";
        Hexagon_Script[] instant=new Hexagon_Script[3];
        this.transform.localScale = new Vector2((this.transform.localScale.x + 0.01f) * gamemd.ScaleRatio, this.transform.localScale.y * gamemd.ScaleRatio);
        if (hexascripts[0].Col == hexascripts[1].Col)
        {
            if (hexascripts[2].Col > hexascripts[0].Col)
            {
                
                if (hexascripts[0].Row<hexascripts[1].Row)
                {
                    instant[0] = hexascripts[0];
                    instant[1] = hexascripts[1];
                    instant[2] = hexascripts[2];
                }
                else
                {
                    instant[0] = hexascripts[1];
                    instant[1] = hexascripts[0];
                    instant[2] = hexascripts[2];
                }
            }
            else
            {
                this.transform.Rotate(0, 0, 180);
                if (hexascripts[0].Row < hexascripts[1].Row)
                {
                    instant[0] = hexascripts[0];
                    instant[1] = hexascripts[2];
                    instant[2] = hexascripts[1];
                }
                else
                {
                    instant[0] = hexascripts[1];
                    instant[1] = hexascripts[2];
                    instant[2] = hexascripts[0];
                }
            }
        }
        else if (hexascripts[0].Col == hexascripts[2].Col)
        {
            if (hexascripts[1].Col > hexascripts[0].Col)
            {
                
                if (hexascripts[0].Row < hexascripts[2].Row)
                {
                    instant[0] = hexascripts[0];
                    instant[1] = hexascripts[2];
                    instant[2] = hexascripts[1];
                }
                else
                {
                    instant[0] = hexascripts[2];
                    instant[1] = hexascripts[0];
                    instant[2] = hexascripts[1];
                }
            }
            else
            {
                this.transform.Rotate(0, 0, 180);
                if (hexascripts[0].Row < hexascripts[2].Row)
                {
                    instant[0] = hexascripts[0];
                    instant[1] = hexascripts[1];
                    instant[2] = hexascripts[2];
                }
                else
                {
                    instant[0] = hexascripts[2];
                    instant[1] = hexascripts[1];
                    instant[2] = hexascripts[0];
                }
            }
        }
        else
        {
            if (hexascripts[0].Col > hexascripts[1].Col)
            {
                
                if (hexascripts[1].Row < hexascripts[2].Row)
                {
                    instant[0] = hexascripts[1];
                    instant[1] = hexascripts[2];
                    instant[2] = hexascripts[0];
                }
                else
                {
                    instant[0] = hexascripts[2];
                    instant[1] = hexascripts[1];
                    instant[2] = hexascripts[0];
                }
            }
            else
            {
                this.transform.Rotate(0, 0, 180);
                if (hexascripts[1].Row < hexascripts[2].Row)
                {
                    instant[0] = hexascripts[1];
                    instant[1] = hexascripts[0];
                    instant[2] = hexascripts[2];
                }
                else
                {
                    instant[0] = hexascripts[2];
                    instant[1] = hexascripts[0];
                    instant[2] = hexascripts[1];
                }
            }
        }
        hexascripts=instant;
        for (int i = 0; i < 3; i++)
        {
            Debug.Log(hexascripts[i].Col+", "+hexascripts[i].Row);
        }
    }
    void GetGameModeandValues()
    {
        gamemd = GameObject.Find("Create_Hexagon").GetComponent<GameMode>();
        selectedobejects = new GameObject[3];
        hexascripts = new Hexagon_Script[3];
        for (int i = 0; i < 3; i++)
        {
            selectedobejects[i] = gamemd.SelectedHexas[i].collider.gameObject;
            hexascripts[i] = selectedobejects[i].GetComponent<Hexagon_Script>();
        }
        
    }
    void TurnAll() {
        
            if (hexascripts != null)
            {

            for (int i = 0; i < 2; i++)
            {



                var tempArray = hexascripts.Select(hs => new { hs.x, hs.y, hs.Col, hs.Row }).ToArray();
                //GameObject tempArray = (GameObject[])hexascripts.Clone();
                hexascripts[0].x = tempArray[2].x;
                hexascripts[0].y = tempArray[2].y;
                hexascripts[0].Col = tempArray[2].Col;
                hexascripts[0].Row = tempArray[2].Row;



                hexascripts[1].x = tempArray[0].x;
                hexascripts[1].y = tempArray[0].y;
                hexascripts[1].Col = tempArray[0].Col;
                hexascripts[1].Row = tempArray[0].Row;



                hexascripts[2].x = tempArray[1].x;
                hexascripts[2].y = tempArray[1].y;
                hexascripts[2].Col = tempArray[1].Col;
                hexascripts[2].Row = tempArray[1].Row;
                hexascripts[0].IsPositionChanged = true;
                hexascripts[1].IsPositionChanged = true;
                hexascripts[2].IsPositionChanged = true;
            }
                

                //Hexagon_Script[] instant=new Hexagon_Script[3];

                //for (int i = 0; i < 3; i++)
                //{
                //    Hexagon_Script[] copyhexscript = hexascripts.ToArray();
                //    for (int j = 0; j < 3; j++)
                //    {
                //        if (j == 2)
                //        {
                //            Debug.Log("Hi");
                //            instant[0] = hexascripts[2];
                //            hexascripts[2].x = copyhexscript[0].x;
                //            hexascripts[2].y = copyhexscript[0].y;
                //            hexascripts[2].IsDrop = true;
                //        }
                //        else
                //        {
                //            Debug.Log("Deneme");
                //            instant[j+1] = hexascripts[j];
                //            hexascripts[j].x = copyhexscript[j+1].x;
                //            hexascripts[j].y = copyhexscript[j+1].y;
                //            hexascripts[j].IsDrop = true;
                //        }

                //    }

                //    hexascripts = instant;
                //}
            }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GetGameModeandValues();
        SetChosenStats();
        TurnAll();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}
