using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public interface EntutyRecordInreface
{
    public void SetPositionInTableEntity(float PositionInTable);
    public void SetPosition(int Position);
    public void SetName(string Name);
    public void SetRecord(int Record);
}

public class EntutyRecord : MonoBehaviour, EntutyRecordInreface
{

    [SerializeField] public Text Name;  
    [SerializeField] public Text Record; 
    [SerializeField] public Text Position;
    [SerializeField] public RectTransform PositionEntuty;


    private void Start()
    {

        

    }
    public void SetPositionInTableEntity(float PositionInTable)
    {
        PositionEntuty.anchoredPosition = new Vector3(0, PositionInTable, 0);

    }
    public void SetPosition(int Position)
    {
        this.Position.text = Position.ToString();
    }
    public void SetName(string Name)
    {
        this.Name.text = Name;
    }
    public void SetRecord(int Record)
    {
        this.Record.text = Record.ToString();
    }
}
