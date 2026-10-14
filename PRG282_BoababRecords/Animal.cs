public class Animal
{
    public string AnimalID { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public int Age { get; set; }
    public int RecoveryScore { get; set; }
    public string Status { get; set; }
    public string HousingUnit { get; set; }


    public Animal(string animalID, string name, string species, int age, int recoveryscore, string status, string housingunit)
    {
        AnimalID = animalID;
        Name = name;
        Species = species;
        Age = age;
        RecoveryScore = recoveryscore;
        Classify();
    }

    public void Classify()
    {
        if(RecoveryScore<20)
        {
            Status = "Critical";
            HousingUnit = "Instensive Care Unit";
        }
        else if(RecoveryScore>=20 && RecoveryScore < 40) 
        {
            Status = "Serious";
            HousingUnit = "High-Dependancy Ward";
        }
        else if (RecoveryScore >= 40 && RecoveryScore < 60)
        {
            Status = "Stable";
            HousingUnit = "Recovery Ward";
        }
        else if (RecoveryScore >= 60 && RecoveryScore < 80)
        {
            Status = "Recovery";
            HousingUnit = "Outdoor Enclosure";
        }
        else if (RecoveryScore >= 80 && RecoveryScore < 100)
        {
            Status = "Release-Ready";
            HousingUnit = "Pre-Release Camp";
        }
    }

    public string ToFileLine()
    {
        return $"{AnimalID}|{Name}|{Species}|{Age}|{RecoveryScore}|{Status}|{HousingUnit}";
    }
}