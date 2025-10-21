using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero
{
    public class SuperHero
    {
        public string HeroID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Superpower { get; set; }
        public int ExamScore { get; set; }
        public string Rank { get; set; }
        public string ThreatLevel { get; set; }


        public SuperHero(string heroID, string name, int age, string superpower, int examScore)
        {
            HeroID = heroID;
            Name = name;
            Age = age;
            Superpower = superpower;
            ExamScore = examScore;
            CalculateRankAndThreatLevel(); // calculate rank basically
        }
        private void CalculateRankAndThreatLevel()
        {
            if (ExamScore >= 81 && ExamScore <= 100)
            {
                Rank = "S-Rank";
                ThreatLevel = "Finals Week";
            }
            else if (ExamScore >= 61 && ExamScore <= 80)
            {
                Rank = "A-Rank";
                ThreatLevel = "Midterm Madness";
            }
            else if (ExamScore >= 41 && ExamScore <= 60)
            {
                Rank = "B-Rank";
                ThreatLevel = "Group Project Gone Wrong";
            }
            else if (ExamScore >= 0 && ExamScore <= 40)
            {
                Rank = "C-Rank";
                ThreatLevel = "Pop Quiz";
            }
        }
        public string ToFileString()
        {
            return $"{HeroID}|{Name}|{Age}|{Superpower}|{ExamScore}|{Rank}|{ThreatLevel}";
        }
        public static SuperHero FromFileString(string line)
        {
            string[] parts = line.Split('|');
            if (parts.Length >= 5)
            {
                return new SuperHero(parts[0], parts[1], int.Parse(parts[2]), parts[3], int.Parse(parts[4]));
            }
            return null;
        }
    }
    public static class FileManager
    {
        private static readonly string fileName = "superheroes.txt";

        // Save a new superhero to file
        public static void SaveSuperhero(SuperHero hero)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(hero.ToFileString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving superhero: {ex.Message}");
            }
        }

        public static void SaveSuperheroes(List<SuperHero> heroes)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false)) // false = overwrite file
                {
                    foreach (SuperHero hero in heroes)
                    {
                        writer.WriteLine(hero.ToFileString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving superheroes: {ex.Message}");
            }
        }
        public static List<SuperHero> LoadSuperheroes()
        {
            List<SuperHero> heroes = new List<SuperHero>();

            try
            {
                // Create file if it doesnt exist
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Close();
                    return heroes;
                }

                // Read all the lines in txtFile
                string[] lines = File.ReadAllLines(fileName);

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        SuperHero hero = SuperHero.FromFileString(line);
                        if (hero != null)
                        {
                            heroes.Add(hero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading superheroes: {ex.Message}");
            }

            return heroes;
        }
        public static bool HeroIDExists(string heroID)
        {
            try
            {
                List<SuperHero> heroes = LoadSuperheroes();
                return heroes.Any(h => h.HeroID.Equals(heroID, StringComparison.OrdinalIgnoreCase));
            }
            catch
            {
                return false;
            }
        }
    }

 }
