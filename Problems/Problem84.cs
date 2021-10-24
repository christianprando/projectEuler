using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler.Problems
{
    internal class Problem84 : IEulerProblem
    {
        public int ProblemNumber => 84;

        private const int GO = 00;
        private const int G2J = 30;
        private const int JAIL = 10;
        
        private const int CC1 = 02;
        private const int CC2 = 17;
        private const int CC3 = 33;
        
        private const int CH1 = 07;
        private const int CH2 = 22;
        private const int CH3 = 36;

        private const int C1 = 11;
        private const int E3 = 24;
        private const int H2 = 39;
        
        private const int R1 = 05;
        private const int R2 = 15;
        private const int R3 = 25;
        private const int R4 = 35;

        private const int U1 = 12;
        private const int U2 = 28;
        
        private List<MonopolySquare> _squares;
        private List<MonopolySquare> _squareLandings;

        private readonly Random _random = new Random();
        private int _doubleCounter;

        public string Run()
        {
            _squares = GenerateSquares();
            _squareLandings = new List<MonopolySquare>
            {
                _squares.First()
            };
            
            for (var i = 0; i < 200000; i++)
            {
                SimulateMove(_squareLandings.Last());
            }
            
            foreach (var square in _squares)
            {
                square.Probability = _squareLandings.Count(x => x.Id == square.Id) /(double)_squareLandings.Count;
            }
            
            _squares.Sort((x, y) => x.Probability > y.Probability ? -1 : 1);
            
            foreach (var square in _squares)
            {
                Console.WriteLine($"Square: {square.IdString} | Probability: {square.Probability}");
            }
            
            
            return _squares.First().IdString + _squares.ElementAt(1).IdString + _squares.ElementAt(2).IdString;
        }

        private void SimulateMove(MonopolySquare currentSquare)
        {
            var diceResult = RollDice(4);
            var nextSquare = (currentSquare.Id + diceResult.Value) % 40;
            
            if (diceResult.IsDouble && _doubleCounter == 2)
            {
                _squareLandings.Add(_squares.First(x => x.Id == JAIL));
                _doubleCounter = 0;
                return;
            }
            
            if (diceResult.IsDouble && _doubleCounter == 0)
            {
                _doubleCounter++;
            }
            
            nextSquare = DoSpecialSquareMove(nextSquare);
            _squareLandings.Add(_squares.First(x => x.Id == nextSquare));
        }

        private int DoSpecialSquareMove(int nextSquare)
        {
            if (IsGoToJail(nextSquare))
            {
                return JAIL;
            }
            if (IsCommunityChest(nextSquare))
            {
                var card = DrawCommunityChestCard();
                return card switch
                {
                    1 => GO,
                    2 => JAIL,
                    _ => nextSquare
                };
            }

            if(IsChance(nextSquare))
            {
                var card = DrawChanceCard();
                
                switch (card)
                {
                    case 1:
                        return GO;
                    case 2:
                        return JAIL;
                    case 3:
                        return C1;
                    case 4:
                        return E3;
                    case 5:
                        return H2;
                    case 6:
                        return R1;
                    case 7:
                    case 8:
                        return GetNextRailway(nextSquare);
                    case 9:
                        return GetNextUtilityCompany(nextSquare);
                    case 10:
                        return DoSpecialSquareMove(nextSquare - 3);
                }
            }

            return nextSquare;
        }

        private int DrawChanceCard()
        {
            return _random.Next(1,17);
        }

        private int DrawCommunityChestCard()
        {
            return _random.Next(1,17);
        }

        private DiceResult RollDice(int i)
        {
            var d1 = _random.Next(1, i + 1);
            var d2 = _random.Next(1, i + 1);
            
            return new DiceResult(d1 + d2, d1 == d2);
        }

        private int GetNextRailway(int nextSquare)
        {
            if (nextSquare < R1 || nextSquare >= R4)
            {
                return R1;
            }

            if (nextSquare < R2)
            {
                return R2;
            }
            
            if (nextSquare < R3)
            {
                return R3;
            }

            return R4;
        }
        
        private int GetNextUtilityCompany(int nextSquare)
        {
            if (nextSquare < U1 || nextSquare >= U2)
            {
                return U1;
            }

            return U2;
        }

        private bool IsCommunityChest(int nextSquare)
        {
            return nextSquare == CC1 || nextSquare == CC2 || nextSquare == CC3;
        }
        
        private bool IsChance(int nextSquare)
        {
            return nextSquare == CH1 || nextSquare == CH2 || nextSquare == CH3;
        }
        
        private bool IsGoToJail(int nextSquare)
        {
            return nextSquare == G2J;
        }
        private List<MonopolySquare> GenerateSquares()
        {
            var list = new List<MonopolySquare>();
            for (var i = 0; i < 40; i++)
            {
                list.Add(new MonopolySquare(i));
            }

            var goToJailSquare = list.First(x => x.Id == G2J);
            goToJailSquare.Probability = 0;
            
            return list;
        }
    }

    internal class DiceResult
    {
        public DiceResult(int value, bool isDouble)
        {
            Value = value;
            IsDouble = isDouble;
        }

        public int Value { get; }
        public bool IsDouble { get; }
    }

    internal class MonopolySquare
    {
        public MonopolySquare(int id)
        {
            Id = id;
            Probability = 1d;
        }

        public int Id { get; }
        public string IdString => Id.ToString("00");
        public double Probability { get; set; }
    }
}