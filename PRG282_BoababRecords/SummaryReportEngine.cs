using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace PRG282_BoababRecords
{
    /// <summary>
    /// Member 4: Engine responsible for summary calculations, summary.txt export, 
    /// safe record deletion, and file exception handling.
    /// </summary>
    public static class SummaryReportEngine
    {
        private const string DataFileName = "animals.txt";
        private const string SummaryFileName = "summary.txt";

        /// <summary>
        /// Reads all animal records from animals.txt, supporting both 5-field and 7-field formats.
        /// </summary>
        public static List<Animal> ReadAnimalsFromFile()
        {
            List<Animal> animals = new List<Animal>();

            try
            {
                if (!File.Exists(DataFileName))
                {
                    File.Create(DataFileName).Dispose();
                    return animals;
                }

                string[] lines = File.ReadAllLines(DataFileName);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|').Select(p => p.Trim()).ToArray();
                    if (parts.Length >= 5)
                    {
                        if (int.TryParse(parts[3], out int age) && int.TryParse(parts[4], out int score))
                        {
                            if (parts.Length == 5)
                            {
                                // Uses the 5-param constructor that calculates Status and HousingUnit automatically via Classify()
                                animals.Add(new Animal(parts[0], parts[1], parts[2], age, score));
                            }
                            else if (parts.Length >= 7)
                            {
                                // Uses the 7-param constructor
                                animals.Add(new Animal(parts[0], parts[1], parts[2], age, score, parts[5], parts[6]));
                            }
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                throw new IOException($"File access error while reading records: {ex.Message}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Permission denied while reading records: {ex.Message}", ex);
            }

            return animals;
        }

        /// <summary>
        /// Deletes an animal record by AnimalID and rewrites animals.txt cleanly.
        /// </summary>
        public static bool DeleteAnimalRecord(string animalId)
        {
            try
            {
                List<Animal> currentAnimals = ReadAnimalsFromFile();
                int originalCount = currentAnimals.Count;

                List<Animal> filtered = currentAnimals
                    .Where(a => !a.AnimalID.Equals(animalId.Trim(), StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filtered.Count == originalCount)
                {
                    return false; // Record not found
                }

                List<string> outputLines = filtered.Select(a => a.ToFileLine()).ToList();
                File.WriteAllLines(DataFileName, outputLines);
                return true;
            }
            catch (IOException ex)
            {
                throw new IOException($"File locked or unavailable during deletion: {ex.Message}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Permission denied while modifying animal records: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Generates the complete summary statistics and writes summary.txt per specification.
        /// </summary>
        public static string GenerateAndExportSummary()
        {
            try
            {
                List<Animal> records = ReadAnimalsFromFile();
                int totalCount = records.Count;

                string avgAgeStr;
                string avgScoreStr;

                if (totalCount == 0)
                {
                    avgAgeStr = "N/A";
                    avgScoreStr = "N/A";
                }
                else
                {
                    double avgAge = records.Average(a => a.Age);
                    double avgScore = records.Average(a => a.RecoveryScore);

                    avgAgeStr = avgAge.ToString("F2", CultureInfo.InvariantCulture);
                    avgScoreStr = avgScore.ToString("F2", CultureInfo.InvariantCulture);
                }

                int criticalCount = records.Count(a => a.Status.Equals("Critical", StringComparison.OrdinalIgnoreCase));
                int seriousCount = records.Count(a => a.Status.Equals("Serious", StringComparison.OrdinalIgnoreCase));
                int stableCount = records.Count(a => a.Status.Equals("Stable", StringComparison.OrdinalIgnoreCase));
                int recoveringCount = records.Count(a => a.Status.Equals("Recovering", StringComparison.OrdinalIgnoreCase));
                int releaseReadyCount = records.Count(a => a.Status.Equals("Release-Ready", StringComparison.OrdinalIgnoreCase));

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");

                string reportContent =
                $@"BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE
                ANIMAL SUMMARY REPORT
                Generated: {timestamp}
                ----------------------------------------
                Total animals: {totalCount}
                Average age (years): {avgAgeStr}
                Average recovery score: {avgScoreStr}
                ----------------------------------------
                Animals per status:
                Critical: {criticalCount}
                Serious: {seriousCount}
                Stable: {stableCount}
                Recovering: {recoveringCount}
                Release-Ready: {releaseReadyCount}";

                File.WriteAllText(SummaryFileName, reportContent);

                return reportContent;
            }
            catch (IOException ex)
            {
                throw new IOException($"Could not write summary.txt. The file might be open elsewhere: {ex.Message}", ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException($"Permission denied when saving summary.txt: {ex.Message}", ex);
            }
        }
    }
}