using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harvested : MonoBehaviour
{
    public GameObject cropOnField;
    public Transform self;
    public GameObject _buffManagerObject;
    private BuffManager _buff;
    public int[] howMany;

    // Start is called before the first frame update
    Vector3 aboveSoil(Vector3 pos) {
        return new Vector3(pos.x, pos.y + 1, pos.z); 
    }

    void Start()
    {
        _buffManagerObject = GameObject.Find("BuffManager");
        _buff = _buffManagerObject.GetComponent<BuffManager>();
    }

    public void Harvesting()
    {
        Destroy(gameObject);
        int many = howMany[Random.Range(0, howMany.Length)];
        if (_buff.orangePray) {
            float manyFloat = (float)many;
            manyFloat *= 1.4f;
            many = Mathf.RoundToInt(manyFloat);
        }
        for (int i = 0; i < many; i++) {
            Instantiate(cropOnField, aboveSoil(self.position), self.rotation);
        }
    }
}
