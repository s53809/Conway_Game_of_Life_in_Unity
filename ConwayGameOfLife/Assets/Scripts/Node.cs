using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Int32 k;
    public Node nw, ne, sw, se;
    public Int64 n;
    
    public Node(Int32 k, Node nw, Node ne, Node sw, Node se, Int64 n)
    {
        this.k = k;
        this.nw = nw;
        this.ne = ne;
        this.sw = sw;
        this.se = se;
        this.n = n;
    }

    public override int GetHashCode()
    {
        if (k == 0) return (Int32)0;
        else return nw.GetHashCode() +
                11 * ne.GetHashCode() +
                101 * sw.GetHashCode() +
                1007 * se.GetHashCode();
    }
}
