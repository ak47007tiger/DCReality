using UnityEngine;
using System.Collections;

namespace DC
{
    public delegate R Convert<R>();

    public delegate R Convert<R, P>(P p);

    public delegate R Convert<R, P1, P2>(P1 p1, P2 p2);

    public delegate R Convert<R, P1, P2, P3>(P1 p1, P2 p2, P3 p3);

    public delegate R Convert<R, P1, P2, P3, P4>(P1 p1, P2 p2, P3 p3, P4 p4);

    public delegate R Convert<R, P1, P2, P3, P4, P5>(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5);
}
