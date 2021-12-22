using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<LevelBlock> currentLevelBlock = new List<LevelBlock>();
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public Transform StartLevelPosition;
    public List<LevelBlock> endLevelBlock;
    public static LevelManager levelManager;
    public int maxLevels = 4;
    public string nextScene = ""; 
    private void Awake()
    {
        if (levelManager == null) levelManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GenerateLevelBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Agrega los bloques de niveles o plataformas, enemigos y coleccionables en la escena
    /// </summary>
    void AddLevelBlock()
    {
        //int randomBlocks = Random.Range(0,allTheLevelBlocks.Count);
        int randomBlocks = Random.Range(0, 2);
        LevelBlock levelBlock;
        Vector3 position;

        if (currentLevelBlock.Count == 0)
        {
            levelBlock = Instantiate(allTheLevelBlocks[0]);
            position = StartLevelPosition.position;
        }
        else
        {
            levelBlock = Instantiate(allTheLevelBlocks[randomBlocks]);
            position = currentLevelBlock[currentLevelBlock.Count - 1].EndPoint.position;
        }
        levelBlock.transform.SetParent(this.transform,false); //Los bloques se añaden como hijos del level manager
        levelBlock.transform.position = new Vector3(
            position.x - levelBlock.StartPoint.position.x,
            position.y - levelBlock.StartPoint.position.y,
            0);
        currentLevelBlock.Add(levelBlock);
    }
    /// <summary>
    /// Cantidad de bloques de nivel a generar
    /// </summary>
    public void GenerateLevelBlocks()
    {
        for (int i = 0; i <= maxLevels; i++)
        {
            if (i == maxLevels)
            {
                EndLevelBlock();
                break;
            }
            else
            {
                AddLevelBlock();
            }
        }
    }
    /// <summary>
    /// Destruir un bloque de nivel
    /// </summary>
    public void EndLevelBlock()
    {
        Vector3 lastPosition;
        LevelBlock lastLevelBlock;

        lastLevelBlock = Instantiate(endLevelBlock[0]);
        lastPosition = currentLevelBlock[currentLevelBlock.Count - 1].EndPoint.position;

        lastLevelBlock.transform.SetParent(transform,false);
        lastLevelBlock.transform.position = new Vector3(
            lastPosition.x - lastLevelBlock.StartPoint.position.x,
            lastPosition.y - lastLevelBlock.StartPoint.position.y,
            0);
        currentLevelBlock.Add(lastLevelBlock);
    }
    /// <summary>
    /// Destruir todos los bloques de nivel. Sucede cuando se reinicia o pierde la partida
    /// </summary>
    public void RemoveAllLevelBlock()
    {
        while(currentLevelBlock.Count > 0)
        {
            LevelBlock oldBlock = currentLevelBlock[0];
            currentLevelBlock.Remove(oldBlock);
            Destroy(oldBlock.gameObject);
        }
    }
}
