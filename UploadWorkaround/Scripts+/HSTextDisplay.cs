using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HSTextDisplay : MonoBehaviour
{
    //public string textValue = "Highscore Table";
    public Text TableBase;
    public Text Table;

    // Start is called before the first frame update
    void Start()
    {
        TableBase.text = "Highscore table\n==============";
        //Hardcoded values
        Table.text = "1. Username1 - 10000\n2. Username2 - 9500\n3. Username3 - 9000\n4. Username4 - 8500";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
