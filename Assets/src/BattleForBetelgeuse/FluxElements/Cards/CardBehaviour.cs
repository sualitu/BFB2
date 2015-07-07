namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;
    using System.Linq;

    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Views;
    using Assets.src.BattleForBetelgeuse.Animations.GUI;
    using Assets.Utilities;

    using UnityEngine;

    public class CardBehaviour : ViewBehaviour<CardView> {

        private UILabel nameLabel;

        private UITexture uiTexture;

        private Holographs holo;

        public override void PushUpdate() {
            throw new NotImplementedException();
        }

        private void Awake() {
            nameLabel = GetComponentInChildren<UILabel>();
            uiTexture = GetComponentsInChildren<UITexture>().First(component => component.tag == "CardFront");
            holo = GetComponentInChildren<Holographs>();
            holo.enabled = true;
        }

        private void Start() {
            transform.localScale = new Vector3(.75f, .75f, .75f);
        }

        public void SetCard(Card card) {
            var texture = CardFactory.GetTexture(card);
            var copyTexture = texture.Copy();
            copyTexture.Apply();
            copyTexture.name = string.Format("Texture for card {0}", card.Name);
            uiTexture.mainTexture = copyTexture;
            Destroy(texture);
            nameLabel.text = card.Name;
        }
    }
}