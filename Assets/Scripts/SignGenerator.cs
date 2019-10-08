using System.Collections;
using System.Collections.Generic;
using SignExtensions;
using UnityEngine;

public class SignGenerator : MonoBehaviour, ISignCallback {
    [SerializeField]
    private GameObject star;
    [SerializeField]
    private SignStorage storage;
    private SimpleShape lineDrawer;

    private SignGenerator instance;

    public static SignGenerator Instance {
        get {
            return Instance;
        }
    }

    void Awake () {
        if (instance == null) {

            instance = this;
            DontDestroyOnLoad (gameObject);
        } else {

            Destroy (gameObject);
        }
    }

    void Start () {
        storage.SetOnReleaceListener(this);
    }

    void Update () {

    }

    public void OnReceived(string id, Sign sign)
    {
        var gameObject = GenerateSign(sign, transform);
        gameObject.name = id;
    }

    public GameObject GenerateSign (Sign sign, Vector3 offset, float scale, Transform parent) {
        GameObject signRoot = new GameObject ();
        signRoot.transform.parent = parent;

        for (int i = 0; i < sign.starPositions.Length; i++) {
            GameObject starGameObject = Instantiate (star, signRoot.transform);
            starGameObject.transform.position = sign.starPositions[i] * scale + offset;
            starGameObject.transform.parent = signRoot.transform;
        }

        SimpleShape drawer = new SimpleShape (signRoot);
        for (int i = 0; i < sign.lines.Length; i++) {
            int startIndex = sign.lines[i].startIndex;
            int endIndex = sign.lines[i].endIndex;
            drawer.AddLine (sign.starPositions[startIndex] * scale + offset,
                sign.starPositions[endIndex] * scale + offset, 0.2f);
        }

        return signRoot;
    }

    public GameObject GenerateSign (Sign sign, Vector3 offset, Transform parent) {
        return GenerateSign (sign, offset, 1f, transform);
    }

    public GameObject GenerateSign (Sign sign, float scale, Transform parent) {
        return GenerateSign (sign, Vector3.zero, scale, transform);
    }

    public GameObject GenerateSign (Sign sign, Transform parent) {
        return GenerateSign (sign, Vector3.zero, 1f, transform);
    }
}