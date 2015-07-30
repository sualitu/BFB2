namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;

    using UnityEngine;

    public class CardPutDownAction : CardAction {
        public CardPutDownAction(Guid id, Vector3 position, bool leftClick)
            : base(id) {
            LeftClick = leftClick;
            Position = position;
        }

        public bool LeftClick { get; private set; }
        public Vector3 Position { get; private set; }
    }
}