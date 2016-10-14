using UnityEngine;
using System.Collections;
using System;

public struct Block {
    public int x;
    public int y;
    public String Entry;
    public String Exit;

    public Block(int x, int y, String en, String ex) {
        this.x = x;
        this.y = y;
        this.Entry = en;
        this.Exit = ex;
    }

    public override int GetHashCode() {
        unchecked {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }

}
