using System.Collections.Generic;
using System;
using UnityEngine;

namespace BattleForBetelgeuse.Publishing {

  public abstract class Publisher<Topic> {
    public delegate void Message(Topic Input);

    internal List<Message> messages = new List<Message>();

    public void Subscribe(Message msg) {
      messages.Add(msg);
    }

    internal virtual void Publish() {
      messages.ForEach(msg => SendMessage(msg));
    }
    
    internal abstract void SendMessage(Message msg);
  }
}

