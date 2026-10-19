using System;
using System.Collections.Generic;
using System.Text;

namespace PRG282_BoababRecords;
public static class UpdateRecordHandler
{
    public static Animal CurrentlySelectedAnimal(List<Animal> animalsInFile, DataGridViewRow mainGrid)
    {
        return animalsInFile[mainGrid.Index];
    }

    public static void UpdateDataInTextBoxes(TextBox nameTbx, TextBox speciesTbx, TextBox ageTbx, TextBox RSTbx, Animal animal)
    {
        nameTbx.Text = animal.Name;
        speciesTbx.Text = animal.Species;
        ageTbx.Text = animal.Age.ToString();
        RSTbx.Text = animal.RecoveryScore.ToString();
    }

    public static void ClearTextBoxes(TextBox nameTbx, TextBox speciesTbx, TextBox ageTbx, TextBox RSTbx)
    {
        nameTbx.Text = string.Empty;
        speciesTbx.Text = string.Empty;
        ageTbx.Text = string.Empty;
        RSTbx.Text = string.Empty;
    }

    public static string[] DataReadFromTextBoxes(TextBox nameTbx, TextBox speciesTbx, TextBox ageTbx,  TextBox recoveryScoreTbx)
        => new string[] {nameTbx.Text, speciesTbx.Text, ageTbx.Text, recoveryScoreTbx.Text};

    public static void WriteDataToTempFile(string temporaryFile, string[] dataReadFromUpdate)
    {
        File.CreateText(temporaryFile);

        File.WriteAllLines(temporaryFile, dataReadFromUpdate);
    }

    public static Animal CreateUpdatedAnimalRecord(List<Animal> animalsInFile, int listIndex, string[] updatedContent)
    {
        string animalId = animalsInFile[listIndex].AnimalID;

        return new Animal(animalId, updatedContent[0], updatedContent[1], int.Parse(updatedContent[2]), int.Parse(updatedContent[3]));
    }

    public static void WriteUpdatedAnimalToFile(Animal updatedAnimal, int lineIndex)
    {
        string updatedLine = updatedAnimal.ToFileLine();

        string[] fileLines = File.ReadAllLines("animals.txt");

        if (lineIndex > 0 && lineIndex <= fileLines.Length)
        {
            fileLines[lineIndex] = updatedLine;

            File.WriteAllLines("animals.txt", fileLines);
        }
    }
}

