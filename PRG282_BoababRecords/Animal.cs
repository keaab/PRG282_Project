namespace PRG282_BoababRecords;

public class Animal
{
    public string AnimalID { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public int RecoveryScore { get; set; }
    public string Status { get; set; }
    public string HousingUnit { get; set; }

    // Default constructor
    public Animal()
    {
    }

    // Constructor used when creating a new animal
    public Animal(string animalID, string name, string species, int age, int recoveryscore)
    {
        AnimalID = animalID;
        Name = name;
        Species = species;
        Age = age;
        RecoveryScore = recoveryscore;
        Classify();
    }

    // Constructor used when loading an animal record directly from file
    public Animal(string animalID, string name, string species, int age, int recoveryscore, string status, string housingunit)
    {
        AnimalID = animalID;
        Name = name;
        Species = species;
        Age = age;
        RecoveryScore = recoveryscore;
        Status = status;
        HousingUnit = housingunit;
    }

    // Calculates status and housing unit based on the recovery score
    public void Classify()
    {
        if (RecoveryScore >= 0 && RecoveryScore <= 19)
        {
            Status = "Critical";
            HousingUnit = "Intensive Care Unit";
        }
        else if (RecoveryScore >= 20 && RecoveryScore <= 39)
        {
            Status = "Serious";
            HousingUnit = "High-Dependency Ward";
        }
        else if (RecoveryScore >= 40 && RecoveryScore <= 59)
        {
            Status = "Stable";
            HousingUnit = "Recovery Ward";
        }
        else if (RecoveryScore >= 60 && RecoveryScore <= 79)
        {
            Status = "Recovering";
            HousingUnit = "Outdoor Enclosure";
        }
        else if (RecoveryScore >= 80 && RecoveryScore <= 100)
        {
            Status = "Release-Ready";
            HousingUnit = "Pre-Release Camp";
        }
    }

    // Converts the animal object into a string for animals.txt
    public string ToFileLine()
    {
        return $"{AnimalID}|{Name}|{Species}|{Age}|{RecoveryScore}|{Status}|{HousingUnit}";
    }
}
