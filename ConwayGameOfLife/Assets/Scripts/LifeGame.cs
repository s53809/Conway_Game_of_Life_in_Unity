using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGame : MonoBehaviour {
    [SerializeField] private GameObject m_cell;
    [SerializeField] private Int64 m_size = 32;
    [SerializeField] private Single m_speed = 1f;
    [SerializeField] private GridRenderer m_grid;
    [SerializeField] private SquarePool m_pool;
    [SerializeField] private Single m_mouseSpeed = 1f;

    private Square[,] Cell;

    private Node on, off;

    private void Awake()
    {
        on = new Node(0, null, null, null, null, 1);
        off = new Node(0, null, null, null, null, 0);
    }

    private void OnValidate()
    {
        if(m_size >= 0 && m_grid != null)
        {
            m_grid.OnUpdate(m_size);
        }
        if(m_mouseSpeed >= 0 && m_pool != null)
        {
            m_pool.OnUpdate(m_mouseSpeed);
        }
    }

    private Node Join(Node a, Node b, Node c, Node d)
    { //#todo : Cache Memoriation
        Int64 n = a.n + b.n + c.n + d.n;
        return new Node(a.k + 1, a, b, c, d, n);
    }

    private Node GetZero(Int32 k)
    { //#todo : Cache Memoriation
        if (k == 0) return off;
        else return Join(GetZero(k - 1), GetZero(k - 1), GetZero(k - 1), GetZero(k - 1));
    }

    private Node Center(Node m)
    {
        Node z = GetZero(m.k - 1);
        return Join(
            Join(z, z, z, m.nw), Join(z, z, m.ne, z),
            Join(z, m.sw, z, z), Join(m.se, z, z, z)
            );
    }
    private Node Life(Node a, Node b, Node c, Node d, Node E, Node f, Node g, Node h, Node i)
    {
        Int64 sum = a.n + b.n + c.n + d.n + f.n + g.n + h.n + i.n;
        if (sum == 3 || (E.n == 1 && sum == 2)) return on;
        else return off;
    }

    private Node Life4x4(Node m)
    {
        Node nw = Life(m.nw.nw, m.nw.ne, m.ne.nw, m.nw.sw, m.nw.se, m.ne.sw, m.sw.nw, m.sw.ne, m.se.nw);
        Node ne = Life(m.nw.ne, m.ne.nw, m.ne.ne, m.nw.se, m.ne.sw, m.ne.se, m.sw.ne, m.se.nw, m.se.ne);
        Node sw = Life(m.nw.sw, m.nw.se, m.ne.sw, m.sw.nw, m.sw.ne, m.se.nw, m.sw.sw, m.sw.se, m.se.sw);
        Node se = Life(m.nw.se, m.ne.sw, m.ne.se, m.sw.ne, m.se.nw, m.se.ne, m.sw.se, m.se.sw, m.se.se);
        return Join(nw, ne, sw, se);
    }

    private Node NextGene(Node m, Int32 j = 0)
    {
        Node s = Life4x4(m);
        if (m.n == 0) return m.nw;
        else if (m.k == 2) s = Life4x4(m);
        else
        {
            if (j == 0) j = m.k - 2;
            else j = Math.Min(j, m.k - 2);
            Node c1 = NextGene(Join(m.nw.nw, m.nw.ne, m.nw.sw, m.nw.se));
            Node c2 = NextGene(Join(m.nw.ne, m.ne.nw, m.nw.se, m.ne.sw));
            Node c3 = NextGene(Join(m.ne.nw, m.ne.ne, m.ne.sw, m.ne.se));
            Node c4 = NextGene(Join(m.nw.sw, m.nw.se, m.sw.nw, m.sw.ne));

        }
        return s;
    }
}
