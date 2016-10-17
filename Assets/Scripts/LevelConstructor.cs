using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;


public class LevelConstructor : MonoBehaviour {


    Block[,] Blocks;
    public int Gridsize;
    public int lapLength;
    public int lapLengthMin;
    public bool established;
    public float gridScale;
    System.Random random = new System.Random();

    Vec2i startPoint;
    Vec2i endPoint;

    List<Vec2i> dirs;
    List<Vector3> splinePoints;
    Vec2i upV;
    Vec2i downV;
    Vec2i leftV;
    Vec2i rightV;

    List<Vec2i> potDirs;

    private GameObject s;


    GameObject line;
    GameObject turnRight;
    GameObject turnLeft;
    Dictionary<String, GameObject> chunks;
    Dictionary<String, float> chunksRot;

    // Use this for initialization

    void Awake() {
        established = false;
        Blocks = new Block[Gridsize, Gridsize];

        for (int i = 0; i < Gridsize; i++) {
            for (int j = 0; j < Gridsize; j++) {
                Blocks[i, j] = new Block(i, j, "null", "null");
            }
        }
        upV = new Vec2i(0, 1);
        downV = new Vec2i(0, -1);
        leftV = new Vec2i(-1, 0);
        rightV = new Vec2i(1, 0);
        dirs = new List<Vec2i>() { upV, downV, leftV, rightV };
        chunks = new Dictionary<String, GameObject>();
        chunksRot = new Dictionary<String, float>();


        line = (Resources.Load("line")) as GameObject;
        turnLeft = (Resources.Load("turnLeft")) as GameObject;
        turnRight = (Resources.Load("turnRight")) as GameObject;

        GameObject downup = line;
        chunks.Add("downup", downup);
        chunksRot.Add("downup", 0);

        GameObject rightleft = line;
        //rightleft.transform.Rotate(0, -90, 0);
        chunks.Add("rightleft", rightleft);
        chunksRot.Add("rightleft", -90);

        GameObject leftright = line;
        //leftright.transform.Rotate(0, 90, 0);
        chunks.Add("leftright", leftright);
        chunksRot.Add("leftright", 90);

        GameObject updown = line;
        //updown.transform.Rotate(0, 180, 0);
        chunks.Add("updown", updown);
        chunksRot.Add("updown", 180);

        GameObject downright = turnRight;
        //downright.transform.Rotate(0, 0, 0);
        chunks.Add("downright", downright);
        chunksRot.Add("downright", 0);

        GameObject rightup = turnRight;
        //rightup.transform.Rotate(0, -90, 0);
        chunks.Add("rightup", rightup);
        chunksRot.Add("rightup", -90);

        GameObject upleft = turnRight;
        //upleft.transform.Rotate(0, 180, 0);
        chunks.Add("upleft", upleft);
        chunksRot.Add("upleft", 180);

        GameObject leftdown = turnRight;
        //leftdown.transform.Rotate(0, 90, 0);
        chunks.Add("leftdown", leftdown);
        chunksRot.Add("leftdown", 90);

        GameObject downleft = turnLeft;
        //downleft.transform.Rotate(0, 0, 0);
        chunks.Add("downleft", downleft);
        chunksRot.Add("downleft", 0);

        GameObject leftup = turnLeft;
        //leftup.transform.Rotate(0, 90, 0);
        chunks.Add("leftup", leftup);
        chunksRot.Add("leftup", 90);

        GameObject upright = turnLeft;
        //upright.transform.Rotate(0, 180, 0);
        chunks.Add("upright", upright);
        chunksRot.Add("upright", 180);

        GameObject rightdown = turnLeft;
        //rightdown.transform.Rotate(0, -90, 0);
        chunks.Add("rightdown", rightdown);
        chunksRot.Add("rightdown", -90);

    }


    void Start() {

        makeLevel();
        createBlocks();
    }

    void makeLevel() {
        splinePoints = new List<Vector3>();
        foreach (Transform child in this.transform) {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < Gridsize; i++) {
            for (int j = 0; j < Gridsize; j++) {
                Blocks[i, j] = new Block(i, j, "null", "null");
            }
        }
        startPoint = new Vec2i(3, 3);
        splinePoints.Add(new Vector3(startPoint.x * gridScale, 0, startPoint.y * gridScale));
        setBlock(startPoint, "down", "up");
        endPoint = startPoint - upV;
        lapLength = 0;
        nextBlock(startPoint + upV, getOppDir("up"));
        if (lapLength < lapLengthMin) {
            makeLevel();
            return;
        }

    }

    void createBlocks() {
        splinePoints.Add(new Vector3(startPoint.x * gridScale, 0, startPoint.y * gridScale));

        foreach(Vector3 v in splinePoints){
            //Debug.Log(v.x + " " + v.z);
        }
        for (int i = 0; i < Gridsize; i++) {
            for (int j = 0; j < Gridsize; j++) {
                Vec2i p = new Vec2i(i, j);
                if (p.Equals(startPoint)) {
                    GameObject s = Instantiate(Resources.Load("startCube")) as GameObject;
                    s.transform.SetParent(this.transform);
                    //s.transform.localScale = new Vector3(10, 10, 10);
                    s.transform.position = new Vector3(i * gridScale, 0, j * gridScale);
                }
                else if (getBlock(p).Entry != "null") {
                    //Debug.Log(getBlock(p).Entry + getBlock(p).Exit);
                    s = Instantiate(chunks[getBlock(p).Entry + getBlock(p).Exit]);
                    s.GetComponent<BlockObj>().set(getBlock(p).Entry + getBlock(p).Exit);
                    s.transform.eulerAngles = new Vector3(0, chunksRot[getBlock(p).Entry + getBlock(p).Exit], 0);
                    //s.transform.localScale = new Vector3(10, 10, 10);
                    s.transform.position = new Vector3(i * gridScale, 0, j * gridScale);
                    s.transform.SetParent(this.transform);
                }
            }
        }
    }

    Block getBlock(Vec2i p) {
        return Blocks[p.x, p.y];
    }
    void setBlock(Vec2i p, String en, String ex) {
        //Debug.Log(p.x + " " + p.y);
        Blocks[p.x, p.y] = new Block(p.x, p.y, en, ex);
    }

    String getDir(Vec2i p) {
        if (p.x == 1)
            return "right";
        if (p.x == -1)
            return "left";
        if (p.y == 1)
            return "up";
        if (p.y == -1)
            return "down";

        return "";
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Start();
        }
    }

    void nextBlock(Vec2i curPos, String entry) {
        lapLength++;
        if (curPos.Equals(endPoint)) {
            setBlock(curPos, entry, getOppDir(getBlock(startPoint).Entry));
            splinePoints.Add(new Vector3(curPos.x * gridScale, 0, curPos.y * gridScale));
            return;
        }
        potDirs = getPotDirs(curPos);
        if (potDirs.Count == 0) {
            makeLevel();
            return;
        }
        splinePoints.Add(new Vector3(curPos.x * gridScale, 0, curPos.y * gridScale));
        int r = random.Next(potDirs.Count);
        setBlock(curPos, entry, getDir(potDirs[r]));

        nextBlock(curPos + potDirs[r], getOppDir(getDir(potDirs[r])));
    }

    public List<Vec2i> getPotDirs(Vec2i curPos) {
        List<Vec2i> tempD = new List<Vec2i>();
        foreach (Vec2i d in dirs) {
            Vec2i potPos = curPos + d;
            if (potPos.x < 0 || potPos.y < 0 || potPos.x >= Gridsize || potPos.y >= Gridsize) {
                continue;
            }
            if (getBlock(potPos).Entry != "null") {
                continue;
            }
            if (potPos.Equals(endPoint)) {
                tempD = new List<Vec2i>() { d };
                break;
            }
            tempD.Add(d);
        }
        return tempD;
    }

    String getOppDir(String dir) {
        if (dir == "up")
            return "down";
        else if (dir == "down")
            return "up";
        else if (dir == "right")
            return "left";
        else if (dir == "left")
            return "right";

        return "";
    }

}
