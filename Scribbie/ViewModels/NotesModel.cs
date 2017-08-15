using DreamTimeStudioZ.Recipes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ProjectWinphone2017.ViewModels
    {
    public class NotesModel : INotifyPropertyChanged
        {

        private const string DATAFILENAME = "scrbbdb.xml";

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
            {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
                {
                handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

        public ObservableCollection<NoteModel> Items
        { get; private set; }

        public NotesModel()
            {

            }

        public bool IsDataLoaded
        { get; private set; }

        public NoteModel CreateNewNote()
            {
            NoteModel result = new NoteModel()
            {
                ID = DateTime.Now.Ticks.ToString(),
                Note = ""
            };

            this.Items.Add(result);
            return result;
            }

        public NoteModel GetItem(string ID)
            {
            NoteModel result = this.Items.Where(f => f.ID == ID).FirstOrDefault();
            return result;
            }

        public async Task RemoveNote(NoteModel noteToRemove)
            {
            this.Items.Remove(noteToRemove);
           await UpdateNotes();
            NotifyPropertyChanged("Items");
            }
        public async Task UpdateNotes()
            {
            StorageFolder appFolder = IORecipes.GetAppStorageFolder();
            await IORecipes.DeleteFileInFolder(appFolder, DATAFILENAME);

            string itemsAsXml = IORecipes.SerializeToString(this.Items);
            StorageFile dataFile = await IORecipes.CreateFileInFolder(appFolder, DATAFILENAME);

            await IORecipes.WriteStringToFile(dataFile, itemsAsXml);
            }

        public async Task LoadData()
            {
            StorageFolder appFolder = IORecipes.GetAppStorageFolder();
            StorageFile dataFile = await IORecipes.GetFileInFolder(appFolder, DATAFILENAME);

            if (dataFile != null)
                {
                if (!IsDataLoaded)
                    {
                    string itemsAsXml = await IORecipes.ReadStringFromFile(dataFile);
                    this.Items = IORecipes.SerializeFromString<ObservableCollection<NoteModel>>(itemsAsXml);
                    }
                }
            else
                {
                if (!IsDataLoaded)
                    {
                    this.Items = new ObservableCollection<NoteModel>();

                    this.Items.Add(
                        new NoteModel()
                        {
                            ID = DateTime.Now.Ticks.ToString(),
                            Note = "Give me the splendid silent sun\nwith all his beams full-dazzling,\nGive me juicy autumnal fruit ripe and red from the orchard,\nGive me a field where the unmow'd grass grows."
                        });

                    this.Items.Add(
                        new NoteModel()
                        {
                            ID = (DateTime.Now.Ticks + 1).ToString(),
                            Note = "I lay on Delos of the Cyclades\nAt evening, on a cape of golden land.  "
                        });

                    this.Items.Add(
                        new NoteModel()
                        {
                            ID = (DateTime.Now.Ticks + 2).ToString(),
                            Note = "On the green of the hill\nWe will drink our fill\nOf golden sunshine"
                        });
                    }
                }

            this.IsDataLoaded = true;
            NotifyPropertyChanged("Items");
            }


        }
    }
