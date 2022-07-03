using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableRecordCreate : MonoBehaviour
{
    [Header("Table generation settings")]
    [SerializeField] private float Offset = 100;
    [SerializeField] private float DistansBetweenEntuty = 20;
    [SerializeField] private int MaxEntutyRecords = 10;
    [SerializeField] private GameObject EntutyRecord;
    [SerializeField] private string KeyPlayerPrefsRecords = "myrecord";

    [Header("Test tabel")]
    [SerializeField] private int MeRecord = 1000;
    [SerializeField] private bool  test= false;


    [Header("My ControllerScene")]
    [SerializeField] private ControllerScene CS;



    private string MyNickname;
    private List<BDPlayerTest> DictionaryPlayer;
    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
        InstantiateTableRecord();
        GiveMyNickname();
    }

    private void GiveMyNickname()
    {
        MyNickname = CS.MyNickname;
    }

    private void InstantiateTableRecord()
    {
        GameObject Entuty;
        EntutyRecordInreface EntutyInterface;
        for (int i = 0; i < MaxEntutyRecords; i++)
        {
            Entuty = Instantiate(EntutyRecord, this.transform);
            EntutyInterface = Entuty.GetComponent<EntutyRecordInreface>();

            EntutyInterface.SetPositionInTableEntity(i * -DistansBetweenEntuty -Offset);
            EntutyInterface.SetPosition(i + 1);
            EntutyInterface.SetName(DictionaryPlayer[i].Name);
            EntutyInterface.SetRecord(DictionaryPlayer[i].Record);
        }
    }

    private void ConnectToServer()
    {
        DictionaryPlayer = TestConnectServer(MaxEntutyRecords);
    }


    private List<BDPlayerTest> ListBD;
    private List<BDPlayerTest> TestConnectServer(int MaxPlaeyr)
    {
        ListBD = new List<BDPlayerTest>()
        {
            new BDPlayerTest( CS.MyNickname, MeRecord),
            new BDPlayerTest("test1" , Random.Range(100,100000)),
            new BDPlayerTest("test12", Random.Range(100,100000)),
            new BDPlayerTest("test2" , Random.Range(100,100000)),
            new BDPlayerTest("test3" , Random.Range(100,100000)),
            new BDPlayerTest("test4" , Random.Range(100,100000)),
            new BDPlayerTest("test5" , Random.Range(100,100000)),
            new BDPlayerTest("test6" , Random.Range(100,100000)),
            new BDPlayerTest("test7" , Random.Range(100,100000)),
            new BDPlayerTest("test8" , Random.Range(100,100000)),
            new BDPlayerTest("test9" , Random.Range(100,100000)),
            new BDPlayerTest("test0" , Random.Range(100,100000)),
            new BDPlayerTest("test4" , Random.Range(100,100000)),
            new BDPlayerTest("test3" , Random.Range(100,100000)),
            new BDPlayerTest("test4" , Random.Range(100,100000)),
            new BDPlayerTest("test5" , Random.Range(100,100000)),
            new BDPlayerTest("test6" , Random.Range(100,100000)),
            new BDPlayerTest("test7" , Random.Range(100,100000)),
            new BDPlayerTest("test8" , Random.Range(100,100000)),
            new BDPlayerTest("test4" , Random.Range(100,100000)),
            new BDPlayerTest("test13", Random.Range(100,100000))
        };

        if(!test)
        {
            BDPlayerTest NewP = ListBD[0];
            NewP.Record = PlayerPrefs.GetInt(KeyPlayerPrefsRecords);
            ListBD[0] = NewP;
        }

        BDPlayerTest temp;
        for (int i = 0; i < ListBD.Count - 1; i++)
        {
            for (int j = i + 1; j < ListBD.Count; j++)
            {
                if (ListBD[i].Record < ListBD[j].Record)
                {
                    temp = ListBD[i];
                    ListBD[i] = ListBD[j];
                    ListBD[j] = temp;
                }
            }
        }

        List<BDPlayerTest> ListTop = new List<BDPlayerTest>(10);
        for(int i = 0; i < MaxPlaeyr; i ++)
        {
            ListTop.Add(ListBD[i]);
        }

        return ListTop;
    }

    private struct BDPlayerTest
    {
        public int Record;
        public string Name;

        public BDPlayerTest(string Name, int Record)
        {
            this.Record = Record;
            this.Name = Name;
        }
    }

}
