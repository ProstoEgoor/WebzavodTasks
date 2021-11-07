using System;
using System.Collections.Generic;
using System.Text;

namespace Task_6._2._1 {
    class Tank {
        public uint MaxVolume { get; }

        int currentVolume;
        public int CurrentVolume {
            get => currentVolume;
            set {
                if (value > MaxVolume) {
                    throw new TankOverflowException();
                }

                if (value < 0) {
                    throw new NotEnoughException();
                }

                currentVolume = value;
            }
        }

        public Tank(uint maxVolume) {
            if (maxVolume == 0) {
                throw new ArgumentOutOfRangeException("Объем цистерны должен быть больше нуля.");
            }
            MaxVolume = maxVolume;
        }

        public void Add(uint liquidVolume) {
            CurrentVolume += (int)liquidVolume;
        }

        public void Take(uint liquidVolume) {
            CurrentVolume -= (int)liquidVolume;
        }

        public override string ToString() {
            return $"Цистерна заполнена на {CurrentVolume} л из {MaxVolume} л.";
        }
    }

    class TankOverflowException : Exception {
        public TankOverflowException() : base("В цистерне не хватает места.") { }
    }
    class NotEnoughException : Exception {
        public NotEnoughException() : base("В цистерне недостаточно жидкости.") { }
    }
}
