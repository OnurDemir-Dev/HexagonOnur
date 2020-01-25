using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
[System.Serializable]
public struct HexaStats
{
    public string HexaName;
    public Color HexaColor;

}
public class Hexagon_Script : MonoBehaviour
{
    public bool IsPositionChanged = false;

    public bool IsDrop = true;
    public float x, y;
    Renderer rend;
    public HexaStats[] Hex;
    GameMode gamemd;
    HexaStats hexstt;
    public int Col;
    public int Row;
    HexaStats Randomhexastatstart()
    {
        List<HexaStats> InstantHex = Hex.ToList();
        int i = 0;
        int index;
        bool notup = false;
        if (Col % 2 == 0)
        {
            notup = true;
        }
        if (Row == 0)
        {
            i = Random.Range(0, InstantHex.Count);
        }
        else if (Col == 0)
        {

            if ((gamemd.HexaStatsArray[Col, Row - 1] == gamemd.HexaStatsArray[Col + 1, Row - 1]))
            {
                index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col, Row - 1]);
                InstantHex.RemoveAt(index);
                i = Random.Range(0, InstantHex.Count);
            }
            else
                i = Random.Range(0, Hex.Length);
        }
        else if (Col == gamemd.Columns - 1)
        {
            if (notup)
            {
                if (gamemd.HexaStatsArray[Col - 1, Row] == gamemd.HexaStatsArray[Col - 1, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row]);
                    InstantHex.RemoveAt(index);
                    i = Random.Range(0, InstantHex.Count);
                }
                if (gamemd.HexaStatsArray[Col - 1, Row - 1] == gamemd.HexaStatsArray[Col, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row - 1]);
                    if (index != -1)
                    {
                        InstantHex.RemoveAt(index);
                    }
                }


                i = Random.Range(0, InstantHex.Count);

            }
            else
            {
                if (gamemd.HexaStatsArray[Col - 1, Row] == gamemd.HexaStatsArray[Col, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row]);
                    InstantHex.RemoveAt(index);
                    i = Random.Range(0, InstantHex.Count);
                }
                else
                {
                    i = Random.Range(0, Hex.Length);
                }
            }
        }
        else
        {
            if (notup)
            {
                if (gamemd.HexaStatsArray[Col - 1, Row] == gamemd.HexaStatsArray[Col - 1, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row]);
                    InstantHex.RemoveAt(index);
                    //i = Random.Range(0, InstantHex.Count);
                }
                if (gamemd.HexaStatsArray[Col - 1, Row - 1] == gamemd.HexaStatsArray[Col, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row - 1]);
                    if (index != -1)
                    {
                        InstantHex.RemoveAt(index);
                    }
                    //i = Random.Range(0, InstantHex.Count);
                }
                if (gamemd.HexaStatsArray[Col, Row - 1] == gamemd.HexaStatsArray[Col + 1, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col, Row - 1]);
                    if (index != -1)
                    {
                        InstantHex.RemoveAt(index);
                    }
                    //i = Random.Range(0, InstantHex.Count);
                }


                i = Random.Range(0, InstantHex.Count);

            }
            else
            {
                if (gamemd.HexaStatsArray[Col - 1, Row] == gamemd.HexaStatsArray[Col, Row - 1])
                {
                    index = InstantHex.FindIndex(s => s.HexaName == gamemd.HexaStatsArray[Col - 1, Row]);
                    InstantHex.RemoveAt(index);
                    i = Random.Range(0, InstantHex.Count - 1);
                }
                else
                {
                    i = Random.Range(0, Hex.Length);
                }
            }
        }
        //Debug.Log(Hex[i].HexaName);
        return InstantHex[i];

    }

    void SetHexagonName(HexaStats name)
    {
        gamemd.HexaStatsArray[Col, Row] = name.HexaName;
    }
    void SetHexagonStat()
    {
        hexstt = Randomhexastatstart();
        SetHexagonName(hexstt);
        //Debug.Log(gamemd.HexaStatsArray[Col,Row]);
        float scaleratio = gamemd.ScaleRatio;
        rend = GetComponent<Renderer>();
        this.name = hexstt.HexaName;
        //Vector2 down = transform.TransformDirection(Vector2.down);

        //HexaInfo[] relatedObjects = GameMode.Objects.Where(o => o.IsInRange(Row,Col)).ToArray();

        rend.material.color = hexstt.HexaColor;
        this.transform.localScale = new Vector2(scaleratio, scaleratio);
    }
    GameMode Getgamemodescript()
    {
        GameObject g = GameObject.Find("Create_Hexagon");
        return g.GetComponent<GameMode>();
    }
    public void NewPos(float _x, float _y)
    {
        x = _x;
        y = _y;
    }
    private void Drop()
    {
        if (IsDrop)
        {
            if (transform.position.y - y < 0.01f)
            {
                IsDrop = false;
                transform.position = new Vector3(x, y, 0);
            }
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), Time.deltaTime * 5f);
        }
    }
    private void PositionChanged()
    {
        if (IsPositionChanged)
        {
           
            transform.position = Vector3.Lerp(transform.position, new Vector3(x, y, 0), Time.deltaTime * 5f);
            gamemd.HexaStatsArray[Col, Row]=this.name;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gamemd = Getgamemodescript();
        SetHexagonStat();
    }

    // Update is called once per frame
    void Update()
    {
        PositionChanged();
        Drop();
    }


}
