using Scratch.Data;
using Scratch.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using static Android.Provider.ContactsContract.CommonDataKinds;
//using static Android.Content.ClipData;

namespace Scratch
{
    public partial class MainPage : ContentPage
    {
        // Binds to collection of notes in xaml view
        public ObservableCollection<NoteItem> Notes { get; set; } = new();
        // Binds to text in editor in xaml view
        public string EditorText { get; set; } = String.Empty;
        //public ICommand SelectNote => new Command(() => ShowNoteInEditor());
        readonly Database _database;

        public MainPage()
        {
            InitializeComponent();
            //NotesCollectionView.ItemsSource = Notes;

            BindingContext = this;

            _database = new Database();
            _ = Initialize();
        }

        private async Task Initialize()
        {
            //var note = new NoteItem();
            //note.Text = "Take out the trash!";
            //note.CreatedDate = DateTime.Now;
            //Notes.Add(note);

            //var note2 = new NoteItem();
            //note2.Text = "Take out the sh!";
            //note2.CreatedDate = DateTime.Now;
            //Notes.Add(note2);

            //var note3 = new NoteItem();
            //note3.Text = "Take out th!";
            //note3.CreatedDate = DateTime.Now;
            //Notes.Add(note3);
            await _database.DeleteAllNotes();

            var dbnotes = await _database.GetNotes();
            foreach (var dbnote in dbnotes)
            {
                Notes.Add(dbnote);
                Console.WriteLine(dbnote.Id.ToString());
                Console.WriteLine(dbnote.Text.ToString());
                Console.WriteLine(dbnote.CreatedDate.ToString());
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var newnote = new NoteItem
            {
                Text = EditorText,
                CreatedDate = DateTime.Now
            };

            var inserted = await _database.AddNote(newnote);
            if (inserted != 0)
            {
                Notes.Add(newnote);
                EditorText = String.Empty;
                //Console.WriteLine(inserted.ToString());
                //Console.WriteLine(inserted.ToString());
                //Console.WriteLine(inserted.ToString());
            }
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            await App.Current.MainPage.DisplayAlert(item.Text, $"You invoked the {item.Text} action.", "OK");
        }

        private async void SelectNote(object sender, EventArgs e)
        {
            await App.Current.MainPage.DisplayAlert("hi", $"You invoked the next action.", "OK");
        }
    }
}