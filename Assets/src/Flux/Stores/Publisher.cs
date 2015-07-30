namespace Assets.Flux.Stores {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Publisher<TTopic> {
        public delegate void Message(TTopic Input);

        internal Dictionary<Guid, Message> Messages = new Dictionary<Guid, Message>();

        public void Subscribe(Guid guid, Message msg) {
            //Logger.Log(guid.ToString());
            //Logger.Log(msg.Method.Name);
            Messages[guid] = msg;
        }

        public void Unsubscribe(Guid guid) {
            Messages.Remove(guid);
        }

        internal virtual void Publish() {
            Messages.Values.ToList().ForEach(SendMessage);
        }

        internal abstract void SendMessage(Message msg);
    }
}