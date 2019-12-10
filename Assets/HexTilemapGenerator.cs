using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTilemapGenerator : MonoBehaviour
{
    public GameObject hexTilePrefab;
    public Transform holder;
    [SerializeField] int mapWidth = 10;
    [SerializeField] int mapHeight = 10;
    float tileXOffset = 1.99f;
    float tileZOffset = 1.72f;

    void Start()
    {
        CreateHexTileMap();
    }

   
    void CreateHexTileMap()
    {
        float mapXMin = -mapWidth / 2;
        float mapXMax = mapWidth / 2;

        float mapZmin = -mapHeight / 2;
        float mapZmax = mapHeight / 2;

        for(float x = mapXMin; x < mapXMax; x++){
            for(float z = mapZmin; z < mapZmax; z++){
                GameObject TempGO = Instantiate(hexTilePrefab);
                Vector3 pos;

                if(z % 2 == 0){
                    pos = new Vector3(x * tileXOffset, 0, z * tileZOffset); 
                }
                else{
                    pos = new Vector3(x * tileXOffset + tileXOffset/2, 0, z * tileZOffset);
                }
                StartCoroutine(SetTileInfo(TempGO, x, z, pos));
            }
        }
    }

    IEnumerator SetTileInfo(GameObject GO, float x, float z, Vector3 pos){
        yield return new WaitForSeconds(0.00001f);
        GO.transform.parent = holder;
        GO.name = x.ToString() + ", " + z.ToString();
        GO.transform.position = pos;
    }

    void OnTriggerExit(Collider other){
        Destroy(other.gameObject);
    }
}
