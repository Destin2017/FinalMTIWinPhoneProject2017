using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ProjectWinphone2017.ViewModels;

namespace ProjectWinphone2017
    {
    public partial class EditorPage : PhoneApplicationPage
        {
        NoteModel ourNote;
        public EditorPage()
            {
            InitializeComponent();
            }

        protected override void OnNavigatedTo(NavigationEventArgs e)
            {
            base.OnNavigatedTo(e);
            string noteID;

            if (NavigationContext.QueryString.TryGetValue("id", out noteID))
                {
                ourNote = App.ViewModel.GetItem(noteID);
                editorTextBox.Text = ourNote.Note;
                }


            }

        protected async override void OnNavigatedFrom(NavigationEventArgs e)
            {
            base.OnNavigatedFrom(e);

            ourNote.Note = editorTextBox.Text.Replace("\r", "\n");
            await App.ViewModel.UpdateNotes();
            }
        }
    }