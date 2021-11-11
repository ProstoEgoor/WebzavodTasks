using System;
using System.Collections.Generic;
using System.Text;

namespace Task_6._2._1 {
    class Tank {
        public int MaxVolume { get; }

        int currentVolume;
        public int CurrentVolume {
            get => currentVolume;
            set {
                if (value > MaxVolume) {
                    throw new TankOverflowException(value - CurrentVolume, value - MaxVolume);
                }

                if (value < 0) {
                    throw new NotEnoughException(CurrentVolume - value, -value);
                }

                currentVolume = value;
            }
        }

        public Tank(int maxVolume) {
            if (maxVolume <= 0) {
                throw new ArgumentOutOfRangeException("Объем цистерны должен быть больше нуля.");
            }
            MaxVolume = maxVolume;
        }

        public void Add(int liquidVolume) {
            if (liquidVolume <= 0) {
                throw new ArgumentOutOfRangeException("Объем должен быть больше нуля.");
            }
            CurrentVolume += liquidVolume;
        }

        public void Take(int liquidVolume) {
            if (liquidVolume <= 0) {
                throw new ArgumentOutOfRangeException("Объем должен быть больше нуля.");
            }
            CurrentVolume -= liquidVolume;
        }

        public override string ToString() {
            return $"Цистерна заполнена на {CurrentVolume} л из {MaxVolume} л.";
        }
    }

    class TankOverflowException : Exception {
        public int AddedVolume { get; }
        public int ExcessVolume { get; }
        public TankOverflowException(int addedVolume, int excessVolume) : base($"В цистерне не хватает места. {excessVolume} л. не помещаются.") {
            AddedVolume = addedVolume;
            ExcessVolume = excessVolume;
        }
    }
    class NotEnoughException : Exception {
        public int TakedVolume { get; }
        public int LackOfVolume { get; }
        public NotEnoughException(int takedVolume, int lackOfVolume) : base($"В цистерне недостаточно жидкости. {lackOfVolume} л. не хватает.") {
            TakedVolume = takedVolume;
            LackOfVolume = lackOfVolume;
        }
    }
}
