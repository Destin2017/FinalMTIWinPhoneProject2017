using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ProjectWinphone2017.Resources;
using ProjectWinphone2017.ViewModels;
using Microsoft.Xna.Framework.GamerServices;



namespace ProjectWinphone2017
    {
    public partial class MainPage : PhoneApplicationPage
        {

        private NoteModel lastSelectedNote;

        ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FromTile"));
        static string backTitle;
        static string tileID;
    

        // Constructor
        public MainPage()
            {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("tileID @initializeComponent: " + tileID);
            
            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
            }

        /** private void UpdateTile_click(object sender, RoutedEventArgs e)
             {
             ShellTile pinnedTile = ShellTile.ActiveTiles.First(); //the pinnedTile object will have reference 
                                                                                    //to the default pinned tile of the application
                                                                                     //even if this tile isn't pinned to the start screen
             FlipTileData updatedTileData = new FlipTileData
                 {
                     Title = "My Notes",
                     Count = 8,

                     SmallBackgroundImage = new Uri ("/Assets/fliptilesmall.png", UriKind.Relative),
                     BackgroundImage = new Uri ("/Assets/fliptilemedium.png", UriKind.Relative),
                     WideBackgroundImage = new Uri ("/Assets/fliptilewide.png", UriKind.Relative),

                     BackBackgroundImage = new Uri ("/Assets/backbckgr.png", UriKind.Relative),
                     WideBackBackgroundImage = new Uri ("/Assets/backbckgr.png", UriKind.Relative),

                     BackTitle = "San Francisco, CA",
                     BackContent = "Sunny, 29",
                     WideBackContent = "Tuesday May 23rd\nSunny 29 degrees"
                 };

             pinnedTile.Update(updatedTileData);
             } **/

        // Load data for the ViewModel Items

        protected async override void OnNavigatedTo(NavigationEventArgs e)
            {
            if (!App.ViewModel.IsDataLoaded)
                {
                await App.ViewModel.LoadData();
                }
            }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            NoteModel selectedItem = MainLongListSelector.SelectedItem as NoteModel;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/EditorPage.xaml?id=" + selectedItem.ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            lastSelectedNote = selectedItem;
            MainLongListSelector.SelectedItem = null;
            }

        private void AddButton_Click(object sender, EventArgs e)
            {


            NoteModel newNote = App.ViewModel.CreateNewNote();

            NavigationService.Navigate(new Uri("/EditorPage.xaml?id=" + newNote.ID, UriKind.Relative));


            /**  if (TileToFind != null)
                  {
                  TileToFind.Update(new StandardTileData
                  {
                      BackgroundImage = new Uri("/Assets/finger-string.jpg", UriKind.Relative),
                      Title = "AfterDelScribbie",
                      Count = App.ViewModel.Items.Count,
                      BackTitle = "AfetrDelNotes",
                      BackContent = backTitle,
                      BackBackgroundImage = new Uri("", UriKind.Relative)
                  });
                  } **/


            }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
            {
            lastSelectedNote = (sender as MenuItem).DataContext as NoteModel;
            System.Diagnostics.Debug.WriteLine("tileID before MSGBOX delete: " + tileID);
            System.Diagnostics.Debug.WriteLine("NoteID before MSGBOX dele: " + lastSelectedNote.ID);
            Guide.BeginShowMessageBox(
                "Please confirm",
                "Are you sure you want to delete the note?",
                new string[] { "Yes", "No" }, 0,
                MessageBoxIcon.Warning,
                new AsyncCallback(OnMessageBoxAction),
                null);
            }

        private void OnMessageBoxAction(IAsyncResult ar)
            {
            int? selectedButton = Guide.EndShowMessageBox(ar);
            switch (selectedButton)
                {
                case 0:
                    System.Diagnostics.Debug.WriteLine("tileID after MSGBOX delete: " + tileID);
                    System.Diagnostics.Debug.WriteLine("NoteID after MSGBOX dele: " + lastSelectedNote.ID);
                    Deployment.Current.Dispatcher.BeginInvoke(() => DeletePart2());
                    break;
                default:
                    break;
                }
            }

        private async void DeletePart2()
            {
            System.Diagnostics.Debug.WriteLine("tileID before delete: " + tileID);
            System.Diagnostics.Debug.WriteLine("NoteID before dele: " + lastSelectedNote.ID);
            System.Diagnostics.Debug.WriteLine("backTitle: " + backTitle);
            System.Diagnostics.Debug.WriteLine("FirstLine: " + lastSelectedNote.FirstLine);           

            if (TileToFind != null && tileID == lastSelectedNote.ID)
                {
                TileToFind.Update(new StandardTileData
                {
                    BackContent = "Note it again, Sam, init man, howdy ho.",

                });
                tileID = null;
                }

            await App.ViewModel.RemoveNote(lastSelectedNote);

            if (TileToFind != null)
                {
                TileToFind.Update(new StandardTileData
                {
                    Count = App.ViewModel.Items.Count,

                });
                }
            System.Diagnostics.Debug.WriteLine("tileID after delete: " + tileID);
            System.Diagnostics.Debug.WriteLine("NoteID after dele: " + lastSelectedNote.ID);
            }


        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
        private void PinButton_Click(object sender, EventArgs e)
            {
            if (!Object.ReferenceEquals(lastSelectedNote, null))
                {
                backTitle = lastSelectedNote.FirstLine;
                System.Diagnostics.Debug.WriteLine("tileID @beginning of addPin: " + tileID);
                }
            else
                {
                backTitle = "Note it again, Sam.";
                tileID = null;
                }
            // Look to see if the tile already exists and if so, don't try to create again.

            // Create the tile if we didn't find it already exists.
            if (TileToFind == null)
                {
                // Create the tile object and set some initial properties for the tile.
                // The Count value of 12 will show the number 12 on the front of the Tile. Valid values are 1-99.
                // A Count value of 0 will indicate that the Count should not be displayed.
                StandardTileData NewTileData = new StandardTileData

                {

                    BackgroundImage = new Uri("/Assets/finger-string.jpg", UriKind.Relative),
                    Title = "ProjectWinphone2017",
                    Count = App.ViewModel.Items.Count,
                    BackTitle = "Notes",
                    BackContent = backTitle,

                    BackBackgroundImage = new Uri("", UriKind.Relative)

                };

                // Create the tile and pin it to Start. This will cause a navigation to Start and a deactivation of our application.
                ShellTile.Create(new Uri("/MainPage.xaml?DefaultTitle=FromTile", UriKind.Relative), NewTileData);
                }

            else
                {

                TileToFind.Update(new StandardTileData
                                {
                                    BackgroundImage = new Uri("/Assets/finger-string.jpg", UriKind.Relative),
                                    Title = "ProjectWinphone2017",
                                    Count = App.ViewModel.Items.Count,
                                    BackTitle = "Notes",
                                    BackContent = backTitle,
                                    BackBackgroundImage = new Uri("", UriKind.Relative)
                                });
                }

            System.Diagnostics.Debug.WriteLine("tileID @addPin: " + tileID);
            }
        }
    }