using System;
using System.Data; //Install-Package MySql.Data
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Timers;
using Timer = System.Windows.Forms.Timer;
using System.Media;
using System.IO;

using NAudio.Wave;
using System.Threading;
using System.Net.Http;
using Newtonsoft.Json;


namespace alarmeWinForm
{
    public partial class btnzone2 : Form
    {
        private Timer timer;

        public btnzone2()
        {
            InitializeComponent();
            InitializeTimer();     
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // Vérifiez toutes les 1000 ms (1 seconde)
            timer.Tick += (sender, e) => UpdateUI();
            timer.Start();
        }

        private int prevState = -1;
        private bool prevZone1, prevZone2, prevZone3;

        private bool onOffSoundPlayed = false;
        private bool zone1SoundPlayed = false;
        private bool zone2SoundPlayed = false;
        private bool zone3SoundPlayed = false;

        private bool IntToBool(int value)
        {
            return value == 1;
        }

        private async void UpdateUI()
        {
            DataTable data = await GetDataFromDatabaseAsync();

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                int state = Convert.ToInt32(row["state"]);
                bool zone1 = IntToBool(Convert.ToInt32(row["zone1"]));
                bool zone2 = IntToBool(Convert.ToInt32(row["zone2"]));
                bool zone3 = IntToBool(Convert.ToInt32(row["zone3"]));

                // Update buttons based on state and zones
                btnState.BackColor = state == 1 ? Color.Green : Color.Red;
                btnState.Text = state == 1 ? "ON" : "OFF";
                btnZ1.BackColor = zone1 ? Color.Red : Color.Black;
                btnZ2.BackColor = zone2 ? Color.Red : Color.Black;
                btnZ3.BackColor = zone3 ? Color.Red : Color.Black;

                if (prevState != state)
                {
                    if (!onOffSoundPlayed)
                    {
                        timer.Stop(); // Stop the timer
                        await PlaySoundAsync(state == 1 ? "isON.wav" : "isOFF.wav");
                        onOffSoundPlayed = true;
                        timer.Start(); // Restart the timer
                    }
                    prevState = state;
                }
                else
                {
                    onOffSoundPlayed = false;
                }

                if (prevZone1 != zone1)
                {
                    if (zone1 && prevState == 1 && !zone1SoundPlayed)
                    {
                        await PlaySoundAsync("z1.wav");
                        zone1SoundPlayed = true;
                    }
                    prevZone1 = zone1;
                }
                else
                {
                    zone1SoundPlayed = false;
                }

                if (prevZone2 != zone2)
                {
                    if (zone2 && prevState == 1 && !zone2SoundPlayed)
                    {
                        await PlaySoundAsync("z2.wav");
                        zone2SoundPlayed = true;
                    }
                    prevZone2 = zone2;
                }
                else
                {
                    zone2SoundPlayed = false;
                }

                if (prevZone3 != zone3)
                {
                    if (zone3 && prevState == 1 && !zone3SoundPlayed)
                    {
                        await PlaySoundAsync("z3.wav");
                        zone3SoundPlayed = true;
                    }
                    prevZone3 = zone3;
                }
                else
                {
                    zone3SoundPlayed = false;
                }
            }
        }


        private async Task PlaySoundAsync(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sounds", fileName);
            if (File.Exists(path))
            {
                using (var audioFile = new AudioFileReader(path))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    await Task.Delay((int)audioFile.TotalTime.TotalMilliseconds);
                }
            }
        }


        private async Task<DataTable> GetDataFromDatabaseAsync()
        {
            string phpUrl = "http://172.20.10.2/alarmProject/fromDbToWF.php";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(phpUrl);
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    Dictionary<string, object> row = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                    DataTable dataTable = new DataTable();

                    if (row.Count > 0)
                    {
                        foreach (var column in row)
                        {
                            dataTable.Columns.Add(column.Key, column.Value.GetType());
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (var column in row)
                        {
                            dataRow[column.Key] = column.Value;
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    return dataTable;
                }
                else
                {
                    MessageBox.Show("Erreur lors de la récupération des données de la base de données.");
                    return new DataTable();
                }
            }
        }




        private async Task UpdateAlarmStateAsync(int state, bool zone1, bool zone2, bool zone3)
        {
            using (var httpClient = new HttpClient())
            {
                var formData = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("state", state.ToString()),
                    new KeyValuePair<string, string>("zone1", zone1.ToString()),
                    new KeyValuePair<string, string>("zone2", zone2.ToString()),
                    new KeyValuePair<string, string>("zone3", zone3.ToString())
                });
                
                string phpUrl = "http://172.20.10.2/alarmProject/postToDb.php";
                var response = await httpClient.PostAsync(phpUrl, formData);
                if (!response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Erreur lors de la mise à jour de l'état de l'alarme.");
                }
            }
        }

        private async void btnState_Click(object sender, EventArgs e)
        {
            int newState = prevState == 1 ? 0 : 1;
            if (newState == 1)
            {
                await UpdateAlarmStateAsync(newState, prevZone1, prevZone2, prevZone3);
            }
            else
            {
                await UpdateAlarmStateAsync(newState, false, false, false);
            }
        }

        private async void btnZ1_Click(object sender, EventArgs e)
        {
            if (prevState == 1) // Allow zone change only when the system is ON
            {
                await UpdateAlarmStateAsync(prevState, true, false, false); // Activate zone 1, deactivate zones 2 and 3
            }
        }

        private async void btnZ2_Click(object sender, EventArgs e)
        {
            if (prevState == 1) // Allow zone change only when the system is ON
            {
                await UpdateAlarmStateAsync(prevState, false, true, false); // Activate zone 2, deactivate zones 1 and 3
            }
        }

        private async void btnZ3_Click(object sender, EventArgs e)
        {
            if (prevState == 1) // Allow zone change only when the system is ON
            {
                await UpdateAlarmStateAsync(prevState, false, false, true); // Activate zone 3, deactivate zones 1 and 2
            }
        }




        //Corbeil============================================================
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


    }
}
