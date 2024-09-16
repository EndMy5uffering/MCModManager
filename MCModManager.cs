using System.Text.Json;

namespace MinecraftModManager
{
    public partial class MinecraftModManager : Form
    {
        private Settings settings = new Settings();
        private List<ModGroup> ModGroups = new List<ModGroup>();
        private ModGroup? selectedModGroup = null;
        public MinecraftModManager()
        {
            InitializeComponent();
            this.FormClosing += FormCloseEvent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bool hasSettingsFile = File.Exists("./Settings.json");
            FileStream settingsStream = File.Open("./Settings.json", FileMode.OpenOrCreate);

            if (hasSettingsFile)
            {
                try
                {
                    settings = JsonSerializer.Deserialize<Settings>(settingsStream);
                    if (settings == null) settings = new Settings();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "[Error] Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            settingsStream.Close();

            if (settings.PathModFolder != null) textBox1.Text = settings.PathModFolder;

            if (settings.RootResourcePath != null) textBox2.Text = settings.RootResourcePath;

            CheckAndLoadResources();

        }

        private void FormCloseEvent(object? sender, FormClosingEventArgs e)
        {
            WriteSettings();
            WriteResources();
        }

        void WriteSettings()
        {
            File.WriteAllText("./Settings.json", JsonSerializer.Serialize(settings));
        }

        void WriteResources()
        {
            if (settings.RootResourcePath != null && File.Exists(settings.RootResourcePath + "\\resources.json"))
            {
                File.WriteAllText(settings.RootResourcePath + "\\resources.json", JsonSerializer.Serialize(ModGroups));
            }
        }

        private void CheckAndLoadResources()
        {
            if (settings.RootResourcePath == null)
            {
                ModGroupsGouping.Enabled = false;
                return;
            }

            if (!File.Exists(settings.RootResourcePath + "\\resources.json"))
            {
                DialogResult dr = MessageBox.Show(this, "No resources detected!\nCreate new resource root at: " + settings.RootResourcePath + "?", "[Error] No root", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                if (dr == DialogResult.OK)
                {
                    File.Create(settings.RootResourcePath + "\\resources.json").Close();
                    return;
                }
                else
                {
                    ModGroupsGouping.Enabled = false;
                    return;
                }
            }

            if (!File.Exists(settings.RootResourcePath + "\\resources.json"))
            {
                ModGroupsGouping.Enabled = false;
                return;
            }


            FileStream fs = null;
            try
            {
                fs = File.Open(settings.RootResourcePath + "\\resources.json", FileMode.Open);
                List<ModGroup> modGroup = JsonSerializer.Deserialize<List<ModGroup>>(fs);
                if (modGroup != null) ModGroups = modGroup;
                int uitemCount = 0;
                foreach (ModGroup mg in modGroup)
                {
                    if (mg.ModGroupName != null)
                    {
                        ModGroupItembox.Items.Add(mg.ModGroupName);
                    }
                    else
                    {
                        ModGroupItembox.Items.Add("Unidentified " + uitemCount);
                        uitemCount++;
                    }
                }
                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "[Error] Resources", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (fs != null) fs.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.PathModFolder = fbd.SelectedPath;
                textBox1.Text = settings.PathModFolder;
            }
        }

        private void OpenResourcePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                settings.RootResourcePath = fbd.SelectedPath;
                textBox2.Text = settings.RootResourcePath;
            }

            CheckAndLoadResources();
        }

        private void AddNewModGroup_Click(object sender, EventArgs e)
        {
            if (settings.RootResourcePath == null || !File.Exists(settings.RootResourcePath + "\\resources.json"))
            {
                MessageBox.Show(this, "Can not add group without resource path or resource file!", "[Error] Add Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ngroupName = NewGroupNameTextbox.Text;
            if (ngroupName == null || ngroupName == string.Empty)
            {
                DialogResult dr = MessageBox.Show(this, "Can not add group without a name!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK) return;
            }

            if (ModGroupItembox.Items.Contains(ngroupName))
            {
                DialogResult dr = MessageBox.Show(this, "Group allready exists!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK) return;
            }

            ModGroupItembox.Items.Add(ngroupName);
            NewGroupNameTextbox.Text = string.Empty;

            ModGroup modGroup = new ModGroup();
            modGroup.ModGroupName = ngroupName;
            this.ModGroups.Add(modGroup);

            Directory.CreateDirectory(settings.RootResourcePath + "\\" + modGroup.ModGroupName);

            WriteResources();
        }

        private void DeleteSelectedModGroup_Click(object sender, EventArgs e)
        {
            if (selectedModGroup != null)
            {
                DialogResult res = MessageBox.Show(this, "Remove mod group and all mods within the group " + selectedModGroup.ModGroupName + "?", "Remove group " + selectedModGroup.ModGroupName, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.Cancel)
                {
                    return;
                }
                else if (res == DialogResult.OK)
                {
                    ModGroupItembox.Items.Remove(selectedModGroup.ModGroupName);
                    ModCheckList.Items.Clear();
                    ModsGroupBox.Enabled = false;
                    ModGroups.Remove(selectedModGroup);

                    try
                    {
                        IEnumerable<string> fileNames = Directory.EnumerateFiles(settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName);
                        foreach (string fileName in fileNames)
                        {
                            if (!File.Exists(fileName)) { continue; }
                            File.Delete(fileName);
                        }
                        Directory.Delete(settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName);
                    }
                    catch (Exception ex) 
                    {
                        MessageBox.Show(this, ex.Message, "[Error] Delete group " + selectedModGroup.ModGroupName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    MessageBox.Show(this, "Group " + selectedModGroup.ModGroupName + " deleted.", "[Info] Delete group " + selectedModGroup.ModGroupName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    selectedModGroup = null;
                    WriteResources();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            settings.PathModFolder = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            settings.RootResourcePath = textBox2.Text;
        }

        private void ModGroupItembox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ModGroupItembox.SelectedItems.Count <= 0) return;
            object? item = ModGroupItembox.SelectedItems[0];
            if (item == null) return;
            for (int i = 0; i < ModGroups.Count; ++i)
            {
                ModGroup modGroup = ModGroups[i];
                if (modGroup.ModGroupName == (string)item)
                {
                    selectedModGroup = modGroup;
                    break;
                }
            }

            if (selectedModGroup != null)
            {
                ModsGroupBox.Enabled = true;
            }
            else
            {
                ModsGroupBox.Enabled = false;
            }
        }

        private void ModCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
