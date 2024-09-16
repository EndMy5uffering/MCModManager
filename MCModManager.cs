using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Text.Json;

namespace MinecraftModManager
{
    public partial class MinecraftModManager : Form
    {
        private Settings settings = new Settings();
        private List<ModGroup> ModGroups = new List<ModGroup>();
        private ModGroup? selectedModGroup = null;
        private Mod? selectedMod = null;
        bool uncheckOneDoNotUncheckAll = false;
        public MinecraftModManager()
        {
            InitializeComponent();
            this.FormClosing += FormCloseEvent;
            ModCheckList.ItemCheck += ModCheckEvent;
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
                    ModGroupsGouping.Enabled = true;
                    return;
                }
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
                ModGroupsGouping.Enabled = true;
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
                    selectedMod = null;
                    WriteResources();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedModGroup == null)
            {
                MessageBox.Show(this, "Can not add mod a null set!", "[Error] Add mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.InitialDirectory = settings.RootResourcePath;
            fileDialog.Filter = "JAR Files (*.jar)|*.jar";
            DialogResult result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                foreach (string fname in fileDialog.FileNames)
                {
                    string fileName = Path.GetFileName(fname);
                    string fileWithoutExtention = Path.GetFileNameWithoutExtension(fname);
                    if (!Directory.Exists(settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName))
                    {
                        Directory.CreateDirectory(settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName);
                    }
                    if (File.Exists(settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName + "\\" + fileName))
                    {
                        MessageBox.Show(this, "File already exists!", "[Error] Add mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    File.Copy(fname, settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName + "\\" + fileName);

                    Mod mod = new Mod();
                    mod.ModName = fileWithoutExtention;
                    mod.ModPath = settings.RootResourcePath + "\\" + selectedModGroup.ModGroupName + "\\" + fileName;
                    mod.IsEnabled = true;
                    if (selectedModGroup.mods == null) selectedModGroup.mods = new List<Mod>();
                    selectedModGroup.mods.Add(mod);
                    ModCheckList.Items.Add(mod.ModName);
                    ModCheckList.SetItemChecked(ModCheckList.Items.Count - 1, true);
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedModGroup == null)
            {
                MessageBox.Show(this, "Can not delete mod from null group!", "[Error] Delte mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selectedMod == null)
            {
                MessageBox.Show(this, "Can not remove null object!", "[Error] Delete mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ModCheckList.Items.Remove(selectedMod.ModName);
            if (selectedModGroup.mods != null) selectedModGroup.mods.Remove(selectedMod);
            WriteResources();

            if (!File.Exists(selectedMod.ModPath))
            {
                MessageBox.Show(this, "Can not remove mod file dose not exist!", "[Error] Delete mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                File.Delete(selectedMod.ModPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "[Error] Delete mod", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            selectedMod = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (selectedModGroup == null)
            {
                MessageBox.Show(this, "Can not load mods from null mod group!", "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selectedModGroup.mods == null)
            {
                MessageBox.Show(this, "No mods in mod group!", "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (settings.PathModFolder == null)
            {
                MessageBox.Show(this, "No destination folder set for mods!", "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Directory.Exists(settings.PathModFolder))
            {
                MessageBox.Show(this, "Mod path dose not exists!", "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult res = MessageBox.Show(this, "Load selected mods? This aciton will delete all mods in the selected mods folder:\n" + settings.PathModFolder + "\n\nLoad Mods?", "[Info] Load mods", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                try
                {
                    IEnumerable<string> currentFiles = Directory.EnumerateFiles(settings.PathModFolder);
                    foreach (string file in currentFiles)
                    {
                        string extention = Path.GetExtension(file);
                        if (extention == ".jar" || extention == ".JAR") File.Delete(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                for (int i = 0; i < ModCheckList.Items.Count; ++i)
                {
                    Debug.WriteLine("Check if mod is checked");
                    if (ModCheckList.CheckedIndices.Contains(i))
                    {
                        Debug.WriteLine("Checked");

                        if (ModCheckList.Items[i] == selectedModGroup.mods[i].ModName)
                        {
                            Debug.WriteLine("Same name");
                            Mod cmod = selectedModGroup.mods[i];
                            string filename = Path.GetFileName(cmod.ModPath);
                            try
                            {
                                File.Copy(cmod.ModPath, settings.PathModFolder + "\\" + filename);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(this, "Could not copy file:\n" + cmod.ModPath + "\nto:\n" + settings.PathModFolder + "\\" + filename, "[Error] Load mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(this, "Unload mods? This aciton will delete all mods in the selected mods folder:\n" + settings.PathModFolder + "\n\nUnload Mods?", "[Info] Load mods", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res == DialogResult.OK)
            {
                try
                {
                    IEnumerable<string> currentFiles = Directory.EnumerateFiles(settings.PathModFolder);
                    foreach (string file in currentFiles)
                    {
                        string extention = Path.GetExtension(file);
                        if (extention == ".jar" || extention == ".JAR") File.Delete(file);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "[Error] Unload mods", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
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
            selectedMod = null;
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
                ModCheckList.Items.Clear();
                if (selectedModGroup.mods == null) return;
                bool allChecked = true;
                foreach (Mod mod in selectedModGroup.mods)
                {
                    ModCheckList.Items.Add(mod.ModName);
                    ModCheckList.SetItemChecked(ModCheckList.Items.Count - 1, mod.IsEnabled);
                    allChecked &= mod.IsEnabled;
                }
                AllChecked.Checked = allChecked;
            }
            else
            {
                ModsGroupBox.Enabled = false;
            }
        }

        private void ModCheckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectedModGroup == null) return;
            if (selectedModGroup.mods == null) return;
            if (ModCheckList.SelectedIndex < 0 || ModCheckList.SelectedIndex > selectedModGroup.mods.Count) return;

            if (selectedModGroup.mods[ModCheckList.SelectedIndex].ModName != ModCheckList.Items[ModCheckList.SelectedIndex])
            {
                MessageBox.Show(this, "Selection missmatch!", "[Error] Mod selection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            selectedMod = selectedModGroup.mods[ModCheckList.SelectedIndex];
        }

        private void ModCheckEvent(object? sender, ItemCheckEventArgs e)
        {
            if (selectedModGroup == null) return;
            if (selectedModGroup.mods == null) return;
            if (e.Index < 0 || selectedModGroup.mods.Count <= e.Index)
            {
                MessageBox.Show(this, "Selection out of bounds!", "[Error] Check change", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            selectedModGroup.mods[e.Index].IsEnabled = e.NewValue == CheckState.Checked;

            bool allChecked = true;
            foreach (Mod mod in selectedModGroup.mods)
            {
                allChecked &= mod.IsEnabled;
            }
            uncheckOneDoNotUncheckAll = (AllChecked.Checked != allChecked);
            AllChecked.Checked = allChecked;

            WriteResources();
        }

        private void AllChecked_CheckedChanged(object sender, EventArgs e)
        {
            if (uncheckOneDoNotUncheckAll)
            {
                uncheckOneDoNotUncheckAll = false;
                return;
            }
            if (selectedModGroup == null) return;
            if (selectedModGroup.mods == null) return;

            bool checkState = AllChecked.Checked;
            for (int i = 0; i < ModCheckList.Items.Count; ++i)
            {
                ModCheckList.SetItemChecked(i, checkState);
                selectedModGroup.mods[i].IsEnabled = checkState;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (settings.PathModFolder == null) return;

            try
            {
                Process.Start("explorer.exe", settings.PathModFolder);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "[Error] Open mod folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (settings.RootResourcePath == null) return;

            try
            {
                Process.Start("explorer.exe", settings.RootResourcePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "[Error] Open resource folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
