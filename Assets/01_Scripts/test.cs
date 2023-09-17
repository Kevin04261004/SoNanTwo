using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct Item
{
    public string name;
    public string dl; // 설명
    public int price; // 가격

    public Item(string _name, string _dl, int  _price)
    {
        name = _name;
        dl = _dl;
        price = _price;
    }
}
public class test : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    void Start()
    {
        List<Dictionary<string, string>> ItemDataTable;

        ItemDataTable = CSVLoader.Read("Items");
        int count = 0;
        foreach (var value in ItemDataTable)
        {
            items.Add(new Item(
                value["Name"],
                value["Description"],
                int.Parse(value["Price"])
                ));
            Debug.Log(items[count].name + items[count].dl + items[count].price);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
