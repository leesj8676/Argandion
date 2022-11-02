using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private Rigidbody rigid;
    private Transform trans;
    private BoxCollider box;
    public int itemCode;
    public ItemObject itemObject;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        rigid.AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        string jsonString = File.ReadAllText(Application.dataPath + "/Scripts/ItemTable.json");
        var itemData = JsonHelper.FromJson<ItemObject>(jsonString);
        itemObject = itemData[itemCode];
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        if (trans.position.y < 0.5f) {
            trans.position = new Vector3(trans.position.x, 0.5f, trans.position.z);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("wateredDirt") || other.gameObject.CompareTag("dirt")) {
            rigid.useGravity = false;
            rigid.isKinematic = true;
        }
    }
}