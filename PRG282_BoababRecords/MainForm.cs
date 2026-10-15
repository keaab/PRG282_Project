namespace PRG282_BoababRecords;

public partial class MainForm : Form
{

    private List<Animal> animalList = new List<Animal>();

    public MainForm()
    {
        InitializeComponent();
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        AnimalsFromFile();
    }

    private void AnimalsFromFile()
    {
        string filePath = "animals.txt";

        if (!File.Exists(filePath)) 
        {
            File.Create(filePath).Close();
            return;
        }

        animalList.Clear();
        int skippedLineCount = 0;

        try
        {
            string[] Lines = File.ReadAllLines(filePath);

            foreach(string line in Lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split('|');

                if(parts.Length != 7)
                {
                    skippedLineCount++;
                    continue;
                }

                if (!int.TryParse(parts[3].Trim(), out int age) || !int.Parse(parts[4].Trim(), out int score))
                {
                    skippedLineCount++;
                    continue;
                }

                Animal a = new Animal();
                a.AnimalID = parts[0].Trim().ToUpper();
                a.Name = parts[1].Trim();
                a.Species = parts[2].Trim();
                a.Age = age;
                a.RecoveryScore = score;
                a.Status = parts[5].Trim();
                a.HousingUnit = parts[6].Trim();

                animalList.Add(a);         
            }
            if (skippedLineCount > 0)
            {
                MessageBox.Show($"{skippedLineCount} Malformed lines were skipped during loading.", MessageBoxButtons.OK)
            }
            
        catch(IOException ex)
        {
            MessageBox.Show($"Could not read {filePath}: {ex}", MessageBoxButtons.OK);
        }

        }
}
