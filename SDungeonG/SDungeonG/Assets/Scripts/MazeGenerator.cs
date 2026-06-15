using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class MazeGenerator : MonoBehaviour
{
    public Transform plane;
    public Transform player;
    public Transform finish;
    [Header("Maze Size")]
    public int width = 20;
    public int height = 20;
    public float cellSize = 3f;

    [Header("Prefabs")]
    public GameObject wallPrefab;

    [Header("Seed")]
    public int seed = 12345;
    public bool useRandomSeed = true;

    private Cell[,] cells;

    [Header("UI")]
    public TMP_Text seedText;

    [Header("Debug Panel")]
    public TMP_InputField seedInput;
    public TMP_InputField sizeInput;

    public int winCount = 0;
    public bool useExternalStartFinish;

    private class Cell
    {
        public bool visited;
        public bool topWall = true;
        public bool bottomWall = true;
        public bool leftWall = true;
        public bool rightWall = true;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("MazeSeed"))
        {
            seed = PlayerPrefs.GetInt("MazeSeed");
            useRandomSeed = PlayerPrefs.GetInt("UseRandomSeed", 1) == 1;
        }

        if (PlayerPrefs.HasKey("MazeSize"))
        {
            int savedSize = PlayerPrefs.GetInt("MazeSize");
            width = savedSize;
            height = savedSize;
        }

        GenerateMaze();

        //cells[0, 0].bottomWall = false;
        //cells[width - 1, height - 1].topWall = false;

        ApplySceneMode();
        BuildMaze();
        PositionSceneObjects();
    }

    void GenerateMaze()
    {
        if (useRandomSeed)
        {
            seed = Random.Range(0, 999999);
        }

        Random.InitState(seed);

        if (seedText != null)
        {
            seedText.text = "SEED: " + seed;
        }

        Debug.Log("Maze Seed: " + seed);

        cells = new Cell[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                cells[x, y] = new Cell();

        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        Vector2Int current = new Vector2Int(0, 0);
        cells[0, 0].visited = true;
        stack.Push(current);

        while (stack.Count > 0)
        {
            current = stack.Peek();
            List<Vector2Int> neighbors = GetUnvisitedNeighbors(current);

            if (neighbors.Count > 0)
            {
                Vector2Int chosen = neighbors[Random.Range(0, neighbors.Count)];
                RemoveWall(current, chosen);
                cells[chosen.x, chosen.y].visited = true;
                stack.Push(chosen);
            }
            else
            {
                stack.Pop();
            }
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        Check(cell.x, cell.y + 1, neighbors);
        Check(cell.x, cell.y - 1, neighbors);
        Check(cell.x - 1, cell.y, neighbors);
        Check(cell.x + 1, cell.y, neighbors);

        return neighbors;
    }

    void Check(int x, int y, List<Vector2Int> neighbors)
    {
        if (x >= 0 && x < width && y >= 0 && y < height && !cells[x, y].visited)
            neighbors.Add(new Vector2Int(x, y));
    }

    void RemoveWall(Vector2Int a, Vector2Int b)
    {
        if (b.y > a.y)
        {
            cells[a.x, a.y].topWall = false;
            cells[b.x, b.y].bottomWall = false;
        }
        else if (b.y < a.y)
        {
            cells[a.x, a.y].bottomWall = false;
            cells[b.x, b.y].topWall = false;
        }
        else if (b.x < a.x)
        {
            cells[a.x, a.y].leftWall = false;
            cells[b.x, b.y].rightWall = false;
        }
        else if (b.x > a.x)
        {
            cells[a.x, a.y].rightWall = false;
            cells[b.x, b.y].leftWall = false;
        }
    }

    void BuildMaze()
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                Vector3 basePos = new Vector3(x * cellSize, 0, y * cellSize);

                if (cells[x, y].topWall)
                    CreateWall(basePos + new Vector3(0, 1, cellSize / 2), new Vector3(cellSize, 2, 0.2f));

                if (cells[x, y].bottomWall)
                    CreateWall(basePos + new Vector3(0, 1, -cellSize / 2), new Vector3(cellSize, 2, 0.2f));

                if (cells[x, y].leftWall)
                    CreateWall(basePos + new Vector3(-cellSize / 2, 1, 0), new Vector3(0.2f, 2, cellSize));

                if (cells[x, y].rightWall)
                    CreateWall(basePos + new Vector3(cellSize / 2, 1, 0), new Vector3(0.2f, 2, cellSize));
            }
    }

    void CreateWall(Vector3 position, Vector3 scale)
    {
        GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity, transform);
        wall.transform.localScale = scale;
    }

    void PositionSceneObjects()
    {
        float centerX = (width - 1) * cellSize / 2f;
        float centerZ = (height - 1) * cellSize / 2f;

        if (plane != null)
        {
            plane.position = new Vector3(centerX, -0.05f, centerZ);
            plane.localScale = new Vector3(width * cellSize / 10f + 1f, 1, height * cellSize / 10f + 1f);
        }

        if (player != null)
        {
            CharacterController cc = player.GetComponent<CharacterController>();

            if (cc != null)
                cc.enabled = false;

            player.position = new Vector3(0f, 1f, 0f);

            if (cc != null)
                cc.enabled = true;
        }

        if (finish != null)
        {
            finish.position = new Vector3(
                (width - 1) * cellSize,
                0.25f,
                (height - 1) * cellSize
            );
        }

        //    if (player != null)
        //    {
        //        CharacterController cc = player.GetComponent<CharacterController>();

        //        if (cc != null)
        //        {
        //            cc.enabled = false;
        //        }

        //        player.position = new Vector3(0f, 1f, -cellSize);

        //        if (cc != null)
        //        {
        //            cc.enabled = true;
        //        }
        //    }

        //    if (finish != null)
        //    {
        //        finish.position = new Vector3((width - 1) * cellSize, 0.25f, height * cellSize);
        //    }
    }

    public void ApplySettings()
    {
        Debug.Log("SET button pressed");

        if (!string.IsNullOrWhiteSpace(seedInput.text))
        {
            if (int.TryParse(seedInput.text, out int newSeed))
            {
                seed = newSeed;
                useRandomSeed = false;

                PlayerPrefs.SetInt("MazeSeed", seed);
                PlayerPrefs.SetInt("UseRandomSeed", 0);
            }
        }

        if (!string.IsNullOrWhiteSpace(sizeInput.text))
        {
            if (int.TryParse(sizeInput.text, out int newSize))
            {
                width = newSize;
                height = newSize;

                PlayerPrefs.SetInt("MazeSize", newSize);
            }
        }

        PlayerPrefs.Save();

        RegenerateMaze();
    }

    public void RegenerateMaze()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        GenerateMaze();

        //cells[0, 0].bottomWall = false;
        //cells[width - 1, height - 1].topWall = false;

        ApplySceneMode();
        BuildMaze();
        PositionSceneObjects();
    }

    public void NextMaze()
    {
        winCount++;

        // Her 5 kazanmada maze buyusun
        if (winCount % 5 == 0)
        {
            width++;
            height++;
        }

        useRandomSeed = true;

        seed = Random.Range(0, 999999);

        RegenerateMaze();
    }

    void ApplySceneMode()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        useExternalStartFinish = sceneName == "TestScene";

        if (useExternalStartFinish)
        {
            cells[0, 0].bottomWall = false;
            cells[width - 1, height - 1].topWall = false;
        }
    }
}