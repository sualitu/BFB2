namespace Assets.Flux.Stores {
    using System.Collections.Generic;

    public abstract class Publisher<TTopic> {
        public delegate void Message(TTopic Input);

        internal List<Message> messages = new List<Message>();

        public void Subscribe(Message msg) {
            messages.Add(msg);
        }

        internal virtual void Publish() {
            messages.ForEach(SendMessage);
        }

        internal abstract void SendMessage(Message msg);
    }
}