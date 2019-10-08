using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SignExtensions
{
    public struct Line
    {
        internal int startIndex;
        internal int endIndex;
    }

    public class Lines
    {
        public List<int[]> lines = new List<int[]>();

        public bool Add(int startIndex, int endIndex)
        {
            // 番号が同じときは追加しない
            if (startIndex == endIndex) return false;
            // 同じ組み合わせがすでにあるときは追加しない
            List<int[]> results = lines.FindAll(x => Array.IndexOf(x, startIndex) >= 0);
            foreach (var result in results)
            {
                if (startIndex + endIndex == result.Sum()) return false;
            }

            // linesに追加する
            lines.Add(new int[] {startIndex, endIndex});
            return true;
        }
        
        public int[] GetLineIndices()
        {
            int[] indices = new int[lines.Count * 2];
            for (int i = 0; i < indices.Length/2; i++)
            {
                indices[2 * i] = lines[i][0];
                indices[2 * i + 1] = lines[i][1];
            }
            return indices;
        }
    }

    public struct Sign
    {
        internal Vector3[] starPositions;
        internal Line[] lines;
    }

    [Serializable]
    public struct SimpleSign
    {
        public float[] starPositions;
        public int[] lines;
    }

    public struct Controll
    {
        internal int lastTargetIndex;
        internal Vector3 position;
    }
}