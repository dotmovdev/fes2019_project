using System.Collections;
using UnityEngine;
using WebSocketExtensions;
using WebSocketSharp.Server;
using SignExtensions;

public class Server : MonoBehaviour, ISign {
    [SerializeField]
    private int port = 8080;

    [SerializeField]
    private SignStorage signStorage;

    private WebSocketServer server;

    private Server instance;

    public static Server Instance
    {
        get {
            return Instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }

    void Start () {
        server = new WebSocketServer (port);
        server.AddWebSocketService<SignServer> ("/", () => new SignServer (this));
        server.Start ();
    }

    void Update () {

    }

    public void OnTouched(string id)
    {

    }
    
    public void OnReleased(string id)
    {

    }

    public void OnMoved(string id, Vector3 position, int lastTargetIndex) {
        
    }

    public void OnLined(string id, Sign sign) {
        signStorage.Store(id, sign);
    }

    public void OnCompleted(string id) {
        signStorage.Releace(id);
    }

    void OnDestroy()
    {
        if (server != null)
        {
            server.Stop();
        server = null;
        }
    }

    void UploadToWeb(string url, Sign sign)
    {

    }
}