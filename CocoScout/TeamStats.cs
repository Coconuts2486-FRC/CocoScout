using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocoScout
{
    public class TeamStats
    {
        public int TeamNumber { get; set; }

        public int MatchNumber { get; set; }

        public int TeleOpScore { get
            {
                int score = LowBar * 5 + ChevaldeFrise * 5 + Moat * 5 + Ramparts * 5 + Drawbridge * 5 +
                    SallyPort * 5 + RockWall * 5 + RoughTerrain * 5 + LowGoalTele * 2 + HighGoalTeleHit * 5;
                if (ScaledCheck == true)
                {
                    score += 15;
                }
                else if (ChallengedCheck == true) 
                {
                    score += 5;
                }
                return score;
            }
        }

        public int AutoScore { get
            {
                return LowBarAuto *10 + ChevaldeFriseAuto * 10 + MoatAuto * 10 + RampartsAuto * 10 + DrawbridgeAuto * 10 +
                    SallyPortAuto * 10 + RockWallAuto * 10 + RoughTerrainAuto * 10 + LowGoalAuto * 5 + HighGoalAuto * 10;
            }
        }

        public int LowGoalAuto { get; set; }

        public int HighGoalAuto { get; set; }

        public int LowGoalTele { get; set; }

        public int HighGoalTeleFail { get; set; }

        public int HighGoalTeleHit { get; set; }

        public double HighGoalTelePercent { get
            {
                int total = HighGoalTeleFail + HighGoalTeleHit;
                if (total > 0)
                {
                    double percent = (double)HighGoalTeleHit / (double)total;
                    return Math.Round(percent * 100, 1);
                }
                else
                {
                    return -1;
                }
            }
        }

        public int LowBar { get; set; }

        public int ChevaldeFrise { get; set; }

        public int Moat { get; set; }

        public int Ramparts { get; set; }

        public int Drawbridge { get; set; }

        public int SallyPort { get; set; }

        public int RockWall { get; set; }

        public int RoughTerrain { get; set; }

        public int LowBarAuto { get; set; }

        public int ChevaldeFriseAuto { get; set; }

        public int MoatAuto { get; set; }

        public int RampartsAuto { get; set; }

        public int DrawbridgeAuto { get; set; }

        public int SallyPortAuto { get; set; }

        public int RockWallAuto { get; set; }

        public int RoughTerrainAuto { get; set; }

        public bool ChallengedCheck { get; set; }

        public bool ScaledCheck { get; set; }
    }
}
