using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PRG282_BoababRecords
{
    internal class AnimalFileManager
    {
        private static readonly string filePath =
        Path.Combine(AppContext.BaseDirectory, "animals.txt");

        public static void EnsureFileExists()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, "");
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Unable to create animals.txt. Check whether the file is in use.");
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Permission denied when accessing animals.txt.");

            }
        }
        // This will read the animal records from animals.txt
        public static List<Animal> LoadAnimals()
        {
            List<Animal> animals = new List<Animal>();
            EnsureFileExists();

            if (!File.Exists(filePath))
            {
                return animals;
            }

            string[] lines;

            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (IOException)
            {
                MessageBox.Show("Unable to read animals.txt. The file may be in use.");
                return animals;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Permission denied when reading animals.txt.");
                return animals;
            }
            int skippedLines = 0;

            foreach (string line in lines)
            {
                string[] fields = line.Split('|');

                if (fields.Length != 7)
                {
                    skippedLines++;
                    continue;
                }
                if (!int.TryParse(fields[3], out int age) ||
                 age < 0 || age > 100)
                {
                    skippedLines++;
                    continue;
                }

                if (!int.TryParse(fields[4], out int recoveryScore) ||
                    recoveryScore < 0 || recoveryScore > 100)
                {
                    skippedLines++;
                    continue;
                }
                string animalID = fields[0].Trim().ToUpperInvariant();

                if (!Regex.IsMatch(animalID, @"^WR-[0-9]{4}$"))
                {
                    skippedLines++;
                    continue;
                }
                string name = fields[1].Trim();
                string species = fields[2].Trim();
                string status = fields[5].Trim();
                string housingUnit = fields[6].Trim();
                Animal animal = new Animal(
                    animalID,
                    name,
                    species,
                    age,
                    recoveryScore,
                    status,
                    housingUnit

                 );

                animals.Add(animal);
            }

            if (skippedLines > 0)
            {
                MessageBox.Show(
                    $"{skippedLines} malformed animal record(s) were skipped.",
                    "File Loading Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            return animals;
        }
    }
}
