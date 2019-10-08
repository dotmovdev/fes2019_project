using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SignExtensions;
using System.Linq;

public interface ISignCallback
{   
    void OnReceived(string id, Sign sign);
}

public class SignStorage : MonoBehaviour
{
    private Dictionary<string, Sign> signStorage = new Dictionary<string, Sign>();

    private ISignCallback callback = null;

    private List<string> requestBuffer = new List<string>();

    public void SetOnReleaceListener(ISignCallback callback)
    {
        this.callback = callback;
    }

    public void Releace(string id)
    {
        if (!signStorage.ContainsKey(id)) return;

        requestBuffer.Add(id);
    }

    public void Store(string id, Sign sign)
    {
        if (signStorage.ContainsKey(id))
        {
            signStorage[id] = sign;
        } else
        {
            signStorage.Add(id, sign);
        }
    }

    public Sign GetSign(string id)
    {
        return signStorage[id];
    }

    public void Update()
    {
        if (requestBuffer.Count > 0 && callback != null)
        {
            foreach(string id in requestBuffer)
            {
                Sign sign = signStorage[id];

                // センタリングする
                float minX = sign.starPositions.Min(value => value.x);
                float maxX = sign.starPositions.Max(value => value.x);
                float minY = sign.starPositions.Min(value => value.y);
                float maxY = sign.starPositions.Max(value => value.y);

                Vector3 center = new Vector3(
                    (minX + maxX) / 2,
                    (minY + maxY) / 2,
                    0);

                for (int i = 0; i < sign.starPositions.Length; i++)
                {
                    sign.starPositions[i] -= center;
                }

                callback.OnReceived(id, sign);
                signStorage.Remove(id);
            }
            requestBuffer.Clear();
        }
    }
}
