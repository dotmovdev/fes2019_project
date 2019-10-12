using System;
using UnityEngine;
using WebSocketSharp;
using WebSocketSharp.Server;
using SignExtensions;

namespace WebSocketExtensions {
    public enum Event {
        EVENT_INIT,
        EVENT_SIGN, // 星座完成の信号を送る時
        EVENT_TOUCHED, // タッチした時
        EVENT_MOVED, // スワイプがなされたとき
        EVENT_LINED, // 線を引かれたとき
        EVENT_RELEASED, // タッチが離れたとき
    }

    public interface IWebsocket
    {
        void OnMessage(string data);
    }

    public interface ISign
    {
        void OnTouched(string id);
        void OnReleased(string id);
        void OnMoved(string id, Vector3 position, int lastTargetIndex);
        void OnLined(string id, Sign sign);
        void OnCompleted(string id);
    }

    public class SignServer : WebSocketBehavior {
        private ISign callback = null;

        public SignServer () : this (null) {

        }

        public SignServer (ISign callback) {
            this.callback = callback;
        }

        protected override void OnMessage (MessageEventArgs e) {
            Debug.Log(e.Data);
            if (callback != null) {
                string[] splitted = e.Data.Split('|');

                int ev = int.Parse(splitted[0]);

                switch (ev)
                {
                    case (int)Event.EVENT_INIT:
                        string newId = GetUID();
                        string text = ((int)Event.EVENT_INIT).ToString() + "|" + newId;
                        Send(text);
                        return;
                }

                string id = splitted[1];
                string data = splitted[2];

                switch (ev) {
                    case (int)Event.EVENT_SIGN:
                        callback.OnCompleted(id);
                        break;
                    case (int)Event.EVENT_LINED:
                        SimpleSign sign = JsonUtility.FromJson<SimpleSign>(data);
                        callback.OnLined(id, sign.ToSign());
                        break;
                    case (int)Event.EVENT_MOVED:
                        Controll controll = JsonUtility.FromJson<Controll>(data);
                        callback.OnMoved(id, controll.position, controll.lastTargetIndex);
                        break;
                    case (int)Event.EVENT_TOUCHED:
                        callback.OnTouched(id);
                        break;
                    case (int)Event.EVENT_RELEASED:
                        callback.OnReleased(id);
                        break;
                }
            }
        }

        private string GetUID()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmffff");
        }
    }
}