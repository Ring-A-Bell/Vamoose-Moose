using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Consumables : MonoBehaviour
{
    private Tilemap tilemap;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            if(!audioSource.isPlaying)
                audioSource.Play();
            
            tilemap = GetComponent<Tilemap>();
            GridLayout gridLayout = tilemap.layoutGrid;
            Vector3Int cellPosition = gridLayout.WorldToCell(collider.gameObject.transform.position);
            if(tilemap.GetTile(cellPosition)!=null)
            {
                tilemap.SetTile(cellPosition, null);
                tilemap.SetTile(cellPosition + new Vector3Int(1,0,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(1,1,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(1,-1,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(-1,0,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(-1,1,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(-1,-1,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(0,1,0), null);
                tilemap.SetTile(cellPosition + new Vector3Int(0,-1,0), null);
                
                collider.gameObject.GetComponent<PlayerMovement>().AddEnergy();
            }

            
        }
    }
}
