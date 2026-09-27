using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MovingPlatformHorizontalLeft : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    List<GameObject> allPlatforms = new();

    Vector3 pos;

    void Start()
    {
        pos = obj.transform.position;
        if (File.Exists(Application.persistentDataPath + "/horizontaldata.json"))
        {
            var data = JsonUtility.FromJson<Vector3>(Application.persistentDataPath + "/horizontaldata.json");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            obj.transform.position += new Vector3(-3, 0, 0);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string data = JsonUtility.ToJson(obj.transform.position);
        File.WriteAllText(Application.persistentDataPath + "/horizontaldata.json", data);
    }

    private void OnTriggerStay(Collider other)
    {

    }
}