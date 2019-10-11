using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace SignExtensions
{
    public struct ColorSet
    {
        public Color starColor;
        public Color lineColor;

        public ColorSet (Color starColor, Color lineColor)
        {
            this.starColor = starColor;
            this.lineColor = lineColor;
        }
}

    public struct SignColor
    {
        public static readonly Color[] StarColor = new Color[6]
        {
            new Color(150f / 255f, 201f / 255f,  98f / 255f),
            new Color(  0f / 255f, 205f / 255f, 255f / 255f),
            new Color(240f / 255f, 135f / 255f,  33f / 255f),
            new Color(209f / 255f, 118f / 255f, 172f / 255f),
            new Color(255f / 255f, 241f / 255f,   0f / 255f),
            new Color(255f / 255f, 241f / 255f,   0f / 255f),
        };

        public static readonly Color[] LineColor = new Color[6]
        {
            new Color(255f / 255f, 255f / 255f,  73f / 255f),
            new Color(255f / 255f, 255f / 255f,  73f / 255f),
            new Color(255f / 255f, 255f / 255f,  73f / 255f),
            new Color(255f / 255f, 255f / 255f,  73f / 255f),
            new Color(223f / 255f, 153f / 255f,  31f / 255f),
            new Color( 29f / 255f, 219f / 255f, 255f / 255f),
        };

        public ColorSet GetColorSet(int index)
        {
            return new ColorSet(StarColor[index], LineColor[index]);
        }
    } 

    public struct Line
    {
        internal int startIndex;
        internal int endIndex;
    }

    public class Lines
    {
        private List<int[]> lines = new List<int[]>();

        public List<int[]> Value
        {
            get
            {
                return lines;
            }
        }

        public int Count
        {
            get
            {
                return lines.Count;
            }
        }

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
            lines.Add(new int[] { startIndex, endIndex });
            return true;
        }

        public bool Add(Line line)
        {
            return Add(line.startIndex, line.endIndex);
        }

        public void Remove(int index)
        {
            lines.RemoveAt(index);
        }

        public void Clear()
        {
            lines.Clear();
        }

        public int[] GetLineIndices()
        {
            int[] indices = new int[lines.Count * 2];
            for (int i = 0; i < indices.Length / 2; i++)
            {
                indices[2 * i] = lines[i][0];
                indices[2 * i + 1] = lines[i][1];
            }
            return indices;
        }

        public int[] GetLastLineIndices()
        {
            int[] lastIndices = new int[2];
            lastIndices[0] = lines[lines.Count - 1][0];
            lastIndices[1] = lines[lines.Count - 1][1];
            return lastIndices;
        }

        public Line GetLastLine()
        {
            Line lastIndices = new Line();
            lastIndices.startIndex = lines[lines.Count - 1][0];
            lastIndices.endIndex = lines[lines.Count - 1][1];
            return lastIndices;
        }
    }

    public struct Sign
    {
        internal Vector3[] starPositions;
        internal Line[] lines;
        internal int colorIndex;

        public SimpleSign ToSimpleSign()
        {
            SimpleSign simpleSign = new SimpleSign();
            int starLength = starPositions.Length * 3;
            simpleSign.starPositions = new float[starLength];
            for (int i = 0; i < starPositions.Length; i++)
            {
                simpleSign.starPositions[i * 3] = starPositions[i].x;
                simpleSign.starPositions[i * 3 + 1] = starPositions[i].y;
                simpleSign.starPositions[i * 3 + 2] = starPositions[i].z;
            }

            int lineLength = lines.Length * 2;
            simpleSign.lines = new int[lineLength];
            for (int i = 0; i < lines.Length; i++)
            {
                simpleSign.lines[i * 2] = lines[i].startIndex;
                simpleSign.lines[i * 2 + 1] = lines[i].endIndex;
            }

            simpleSign.colorIndex = colorIndex;

            return simpleSign;
        }
    }

    [Serializable]
    public struct SimpleSign
    {
        public float[] starPositions;
        public int[] lines;
        public int colorIndex;

        public Sign ToSign()
        {
            Sign sign = new Sign();
            int starNum = starPositions.Length / 3;
            sign.starPositions = new Vector3[starNum];
            for (int i = 0; i < starNum; i++)
            {
                sign.starPositions[i] = new Vector3(
                    starPositions[i * 3 + 0],
                    starPositions[i * 3 + 1],
                    starPositions[i * 3 + 2]
                    );
            }

            int lineNum = lines.Length / 2;
            sign.lines = new Line[lineNum];
            for (int i = 0; i < lineNum; i++)
            {
                sign.lines[i] = new Line();
                sign.lines[i].startIndex = lines[i * 2 + 0];
                sign.lines[i].endIndex = lines[i * 2 + 1];
            }

            sign.colorIndex = colorIndex;

            return sign;
        }
    }

    public struct Controll
    {
        internal int lastTargetIndex;
        internal Vector3 position;
    }
}