using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoading : MonoBehaviour
{
    public int ChunkView;
    Vector2 PlayerChunkPos, lastPlayerChunk;
    public GameObject Player, Chunk;
    bool ChunksLoaded;
    public bool LoadedChunk;

    private void FixedUpdate()
    {
        PlayerChunkPos = new Vector2(Mathf.Floor((Player.transform.position.x + 5) / 10), Mathf.Floor((Player.transform.position.z + 5) / 10));

        if ( (lastPlayerChunk != PlayerChunkPos) || !ChunksLoaded )// If the player has moved off a chunk OR the chunks arent loaded (chunks loaded is for starting game)
        {
            Debug.Log("Loaded Chunks");
            LoadChunks();
            UnloadChunks();
        }
    }

    private void LoadChunks()
    {
        foreach (GameObject Water in GameObject.FindGameObjectsWithTag("Water")) Destroy(Water);
        lastPlayerChunk = PlayerChunkPos;// Updates players last known chunk position

        for (int x = -ChunkView; x <= ChunkView; x++)

            for (int y = -ChunkView; y <= ChunkView; y++)

                if (Vector2.Distance(PlayerChunkPos, PlayerChunkPos + new Vector2(x, y)) <= ChunkView / 1.5f)
                {
                    Instantiate(Chunk, new Vector3(x * 10 + PlayerChunkPos.x * 10, -3.12f, y * 10 + PlayerChunkPos.y * 10), Quaternion.identity);
                    LoadedChunk = true;
                }
                else
                {
                    LoadedChunk = false;
                }
        ChunksLoaded = true;
    }


    // Experimental
    private void UnloadChunks()
    {
        if (!LoadedChunk)
        {
            Destroy(Chunk.gameObject);
        }
    }
}
