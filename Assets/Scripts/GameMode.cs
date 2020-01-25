using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public GameObject Obj;
    public GameObject Chosenobj;
    public GameObject InstantHexa;
    public RaycastHit2D[] SelectedHexas;
    
    private const float HorizontalGap = 0.8f;
    private const float VerticalGap = 0.5f;
    public float Columns, Rows;
    public float ScaleRatio;
    public string[,] HexaStatsArray;

    public void SetHexaNamerSize()
    {
        HexaStatsArray = new string[Mathf.FloorToInt(Columns), Mathf.FloorToInt(Rows)];
    }

    public void CreateHexa(float x, float y,int i,int j)
    {
        GameObject NewHexa = GameObject.Instantiate(Obj, new Vector2(x, y + 10), Quaternion.identity);
        InstantHexa = NewHexa;
        Hexagon_Script HexaObject = NewHexa.GetComponent<Hexagon_Script>();
        HexaObject.NewPos(x, y);
        HexaObject.Col = j;
        HexaObject.Row = i;
        //Objects.Add(new HexaInfo(hexaObject));
    }

    void SetScaleRatio()
    {
        if (Rows > Columns)
            ScaleRatio = 7 / Rows;
        else
            ScaleRatio = 6 / Columns;
    }

    //Oluşturulan Objeleri girilen sütun ve satır değerlerine göre dizer...
    public void CreateHexArray()
    {
        
        SetHexaNamerSize();
        SetScaleRatio();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                if (j % 2 != 0)
                    CreateHexa((j * HorizontalGap) * ScaleRatio, i * ScaleRatio,i,j);
                else
                    CreateHexa((j * HorizontalGap) * ScaleRatio, (i - VerticalGap) * ScaleRatio,i,j);
                
            }
        }
    }

    void ChosensHexagons()
    {
        if (Input.touchCount==1)
        {
            switch (Input.GetTouch(0).phase) {
                case TouchPhase.Began:
                GameObject.Destroy(GameObject.Find("Chosen"));
                float x = 0, y = 0;
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                RaycastHit2D[] hits = Physics2D.RaycastAll(mousePosition, new Vector2(0, 0), 0.01f);
                SelectedHexas = hits;
                if (hits.Length == 3) {

                    for (int i = 0; i < 3; i++)
                    {
                        GameObject a = hits[i].collider.gameObject;
                        Hexagon_Script b = a.GetComponent<Hexagon_Script>();
                        //Debug.Log(b.Col + ", " + b.Row);
                        x += a.transform.position.x;
                        y += a.transform.position.y;
                    }
                    GameObject.Instantiate(Chosenobj, new Vector2(x / 3, y / 3), Quaternion.identity);
                }
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateHexArray();
    }

    // Update is called once per frame
    void Update()
    {
        ChosensHexagons();
    }
}
