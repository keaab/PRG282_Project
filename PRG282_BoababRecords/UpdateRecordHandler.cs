using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PRG282_BoababRecords;
public static class UpdateRecordHandler
{
    public static bool AllTextBoxesValid(string[] textBoxInputs)
    {
        //A collection a boolean variable used to determine whether the data from the textboxes is valid
        bool nameFieldFailure = textBoxInputs[0] == null || textBoxInputs[0] == string.Empty;
        bool speciesFieldFailure = textBoxInputs[1] == null || textBoxInputs[1] == string.Empty;
        bool ageFieldIncorrectFormat = !int.TryParse(textBoxInputs[2], out int ageValue);
        bool ageFieldEmpty = textBoxInputs[2] == null || textBoxInputs[2] == string.Empty;
        bool recoveryScoreFieldIncorrectFormat = !int.TryParse(textBoxInputs[3], out int RSvalue);
        bool recoveryScoreFieldEmpty = textBoxInputs[3] == null || textBoxInputs[3] == string.Empty;

        #region IF-Checklist
        //A number of if checks against the above booleans. Is messy and could be refactored to be more streamlined.
        if (nameFieldFailure)
        {
            MessageBox.Show("The name field cannot be left empty. Please enter a valid name.", "Invalid record operation", 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (speciesFieldFailure)
        {
            MessageBox.Show("The species field cannot be left empty. Please enter a valid species.", "Invalid record operation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (ageFieldEmpty)
        {
            MessageBox.Show("The age field must be a number. Please enter a valid number.", "Invalid record operation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (recoveryScoreFieldIncorrectFormat)
        {
            MessageBox.Show("The recovery score field must be a number. Please enter a valid number.", "Invalid record operation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (ageFieldIncorrectFormat)
        {
            MessageBox.Show("The age field cannot be left empty. Please enter a valid number.", "Invalid record operation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (recoveryScoreFieldEmpty)
        {
            MessageBox.Show("The recovery score field cannot be left empty. Please enter a valid number.", "Invalid record operation",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        #endregion
        else
            return true;
    }

    //Used for upated the text field of the textboxes to display the data of the animal currently being updated
    public static void UpdateDataInTextBoxes(TextBox nameTbx, TextBox speciesTbx, TextBox ageTbx, TextBox RSTbx, Animal animal)
    {
        nameTbx.Text = animal.Name;
        speciesTbx.Text = animal.Species;
        ageTbx.Text = animal.Age.ToString();
        RSTbx.Text = animal.RecoveryScore.ToString();
    }

    //Simply reads the text field of the textboxes and assigns the value to there relevant place in the array. Does not handle data validation.
    public static string[] DataReadFromTextBoxes(TextBox nameTbx, TextBox speciesTbx, TextBox ageTbx,  TextBox recoveryScoreTbx)
        => new string[] {nameTbx.Text, speciesTbx.Text, ageTbx.Text, recoveryScoreTbx.Text};

    //Create a new animal instance based upon the string inputs from above array. Parses the strings to integer where necessary.
    public static Animal CreateUpdatedAnimalRecord(List<Animal> animalsInFile, int listIndex, string[] updatedContent)
    {
        string animalId = animalsInFile[listIndex].AnimalID;

        return new Animal(animalId, updatedContent[0], updatedContent[1], int.Parse(updatedContent[2]), int.Parse(updatedContent[3]));
    }

    public static void WriteUpdatedAnimalToFile(Animal updatedAnimal, int lineIndex)
    {
        //string filePath = @"C:\Users\chris\OneDrive\Desktop\Belgium Campus 2026\PRG282\PRG282_Project\PRG282_BoababRecords\animals.txt";
        string updatedLine = updatedAnimal.ToFileLine();

        string[] fileLines = File.ReadAllLines(AnimalFileManager.filePath);

        if (lineIndex >= 0 && lineIndex < fileLines.Length)
        {
            fileLines[lineIndex] = updatedLine;

            try
            {
                File.WriteAllLines(AnimalFileManager.filePath, fileLines);
            }
            catch(IOException ex)
            {
                MessageBox.Show($"Could not read {AnimalFileManager.filePath}: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(UnauthorizedAccessException ex)
            {
                MessageBox.Show($"Could not read {AnimalFileManager.filePath}: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public static string IdQueryResult(string inputId, List<string> listOfIds)
    {
        if(inputId == null || inputId == string.Empty || inputId == "ID:")
        {
            MessageBox.Show("Please enter a valid ID.", "ID cannot be found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return "ID:";
        }
        else
        {
            return listOfIds.FirstOrDefault(id => string.Equals(id, inputId, StringComparison.OrdinalIgnoreCase));
        }
    }

    public static List<string> ReadIdsFromFile(List<Animal> animalsInFile)
    {
        List<string> listOfIds = new List<string>();

        foreach(Animal animal in animalsInFile)
        {
            listOfIds.Add(animal.AnimalID);
        }

        return listOfIds;
    }   
}

