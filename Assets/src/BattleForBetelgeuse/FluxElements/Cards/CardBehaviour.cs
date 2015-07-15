namespace Assets.BattleForBetelgeuse.FluxElements.Cards {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Assets.BattleForBetelgeuse.Animations;
    using Assets.BattleForBetelgeuse.Animations.GUI;
    using Assets.BattleForBetelgeuse.Animations.TweenInteraction;
    using Assets.BattleForBetelgeuse.Cards;
    using Assets.BattleForBetelgeuse.FluxElements.Player;
    using Assets.BattleForBetelgeuse.Management;
    using Assets.Flux.Views;
    using Assets.Utilities;

    using UnityEngine;

    public class CardBehaviour : ViewBehaviour<CardView>, ITweenableGuiComponent {
        private UITexture backTexture;

        private UITexture frontTexture;

        private Holographs holo;

        private CardStatus lastStatus;

        private UILabel nameLabel;

        private int pauseCounter;

        public int Id { get; set; }

        public static string i;

        public static string j;
        public override void PushUpdate() {

            i = lastStatus.ToString();
            j = Companion.Status.ToString();
            if (Companion.Status != lastStatus)
            {
                lastStatus = Companion.Status;

                UpdateStatus();

                if (!Companion.IsHovered) {
                    OnHover(false);
                }
            }
            UpdateLocation();
        }

        private void UpdateStatus()
        {
            switch (Companion.Status) {
                case CardStatus.Creating:
                    AnimateCreation();
                    break;
                case CardStatus.JustCreated:
                    AnimateFromCreationToHand();
                    break;
                case CardStatus.Played:
                    PlayCard();
                    break;
            }
        }

        private void AnimateFromCreationToHand() {
            var newLocation = HandManager.Instance.GetTransformOfCard(Id);

            TweenPositionTo(newLocation.First, Settings.Animations.Cards.CardMovementSlow);

            TweenRotationTo(newLocation.Second, Settings.Animations.Cards.CardMovementSlow);

            // Custom tween for scaling when moving to hand.
            var turningPoint = new Vector3(newLocation.Third.x - .2f,
                                           newLocation.Third.y - .2f,
                                           newLocation.Third.z - .2f);
            var tween = gameObject.GetComponent<TweenScale>() ?? gameObject.AddComponent<TweenScale>();
            tween.method = UITweener.Method.Linear;
            tween.from = transform.localScale;
            tween.to = turningPoint;
            tween.duration = Settings.Animations.Cards.CardMovementSlow / 2;
            tween.delay = 0f;
            EventDelegate.Callback callback = delegate { FlyBackInOnCreation(tween, turningPoint, newLocation.Third); };
            var callbackEvent = new EventDelegate(callback) { oneShot = true };
            tween.onFinished.Add(callbackEvent);
            tween.ResetToBeginning();
            tween.PlayForward();
            UpdateLayer();
        }

        public void FlyBackInOnCreation(TweenScale tween, Vector3 turningPoint, Vector3 target) {
            tween.method = UITweener.Method.EaseOut;
            tween.from = turningPoint;
            tween.to = target;
            tween.duration = Settings.Animations.Cards.CardMovementSlow / 2;
            tween.delay = 0f;

            EventDelegate.Callback callback = delegate { new CardToHandAction(Id); };
            var callbackEvent = new EventDelegate(callback) { oneShot = true };
            tween.onFinished.Add(callbackEvent);
            tween.ResetToBeginning();
            tween.PlayForward();
        }

        public void UpdateLayer(int baseLayer) {
            nameLabel.depth = baseLayer;
            frontTexture.depth = baseLayer - 1;
            backTexture.depth = baseLayer - 2;
            holo.hologramPlane1.GetComponent<UITexture>().depth = baseLayer - 3;
            holo.hologramPlane2.GetComponent<UITexture>().depth = baseLayer - 3;
        }

        public void UpdateLayer() {
            var handIndex = HandManager.Instance.Hand.IndexOf(Id);
            UpdateLayer(handIndex * -4);
        }

        private void UpdateLocation() {
            if (Companion.PositionChange.First) {
                TweenPositionTo(Companion.PositionChange.Second);
            }

            if (Companion.RotationChange.First) {
                TweenRotationTo(Companion.RotationChange.Second);
            }

            if (Companion.ScaleChange.First) {
                TweenScaleTo(Companion.ScaleChange.Second);
            }
        }

        private void TweenScaleTo(Vector3 target, float duration = Settings.Animations.Cards.CardMovementDuration) {
            Movement.Gui.ScaleTo(gameObject.transform.localScale, target, this, duration);
        }

        private void TweenRotationTo(Vector3 target, float duration = Settings.Animations.Cards.CardMovementDuration) {
            Movement.Gui.RotateTo(gameObject.transform.localRotation.eulerAngles, target, this, duration);
        }

        private void TweenPositionTo(Vector3 target, float duration = Settings.Animations.Cards.CardMovementDuration) {
            Movement.Gui.MoveTo(gameObject.transform.localPosition, target, this, duration);
        }

        private void TweenAlphaTo(UIRect target, float alpha) {
            var tweenAlpha = target.gameObject.GetComponent<TweenAlpha>()
                             ?? target.gameObject.AddComponent<TweenAlpha>();
            tweenAlpha.from = target.alpha;
            tweenAlpha.to = alpha;
            tweenAlpha.duration = Settings.Animations.Cards.CardMovementDuration;
            tweenAlpha.method = UITweener.Method.Linear;
            tweenAlpha.ResetToBeginning();
            tweenAlpha.PlayForward();
        }

        private void TweenAlphaOfTextures(float alpha) {
            TweenAlphaTo(frontTexture, alpha);
            TweenAlphaTo(backTexture, 0f);
        }

        private void Awake() {
            BehaviourManager.Behaviours.Add(this);
            nameLabel = GetComponentInChildren<UILabel>();
            frontTexture = GetComponentsInChildren<UITexture>().First(component => component.tag == "CardFront");
            backTexture = GetComponentsInChildren<UITexture>().First(component => component.tag == "CardBack");
            holo = GetComponentInChildren<Holographs>();
        }

        public void AnimateCreation() {
            transform.localPosition = Settings.Animations.Cards.CardSpawnPosition;
            holo.FadeInCallBack = FadeIn;
            holo.enabled = true;
        }

        public void FadeIn() {
            var tweens = new List<TweenAlpha> {
                nameLabel.gameObject.AddComponent<TweenAlpha>(),
                frontTexture.gameObject.AddComponent<TweenAlpha>(),
                backTexture.gameObject.AddComponent<TweenAlpha>()
            };
            EventDelegate.Callback callback = delegate { new CardCreatedAction(Id); };

            tweens[0].onFinished.Add(new EventDelegate(callback) { oneShot = true });
            foreach (var tween in tweens) {
                tween.from = 0f;
                tween.to = 1f;
                tween.ResetToBeginning();
                tween.PlayForward();
            }
        }

        private void Start() {
            transform.localScale = new Vector3(Settings.Animations.Cards.CardSize,
                                               Settings.Animations.Cards.CardSize,
                                               Settings.Animations.Cards.CardSize);
        }

        public void SetCard(Card card) {
            var texture = CardFactory.GetTexture(card);
            texture.Apply();
            frontTexture.mainTexture = texture;
            nameLabel.text = card.Name;
        }

        public void SetHoloTexture() {
            holo.hologramPlane1.GetComponent<UITexture>().mainTexture = frontTexture.mainTexture;
            holo.hologramPlane2.GetComponent<UITexture>().mainTexture = frontTexture.mainTexture;
        }

        private void OnHover(bool isHovedered) {
            if (Companion.InHand && !Companion.PickedUp && !Companion.Played) {
                if (isHovedered) {
                    SetHoloTexture();
                    TweenAlphaOfTextures(.9f);
                    holo.FadeIn(.5f);
                    new CardHovederedAction(Id);
                } else {
                    TweenAlphaOfTextures(1f);
                    holo.FadeOut();
                    new CardUnhovederedAction(Id);
                }
            }
        }

        private void OnClick() {
            if (Companion.InHand) {
                if (!Companion.PickedUp) {
                    PickUp();
                }
            }
        }

        private void PutDown(bool leftClick = false) {
            PauseInteraction();
            OnHover(false);
            new CardPutDownAction(Id, Input.mousePosition, leftClick);
        }

        private void PickUp() {
            PauseInteraction(2);
            new CardPickedUpdAction(Id);
        }

        private void OnDragStart() {
            if (!Companion.PickedUp) {
                PickUp();
            }
        }

        private void OnDragEnd() {
            PutDown(true);
        }

        private void PauseInteraction(int i = 12) {
            pauseCounter = i;
        }

        private void Update() {
            if (pauseCounter <= 0) {
                if (Companion.PickedUp) {
                    TweenCardToMousePosition();
                    if (Input.GetMouseButtonUp(0)) {
                        PutDown(true);
                    } else if (Input.GetMouseButtonUp(1)) {
                        PutDown();
                    }
                }
                if (Companion.Played) {
                    if (Input.GetMouseButtonUp(1)) {
                        Unplay();
                    }
                }
            } else {
                pauseCounter--;
            }
        }

        private void Unplay() {
            PauseInteraction();
            TweenAlphaOfTextures(1f);
            holo.FadeOut(.5f);
            UpdateLocation();
            new CardToHandAction(Id);
            OnHover(false);
        }

        private void PlayCard() {
            PauseInteraction();
            TweenAlphaOfTextures(.5f);
            holo.FadeIn(.5f);
            OnHover(false);
        }

        private void TweenCardToMousePosition() {
            var dragPosition = Input.mousePosition + new Vector3(-Screen.width / 2, -Screen.height / 2, 1f);
            var deltaPosition = transform.localPosition - dragPosition;
            var y = deltaPosition.x / 3;
            var x = (dragPosition - transform.localPosition).y / 3;
            TweenRotationTo(new Vector3(x, y, 0f), Settings.Animations.Cards.CardMovementFast);
            TweenPositionTo(dragPosition, Settings.Animations.Cards.CardMovementFast);
        }
    }
}