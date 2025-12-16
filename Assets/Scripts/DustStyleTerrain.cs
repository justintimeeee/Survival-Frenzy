using UnityEngine;

public class DustStyleTerrain : MonoBehaviour
{
    public Material groundMat;
    public Material wallMat;
    public Material crateMat;

    float mapSize = 100f;
    float wallHeight = 6f;
    float wallThickness = 2f;

    void Start()
    {
        CreateFlatGround();
        CreateBoundaryWalls();
        CreateCrates();
    }

    void CreateFlatGround()
    {
        GameObject ground = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ground.transform.position = Vector3.zero;
        ground.transform.localScale = new Vector3(mapSize, 1, mapSize);

        if (groundMat != null)
            ground.GetComponent<MeshRenderer>().material = groundMat;
    }

    void CreateBoundaryWalls()
    {
        float half = mapSize / 2f;

        CreateWall(new Vector3(0, wallHeight / 2, half), new Vector3(mapSize, wallHeight, wallThickness));
        CreateWall(new Vector3(0, wallHeight / 2, -half), new Vector3(mapSize, wallHeight, wallThickness));
        CreateWall(new Vector3(half, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, mapSize));
        CreateWall(new Vector3(-half, wallHeight / 2, 0), new Vector3(wallThickness, wallHeight, mapSize));
    }

    GameObject CreateWall(Vector3 pos, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.transform.position = pos;
        wall.transform.localScale = scale;

        if (wallMat != null)
            wall.GetComponent<MeshRenderer>().material = wallMat;

        return wall;
    }

    void CreateCrates()
    {
        // Center cover cluster
        CreateCrate(new Vector3(0, 1, 0));
        CreateCrate(new Vector3(2, 1, 1));
        CreateCrate(new Vector3(-2, 1, -1));

        // North side
        CreateCrate(new Vector3(10, 1, 30));
        CreateCrate(new Vector3(12, 1, 28));
        CreateCrate(new Vector3(8, 1, 32));
        CreateCrate(new Vector3(10, 3, 30));

        // South side
        CreateCrate(new Vector3(-15, 1, -25));
        CreateCrate(new Vector3(-18, 1, -22));
        CreateCrate(new Vector3(-12, 1, -23));

        // East side
        CreateCrate(new Vector3(30, 1, 5));
        CreateCrate(new Vector3(28, 1, 8));
        CreateCrate(new Vector3(32, 1, 6));
        CreateCrate(new Vector3(30, 3, 5));

        // West side
        CreateCrate(new Vector3(-30, 1, 10));
        CreateCrate(new Vector3(-28, 1, 12));
        CreateCrate(new Vector3(-32, 1, 8));

        // Extra scattered singles
        CreateCrate(new Vector3(5, 1, -10));
        CreateCrate(new Vector3(-5, 1, 15));
        CreateCrate(new Vector3(15, 1, -5));
        CreateCrate(new Vector3(-20, 1, 5));
    }


    void CreateCrate(Vector3 pos)
    {
        GameObject crate = GameObject.CreatePrimitive(PrimitiveType.Cube);
        crate.transform.position = pos;
        crate.transform.localScale = new Vector3(2, 2, 2);

        if (crateMat != null)
            crate.GetComponent<MeshRenderer>().material = crateMat;
    }
}
